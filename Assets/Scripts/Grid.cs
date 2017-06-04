using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int columns = 5;
    private int rows = 5;
    public Tile emptyTile;

    private Transform gridHolder;
    private Tile[,] gridPositions = new Tile[5,5];
    private float xOffset, yOffset;
    private float offsetValue = 0.05f;
    List<Tile> matches = new List<Tile>();


    public void GridSetup()
    {
        xOffset = 0f;
        yOffset = 0f;
        if(gridHolder!=null)
            Destroy(gridHolder.gameObject);
        gridHolder = new GameObject("Grid").transform;
        gridHolder.transform.position = new Vector3(0,0,0);
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Tile instance = Instantiate(emptyTile, new Vector2(i + xOffset, j + yOffset), Quaternion.identity);
                instance.transform.SetParent(gridHolder);
                instance.name = string.Format("[{0},{1}]", i, j);
                gridPositions[i, j] = instance;
                instance.Column = i;
                instance.Row = j;
                yOffset += offsetValue;
            }
            xOffset += offsetValue;
            yOffset = 0f;
        }
    }

    public Tile GetTileByCoordinates(int x, int y)
    {
        return gridPositions[x, y];
    }

    public List<Tile> GetMatches(int x, int y, Color color)
    {
        matches.Clear();
        matches.Add(GetTileByCoordinates(x, y));
        HorizontalMatch(x, y, color);
        VericalMatch(x, y, color);
        return matches;
    }

    private void HorizontalMatch(int x, int y, Color color)
    {
        if (x != 0)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (GetTileByCoordinates(i, y).Color == color)
                    matches.Add(GetTileByCoordinates(i, y));
                else
                    break;
            }
        }

        if (x < columns)
        {
            for (int i = x + 1; i < columns; i++)
            {
                if (GetTileByCoordinates(i, y).Color == color)
                    matches.Add(GetTileByCoordinates(i, y));
                else
                    break;
            }
        }
    }

    private void VericalMatch(int x, int y, Color color)
    {
        if (y != 0)
        {
            for (int i = y - 1; i >= 0; i--)
            {
                if (GetTileByCoordinates(x, i).Color == color)
                    matches.Add(GetTileByCoordinates(x, i));
                else
                    break;
            }
        }

        if (y < rows)
        {
            for (int i = y + 1; i < rows; i++)
            {
                if (GetTileByCoordinates(x, i).Color == color)
                    matches.Add(GetTileByCoordinates(x, i));
                else
                    break;
            }
        }
    }

    public bool isFilled
    {
        get
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (!gridPositions[i, j].HasColor)
                        return false;
                }
            }
            return true;
        }
    }
}
