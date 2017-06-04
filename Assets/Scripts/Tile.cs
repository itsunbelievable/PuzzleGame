using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tile : MonoBehaviour
{
    private Material mat;

    public delegate void TileClicked(object sender, EventArgs e);
    public event TileClicked OnTileClicked;
    public int row, column;
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

    void Click(Tile reciever)
    {
        if (OnTileClicked != null)
            OnTileClicked(reciever, EventArgs.Empty);
    }
}
	