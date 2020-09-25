using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int numberLvl; 
    public int width, height;
    public GameObject fireTile,waterTile, emptyTile;
    private Tile[,] allTile;
    private int[,] level; 

    void Start()
    {
        allTile = new Tile[width, height];
        SetUp(); 
    }

    /// <summary>
    /// Gereration level
    /// </summary>
    private void SetUp()
    {
        if(numberLvl == 1)
            level = new int[,] { { 2, 2}, { 0, 0}, { 1, 2 }, { 1, 0 }, { 1, 0 } };
        else if (numberLvl == 2)
            level = new int[,] { { 2, 2, 1, 2, 2 }, { 1, 1, 2, 1, 2 }, { 2, 2, 1, 2, 0 }, { 2, 2, 1, 2, 0 } };
        else if (numberLvl == 3)
            level = new int[,] { { 2, 2, 1, 2, 2, 0 }, { 1, 1, 2, 2, 1, 2 }, { 2, 2, 1, 0, 0, 0 }, { 2, 2, 1, 2, 0, 0 } };

        float xPos = transform.position.x, yPos = transform.position.y;
        for (int x = 0; x < height; x++)
            for (int y = 0; y < width; y++)
            {
                Vector2 tempPosition = new Vector2(xPos + x,yPos + y);
                GameObject tileNow;
                if (level[x, y] == 1) tileNow = fireTile;
                else if (level[x, y] == 0) tileNow = emptyTile;
                else tileNow = waterTile;
                GameObject newTile = Instantiate(tileNow, tempPosition, Quaternion.identity);
                newTile.transform.parent = transform;
            }
    }
}
