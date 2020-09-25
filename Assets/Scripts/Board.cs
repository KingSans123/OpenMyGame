using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Board : MonoBehaviour
{
    //public static Board instance;

    //private int xSize, ySize;
    //private Tile fireTile, waterTile;

    //private void Awake()
    //{
    //    instance = this;
    //}

    //public SetValue(int xSize, int ySize, Tile fireTile, Tile waterTile)
    //{
    //    this.xSize = xSize;
    //    this.ySize = ySize;
    //    this.fireTile = fireTile;
    //    this.waterTile = waterTile;

    //    CreateBoard();
    //}

    //public void CreateBoard()
    //{
    //    Tile[,] tileArray = Tile[xSize, ySize];
    //    float xPos = transform.position.x, yPos = transform.position.y;
    //    Vector2 tileSize = fireTile.spriteRenderer.bounds.size;
    //    for (int x = 0; x < xSize; x++)
    //    {
    //        for (int y = 0; y < ySize; y++)
    //        {
    //            Tile newTile = Instantiate(fireTile, transform.position, Quternion.identity);
    //            newTile.transform.position = new Vector3(xPos + (tileSize.x * x), yPos + (tileSize.y * y), 0);
    //        }
    //    }
    //}
}
