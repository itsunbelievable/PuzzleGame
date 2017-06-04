using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Blocks : MonoBehaviour
{
    public Tile emtpyTile;
    public Transform blocksHolder;
    private float offsetValue = 0.05f, xOffset = 0f;
    List<Tile> blocks;
    public void CreateBlocks(int size)
    {
        if (blocksHolder == null)
            blocksHolder = new GameObject("Blocks").transform;
        blocks = new List<Tile>(size);
        for (int i = 0; i < size; i++)
        {
            Tile instance = Instantiate(emtpyTile, new Vector2(blocksHolder.transform.localPosition.x + i + xOffset, blocksHolder.transform.position.y), Quaternion.identity);
            instance.transform.SetParent(blocksHolder);
            instance.Color = colorCollection[Random.Range(0, colorCollection.Count)];
            instance.Row = -1;
            instance.Column = -1;
            blocks.Add(instance);
            xOffset += offsetValue;
        }
        xOffset = 0f;
    }

    public Tile GetTile
    {
        get
        {
            var t = blocks[0];
            blocks.RemoveAt(0);
            Destroy(blocksHolder.GetChild(0).gameObject);
            return t;
        } 
    }

    public bool IsEmpty
    {
        get { return blocks.Count == 0; }
    }


    readonly List<Color> colorCollection = new List<Color>()
    {
        Color.blue,
        Color.red,
        Color.green,
        Color.yellow
    };
}
 