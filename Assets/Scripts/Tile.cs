using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    private Material mat;

    private int row, column;
    public int Row { get; set; }
    public int Column { get; set; }
    void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    public bool HasColor { get { return Color != Color.white; } }

    public Color Color
    {
        get { return mat.color; }
        set { mat.color = value; }
    }
}
	