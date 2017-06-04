using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameScreen : MonoBehaviour
{
    public Grid gridScript;
    public Blocks blocksScript;
    private Camera mainCam;
    List<Tile> addedTiles;
    int score, hiscore;
    public Text scoreText, hiScoreText, gameOverText;
    public Button restart;

    void Awake()
    {
        gridScript = GetComponent<Grid>();
        
        blocksScript = GetComponent<Blocks>();
        blocksScript.CreateBlocks(GetRandom());

        mainCam = Camera.main;
        addedTiles = new List<Tile>();

        Restart();
        restart.onClick.AddListener(Restart);
    }

    private void TileClicked(Tile clickedTile)
    {
        if (clickedTile.HasColor)
            return;
        clickedTile.Color = blocksScript.GetTile.Color;
        addedTiles.Add(clickedTile);

        if (gridScript.isFilled)
        {
            gameOverText.text = "Game Over";
            restart.gameObject.SetActive(true);
        }
    }

    void Restart()
    {
        gameOverText.text = "";
        restart.gameObject.SetActive(false);
        gridScript.GridSetup();
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
                foreach (var clickedTile in addedTiles)
                {
                    var matchedTiles = gridScript.GetMatches(clickedTile.Column, clickedTile.Row, clickedTile.Color);
                    if (matchedTiles.Count >= 3)
                    {
                        foreach (var match in matchedTiles)
                        {
                            match.Color = Color.white;
                        }
                        score += 10;
                    }
                }
                UpdateScore();
                addedTiles.Clear();
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
                foreach (var clickedTile in addedTiles)
                {
                    var matchedTiles = gridScript.GetMatches(clickedTile.Column, clickedTile.Row, clickedTile.Color);
                    if (matchedTiles.Count >= 3)
                    {
                        foreach (var match in matchedTiles)
                        {
                            match.Color = Color.white;
                        }
                        score += 10;
                    }
                }
                UpdateScore();
                addedTiles.Clear();
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

    void UpdateScore()
    {
        if (score > hiscore)
            hiscore = score;
        scoreText.text = "Score: " + score;
        hiScoreText.text = "HiScore: " + hiscore;
    }
}
