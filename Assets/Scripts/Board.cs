using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI; 

public class Board : MonoBehaviour
{
    public GameObject[,] allDots;
    public GameObject[] dots;
    public int numberLvl, width, height; 
    public GameObject tilePrefab;
    public Button buttonClick;


    private Tile[,] allTile;
    private int[,] level;
    private bool allDotsIsEmpty = false; 

    void Start()
    {
        allTile = new Tile[width, height];
        allDots = new GameObject[width, height];
        SetUp(); 
    }

    void Update()
    {

        CheckingForAllEmpty();
        if (allDotsIsEmpty)
        {
            buttonClick.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void CheckingForAllEmpty()
    {
        GameObject dot;
        allDotsIsEmpty = true; 
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                dot = allDots[i, j];
                if (dot.tag == "Water" || dot.tag == "Fire")
                {
                    allDotsIsEmpty = false;
                    break; 
                }
            }
    }

        /// <summary>
        /// Gereration level
        /// </summary>
    private void SetUp()
    {
        //Change level
        switch(numberLvl)
        {
            case 1: 
                level = new int[,] { { 2, 2 }, { 0, 0 }, { 2, 1 }, { 1, 0 }, { 1, 0 } };
                break;
            case 2:
                level = new int[,] { { 2, 2, 1, 2, 2 }, { 1, 1, 2, 1, 2 }, { 2, 2, 1, 2, 0 }, { 2, 2, 1, 2, 0 } };
                break;
            case 3:
                level = new int[,] { { 2, 2, 1, 2, 2, 0 }, { 1, 1, 2, 2, 1, 2 }, { 2, 2, 1, 0, 0, 0 }, { 2, 2, 1, 2, 0, 0 } };
                break;
            default:
                level = new int[width, height];
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                        level[i, j] = Random.Range(0, 2);
                break;
        }

        //create game board 
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2( i, j);

                GameObject newTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                newTile.transform.parent = this.transform;
                newTile.name = $"({i},{j})";

                int dotToUse;
                if (level[i, j] == 0) dotToUse = 0; 
                else if (level[i, j] == 1) dotToUse = 1;
                else dotToUse = 2;

                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = $"({i},{j})";
                allDots[i, j] = dot; 
            }
    }

    /// <summary>
    /// delete dot 
    /// </summary>
    /// <param name="column">column dot</param>
    /// <param name="row">row dot</param>
    private void DestroyMatchesAt(int column, int row)
    {
        if(allDots[column, row].GetComponent<Dot>().isMatched)
        {
            Destroy(allDots[column, row]);
            allDots[column, row] = dots[0]; 
        }
    }

    /// <summary>
    /// delete all dots
    /// </summary>
    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                if (allDots[i, j] != dots[0])
                    DestroyMatchesAt(i, j);
            }
    }
}
