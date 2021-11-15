using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ref
//https://learn.unity.com/project/procedural-cave-generation-tutorial
public class CelluarAutomata : MonoBehaviour
{
	public int gridWidth = 100;
	public int gridHeight = 100;
	public Vector3 postionOffset = new Vector3(50, 0, 0);

	public string seed;
	public bool useRandomSeed = true;
	public int timesToSmooth = 6;

	[Range(0, 100)]
	public int randomFillPercent = 50;

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
		RandomFillMap();

		for (int i = 0; i < timesToSmooth; i++)
		{
			SmoothMap();
		}
	}

	void RandomFillMap()
	{
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				if (x == 0 || x == gridWidth - 1 || y == 0 || y == gridHeight - 1)
				{
					grid[x, y] = 1;
				}
				else
				{
					grid[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
				}
			}
		}
	}

	void SmoothMap()
	{
		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				int neighbourWallTiles = GetNeighbourWallCount(x, y);

				if (neighbourWallTiles > 4)
					grid[x, y] = 1;
				else if (neighbourWallTiles < 4)
					grid[x, y] = 0;
			}
		}
	}

	int GetNeighbourWallCount(int gridX, int gridY) //moore
	{
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
		{
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
			{
				if (neighbourX >= 0 && neighbourX < gridWidth && 
					neighbourY >= 0 && neighbourY < gridHeight)
				{
					if (neighbourX != gridX || neighbourY != gridY)
					{
						wallCount += grid[neighbourX, neighbourY];
					}
				}
				else
				{
					wallCount++;
				}
			}
		}
		return wallCount;
	}

	Color gray = new Color(0.4f, 0.4f, 0.4f);
	void OnDrawGizmos()
	{
		if (grid != null)
		{
			for (int x = 0; x < gridWidth; x++)
			{
				for (int y = 0; y < gridHeight; y++)
				{
					Gizmos.color = (grid[x, y] == 1) ? gray : Color.green;
					Vector3 pos = new Vector3(-gridWidth / 2 + x + 0.5f, -gridHeight / 2 + y + 0.5f, 0);
					Gizmos.DrawCube(pos + postionOffset, Vector3.one);
				}
			}
		}
	}
}
