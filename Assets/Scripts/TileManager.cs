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

        Debug.Log($"Ÿ�� ���� �Ϸ� x : ${tileX}, y : ${tileY}");
    }
}
