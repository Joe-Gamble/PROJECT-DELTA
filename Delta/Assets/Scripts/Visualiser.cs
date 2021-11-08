using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;


public class Visualiser : MonoBehaviour
{
    public Transform root_transform;
    public List<Chain> chains = new List<Chain>();

    public GameObject target;
    public GameObject pole;

    public static int Iterations = 10;
    public static float Delta = 0.001f;
    // Start is called before the first frame update

    //Declare origin transform
    //loop through transfrorms and init chains for all transforms without children (they are ends of the chains)

    void Start()
    {
        InitRig();
        WriteTestData();

        chains[0].SetTarget(target.transform);
        //chains[0].SetPole(pole.transform);
    }

    public void WriteTestData()
    {
        string path = Application.dataPath + "/Log.json";
        string json = JsonUtility.ToJson(JsonParser.WriteJson(chains), true);

        File.WriteAllText(path, json);

        AssetDatabase.Refresh();
    }

    void InitRig()
    {
        Transform[] children = root_transform.GetComponentsInChildren<Transform>();

        foreach(Transform child in children)
        {
            if(child.childCount == 0)
            {
                MakeChain(child);
            }
        }
    }

    private void MakeChain(Transform end_point)
    {
        Chain chain = new Chain(root_transform, end_point);
        
        chains.Add(chain);
    }

    void LateUpdate()
    {
        foreach(Chain chain in chains)
        {
            chain.Resolve();
        }
    }

    private void OnDrawGizmos() 
    {
        foreach(Chain chain in chains)
        {
            Gizmos.color = chain.chain_color;

            foreach(Node joint in chain.nodes)
            {
                Gizmos.DrawSphere(joint.self.position, 0.01f);
                Gizmos.DrawLine(joint.self.position, joint.parent.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct Node
{
    public Transform self;
    public Transform parent;

    public Node(Transform target)
    {
        self = target;
        parent = self.parent;
    }
}

public class Bone
{
    public float length = 0;
    public Node start, end;
    public Bone(Node _start, Node _end)
    {
        start = _start;
        end = _end;

        length = (_start.self.position - _end.self.position).magnitude;
    }
}

public class Chain
{
    [SerializeField] public List<Node> nodes;
    [SerializeField] public List<Bone> bones;

    public Color chain_color;
    public Color bone_color;

    public Transform end_point;
    public float complete_length;

    public Transform target = null;
    public Transform pole = null;

    public Chain(Transform root, Transform end)
    {
        nodes = new List<Node>();
        bones = new List<Bone>();

        InitNodes(root, end);
        nodes.Reverse();

        InitBones();

        foreach(Bone bone in bones)
        {
            complete_length += bone.length;
        }

        chain_color = UnityEngine.Random.ColorHSV();
        bone_color = UnityEngine.Random.ColorHSV();
    }

    public void InitNodes(Transform root_node, Transform end_node)
    {
        Transform current_transform = end_node;

        while (current_transform != root_node.parent)
        {
            Node new_node = new Node(current_transform); 

            if(nodes.Count == 0)
            {
                end_point = current_transform;
            }

            nodes.Add(new_node);

            if(current_transform.parent == null)
            {
                Debug.LogError("Could not find parent for this transform: " + current_transform.name);
                break;
            }
            current_transform = current_transform.parent;
        }
    }

    public void InitBones()
    {
        for(int i = 0; i < nodes.Count - 1; i++)
        {
            Bone new_bone = new Bone(nodes[i], nodes[i+1]);
            bones.Add(new_bone);
        }
    }

    public void Resolve()
    {
        //Check if the bone list is valid
        if(isValid())
        {
            Transform[] transforms = new Transform[bones.Count];

            //Get Positions
            for(int i = 0; i < bones.Count; i++)
            {
                transforms[i] = bones[i].start.self;
            }

            Transform end_transform = transforms[0];

            Debug.Log("End Transform = " + end_transform.name);

            //Calculation - Is target possible to reach?
            if ((target.position - end_transform.position).sqrMagnitude >= complete_length * complete_length)
            {
                //get direction
                var dir = (target.position - end_transform.position).normalized;

                //apply to every bone
                for(int i = 1; i < transforms.Length; i++ )
                {
                    transforms[i].position = transforms[i - 1].position + dir * bones[i - 1].length;
                }
            }
            else
            {
                Transform root_transform = bones[bones.Count - 1].end.self;

                for(int it = 0; it < Visualiser.Iterations; it++)
                {
                    //back function
                    for(int i = bones.Count - 1; i > 0; i--)
                    {
                        //Set to target
                        if (i == bones.Count - 1)
                        {
                            transforms[i].position = target.position;
                        }
                        else
                        {
                            transforms[i].position = transforms[i + 1].position + (transforms[i].position -  transforms[i + 1].position).normalized * bones[i].length;
                        }
                    }

                    for(int i = 1; i < bones.Count; i++)
                    {
                        transforms[i].position = transforms[i - 1].position + 
                            (transforms[i].position -  transforms[i - 1].position).normalized * bones[i - 1].length;
                    }


                    //is the result close enough to the target
                    if((root_transform.position - target.position).sqrMagnitude < Visualiser.Delta * Visualiser.Delta)
                    {
                        break;
                    }
                }
            }

            //move towards pole
            if(pole != null)
            {
                for(int i = 1; i < transforms.Length - 1; i++)
                {
                    var plane = new Plane(transforms[i + 1].position - transforms[i - 1].position, transforms[i - 1].position);
                    var projectedPole = plane.ClosestPointOnPlane(pole.position);
                    var projectedBone = plane.ClosestPointOnPlane(transforms[i].position);
                    var angle = Vector3.SignedAngle(projectedBone - transforms[i - 1].position, projectedPole - transforms[i - 1].position, plane.normal);

                    transforms[i].position = Quaternion.AngleAxis(angle, plane.normal) * transforms[i].position - transforms[i - 1].position;
                }
            }

            //Set Positions
            for(int i = 0; i < transforms.Length; i++)
            {
                bones[i].start.self = transforms[i];
            }
        }
    }

    public void SetTarget(Transform _target)
    {
       target = _target;
       _target.gameObject.GetComponent<MeshRenderer>().materials[0].color = chain_color;
    }

    public void SetPole(Transform _pole)
    {
        pole = _pole;
        _pole.gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = chain_color;
    }

    private bool isValid()
    {
        return (target != null && bones.Count > 0);
    }
}


