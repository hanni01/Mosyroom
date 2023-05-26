using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectTile : MonoBehaviour
{
    private double PointX;
    private double PointY;
    private Vector3 center;
    private double width;
    private double height;
    private double HorizontalDiagonal;
    private double VerticalDiagonal;
    private List<TileManager> tile = new List<TileManager>();
    private Vector3 parentPosition;

    public Transform parent;

    void Start()
    {
        center = parent.transform.position;
        width = parent.GetComponent<RectTransform>().rect.width;
        height = parent.GetComponent<RectTransform>().rect.height;
        PointX = center.x - width / 2;
        PointY = center.y + height / 2;
        HorizontalDiagonal = width / 10;
        VerticalDiagonal = height / 10;
        parentPosition = parent.transform.position;

        Debug.Log("부모 오브젝트 포지션:"+ center);
        Debug.Log("width: " + width + ", height: " + height);
        Debug.Log("H 대각선: "+HorizontalDiagonal+", V 대각선:"+ VerticalDiagonal);

        for(int j = 0;j < 10; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                TileManager tiles = new TileManager((PointX + (HorizontalDiagonal / 2) * j + (HorizontalDiagonal / 2) * (i + 1)) / 2, (center.y - (VerticalDiagonal / 2) * j + (VerticalDiagonal / 2) * i) / 1.85);
                tile.Add(tiles);
            }
        }

        foreach (TileManager tmpTile in tile)
        {
            Console.WriteLine($"X: {tmpTile.tileX}, Y: {tmpTile.tileY}");
        }

        GameObject gridTile = (GameObject)Resources.Load("Prefabs/" + "subGrid");

        for(int i = 0;i < tile.Count; i++)
        {
            GameObject instance = Instantiate(gridTile, parent);
            instance.transform.position = (parent.transform.position / 2) + new Vector3((float)tile[i].tileX , (float)tile[i].tileY, 0);
            instance.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        //        Input.mousePosition.y, -Camera.main.transform.position.z));

        //for(int i = 0;i < tile.Count; i++)
        //{
        //    if(point.x == tile[i].tileX && point.y == tile[i].tileY)
        //    {

        //    }
        //}
    }
}
