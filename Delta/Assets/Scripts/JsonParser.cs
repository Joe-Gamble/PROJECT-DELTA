using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
    public struct JsonNode
    {
        public string self;
        public string parent;
    }

    [Serializable]
    public struct JsonBone
    {
        public float length;
        public string start_bone;
        public string end_bone;
    }

    [Serializable]
    public struct JsonChain
    {
        public List<JsonNode> j_nodes;
        public List<JsonBone> j_bones;

        public string end_leaf_name;
        public float complete_length;
    }

    [Serializable]
    public struct JsonData
    {
        public List<JsonChain> j_chains;
    }


public static class JsonParser
{
    public static JsonData WriteJson(List<Chain> chains)
    {
        JsonData j_data = new JsonData();

        j_data.j_chains =  new List<JsonChain>();

        foreach(Chain chain in chains)
        {                                                   
            JsonChain j_chain = new JsonChain();

            j_chain.j_bones = new List<JsonBone>();
            j_chain.j_nodes = new List<JsonNode>();

            j_chain.end_leaf_name = chain.end_point.name;
            j_chain.complete_length = chain.complete_length;

            foreach(Node node in chain.nodes)
            {
                JsonNode j_node = new JsonNode();

                j_node.self = node.self.name;
                j_node.parent = node.parent.name;

                j_chain.j_nodes.Add(j_node);
            }

            foreach(Bone bone in chain.bones)
            {
                JsonBone j_bone = new JsonBone();

                j_bone.length = bone.length;
                j_bone.start_bone = bone.start.self.name; 
                j_bone.end_bone = bone.end.self.name; 

                j_chain.j_bones.Add(j_bone);
            }

            j_data.j_chains.Add(j_chain);
        }

        Debug.Log(j_data.j_chains.Count);

        return j_data;
    }
}
