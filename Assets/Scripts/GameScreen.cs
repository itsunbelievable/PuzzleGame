using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen: MonoBehaviour
{
    public Grid gridScript;
    public Blocks blocksScript;
    void Awake()
    {
        gridScript = GetComponent<Grid>();
        gridScript.GridSetup();

        blocksScript = GetComponent<Blocks>();
        blocksScript.CreateBlocks(GetRandom());

        gridScript.OnGridTileClicked += GridScript_OnGridTileClicked;
    }

    private void GridScript_OnGridTileClicked(object sender, System.EventArgs e)
    {
        Tile clickedTile = (Tile) sender;
        clickedTile.Color = blocksScript.GetTile.Color;
    }

    void FixedUpdate ()
    {
        if (blocksScript.IsEmpty)
        {
            blocksScript.CreateBlocks(GetRandom());
        }
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
