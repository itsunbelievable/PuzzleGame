using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameScreen : MonoBehaviour
{
    public Grid gridScript;
    public Blocks blocksScript;
    private Camera mainCam;
    List<Tile> added;
    void Awake()
    {
        gridScript = GetComponent<Grid>();
        gridScript.GridSetup();

        blocksScript = GetComponent<Blocks>();
        blocksScript.CreateBlocks(GetRandom());

        mainCam = Camera.main;
        added = new List<Tile>();
    }

    private void TileClicked(Tile clickedTile)
    {
        if (clickedTile.HasColor)
            return;
        clickedTile.Color = blocksScript.GetTile.Color;
        added.Add(clickedTile);
    }

    void FixedUpdate()
    {
        if (blocksScript.IsEmpty)
        {
            blocksScript.CreateBlocks(GetRandom());
        }
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCam.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject reciever = hit.transform.gameObject;
                TileClicked(reciever.GetComponent<Tile>());
            }

            if (touch.phase == TouchPhase.Ended)
            {
                foreach (var clickedTile in added)
                {
                    var matchedTiles = gridScript.GetMatches(clickedTile.column, clickedTile.row, clickedTile.Color);
                    if (matchedTiles.Count >= 3)
                    {
                        foreach (var match in matchedTiles)
                        {
                            match.Color = Color.white;
                        }
                    }
                }
                added.Clear();
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject reciever = hit.transform.gameObject;
                TileClicked(reciever.GetComponent<Tile>());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            {
                foreach (var clickedTile in added)
                {
                    var matchedTiles = gridScript.GetMatches(clickedTile.column, clickedTile.row, clickedTile.Color);
                    if (matchedTiles.Count >= 3)
                    {
                        foreach (var match in matchedTiles)
                        {
                            match.Color = Color.white;
                        }
                    }
                }
                added.Clear();
            }
        }
#endif
    }

    int GetRandom()
    {
        int i = Random.Range(1, 100);
        if (i < 10)
            return 1;
        if (i < 50)
            return 2;
        if (i < 90)
            return 3;
        else
            return 4;
    }
}
