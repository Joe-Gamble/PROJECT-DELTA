using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLook : MonoBehaviour
{
    private PlayerCamManager pcm;
    private GameObject focused_obj = null;

    //move collider stats from component to playerstats scriptable?
    private float range = 10f;
    private int LayerMask = 1 << 7;

    // Start is called before the first frame update
    void Start()
    {
        pcm = PlayerCamManager.Instance;
    }

    public bool hasFocus()
    {
        return focused_obj != null;
    }

    public GameObject getFocus()
    {
        return focused_obj;
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = pcm.GetRawCam();

        RaycastHit hit;
        GameObject new_focus = null;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, LayerMask, QueryTriggerInteraction.Collide))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) <= range)
            {
                new_focus = hit.transform.gameObject;
            }
        }

        if (focused_obj != new_focus)
        {
            if (focused_obj != null)
            {
                //deactivate UI
            }

            focused_obj = new_focus;
            //Activate UI
        }
    }
}
