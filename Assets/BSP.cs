using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{
    public int gridWidth = 100;
    public int gridHeight = 100;
    Vector3 postionOffset = new Vector3(0, -100, 0);

    public string seed;
    public bool useRandomSeed = true;
    public bool useConway = false;

    public int timesToSmooth = 6;

    [Range(0, 100)]
    public int randomFillPercent = 47;

    int[,] grid;
    private System.Random pseudoRandom;

    void Start()
    {
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.ToString();
        }
        pseudoRandom = new System.Random(seed.GetHashCode());

        GenerateMap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        grid = new int[gridWidth, gridHeight];
        FillMap();
        Generate();
    }

    void FillMap()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = 1;
            }
        }
    }
    
    void Generate()
    {
        RectInt dungeon = new RectInt(0, 0, gridWidth, gridHeight);


    }

    void Divide(RectInt space)
    {

        RectInt left = new RectInt();
        RectInt right = new RectInt();
        Divide(left);
        Divide(right);
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Gizmos.color = (grid[x, y] == 1) ? Color.green : Color.gray;
                    Vector3 pos = new Vector3(-gridWidth / 2 + x + 0.5f, -gridHeight / 2 + y + 0.5f, 0);
                    Gizmos.DrawCube(pos + postionOffset, Vector3.one);
                }
            }
        }
    }
}
