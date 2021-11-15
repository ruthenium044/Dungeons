using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ref
//http://www.roguebasin.com/index.php?title=Diffusion-limited_aggregation

public class Diffusion : MonoBehaviour
{
	public int gridWidth;
	public int gridHeight;

	public string seed;
	public bool useRandomSeed;

	public int steps;

	private Vector2Int startPos;
	private Vector2Int currentPos;

	int[,] grid;
	System.Random pseudoRandom;

	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left };

	void Start()
	{
		if (useRandomSeed)
		{
			seed = Time.time.ToString();
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

		startPos.x = gridWidth / 2;
		startPos.y = gridHeight / 2;
		grid[startPos.x, startPos.y] = 0;

		Walk();
		//Carve();
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

	void Carve()
	{
		startPos.x = gridWidth / 2;
		startPos.y = gridHeight / 2;
		grid[startPos.x, startPos.y] = 0;

		currentPos = new Vector2Int(pseudoRandom.Next(1, gridWidth), pseudoRandom.Next(1, gridHeight));
		grid[currentPos.x, currentPos.y] = 0;

		while (grid[currentPos.x, currentPos.y] != 0)
		{
			currentPos += directions[pseudoRandom.Next(0, 4)];
			currentPos.x = Mathf.Clamp(currentPos.x, 1, gridWidth - 2);
			currentPos.y = Mathf.Clamp(currentPos.y, 1, gridHeight - 2);
			grid[currentPos.x, currentPos.y] = 0;
		}
	}

	void Walk()
	{
		currentPos = new Vector2Int(pseudoRandom.Next(0, gridWidth), pseudoRandom.Next(0, gridHeight));
		grid[currentPos.x, currentPos.y] = 0;

		Vector2Int[] directions = {
			Vector2Int.up,
			Vector2Int.right,
			Vector2Int.down,
			Vector2Int.left
		};

		while (currentPos != startPos)
		{
			currentPos += directions[pseudoRandom.Next(0, 4)];
			currentPos.x = Mathf.Clamp(currentPos.x, 1, gridWidth - 2);
			currentPos.y = Mathf.Clamp(currentPos.y, 1, gridHeight - 2);
			grid[currentPos.x, currentPos.y] = 0;
		}
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
					Gizmos.DrawCube(pos, Vector3.one);
				}
			}
		}
	}

}

