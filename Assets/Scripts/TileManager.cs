using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    public double tileX;
    public double tileY;
    public double area; 

    public TileManager(double tileX, double tileY)
    {
        this.tileX = tileX;
        this.tileY = tileY;

        Debug.Log($"타일 생성 완료 x : ${tileX}, y : ${tileY}");
    }
}
