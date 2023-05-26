using Model;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class FurnitureObj : BaseUnit
{
    public HistoricalData previous { get; private set; }
    private List<GameObject> blocks;

    public void Move(Tile tile)
    {
        gameObject.transform.position = tile.transform.position;
        origin = tile;
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void Place(List<Tile> tiles)
    {
        base.tiles = tiles;
        base.tiles.ForEach(tile => tile.isBlock = true);

        previous = new HistoricalData(origin);

        Block(tiles);
    }

    public void Unplaced()
    {
        tiles.ForEach(tile => tile.isBlock = false);
        tiles = new List<Tile>();

        UnBlock();
    }

    private void Block(List<Tile> tiles)
    {
        blocks = new List<GameObject>();
        foreach (var tile in tiles)
        {
            var block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.SetParent(GameObject.Find("UIManager").transform);
            block.transform.localEulerAngles = new Vector3(0, 0, 0);
            block.transform.localScale = new Vector3(2.8f, 1f, 2.8f);
            block.transform.position = new Vector3(tile.gameObject.transform.position.x, 0, tile.gameObject.transform.position.y * 2);
            block.AddComponent<NavMeshObstacle>().carving = true;
            block.GetComponent<Renderer>().enabled = false;
            blocks.Add(block);
        }
    }

    private void UnBlock()
    {
        if(blocks != null)
        {
            blocks.ForEach(block => DestroyImmediate(block));
            blocks = null;
        }
    }
}
