using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector3Int pos;
    public Color color;
    private Material material;
    public Vector3Int Pos 
    {
        get { return pos; }
        set 
        {
            pos = value;
            transform.position = pos;
        }
    }
    public Color Color {
        get { return color; }
        set 
        {
            color = value;
            gameObject.GetComponent<MeshRenderer>().material.color = color;
        }
    }
    public Material Material
    {
        get { return material; }

        set
        {
            material = value;
            GetComponent<MeshRenderer>().material = material;
        }
    }
}
