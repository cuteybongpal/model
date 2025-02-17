using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector3Int pos;
    private Color color;

    public Vector3Int Pos {  get { return pos; } set { pos = value; } }
    public Color Color { get { return color; } set { color = value; } }
}
