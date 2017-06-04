using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int columns = 5;
    private int rows = 5;
    public Tile emptyTile;

    private Transform gridHolder;
    private Tile[,] gridPositions = new Tile[5,5];
    private float xOffset = 0f, yOffset = 0f;
    private float offsetValue = 0.05f;

    public delegate void GridTileClicked(object sender, EventArgs e);

    public event GridTileClicked OnGridTileClicked;
    public void GridSetup()
    {
        gridHolder = new GameObject("Grid").transform;
        gridHolder.transform.position = new Vector3(0,0,0);
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Tile instance = Instantiate(emptyTile, new Vector2(i + xOffset, j + yOffset), Quaternion.identity);
                instance.transform.SetParent(gridHolder);
                instance.OnTileClicked += ClickFromTile;
                instance.name = string.Format("[{0},{1}]", i, j);
                gridPositions[i, j] = instance;
                instance.column = j;
                instance.row = i;
                yOffset += offsetValue;
            }
            xOffset += offsetValue;
            yOffset = 0f;
        }
    }

    public void ClickFromTile(object sender, EventArgs e)
    {
        Tile clickedTile = (Tile) sender;
        if(clickedTile.HasColor)
            return;
        if(OnGridTileClicked!=null)
            OnGridTileClicked(clickedTile, EventArgs.Empty);     
    }
}
