using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkardWalk : MonoBehaviour
{
	public int gridWidth = 100;
	public int gridHeight = 100;
	public Vector3 postionOffset = new Vector3(-50, 0, 0);

	public string seed;
	public bool useRandomSeed = true;

	[Range(0, 100)]
	public int fillPercent = 50;
	private int fillAmount = 0;
	private Vector2Int currentPos;
	
	int[,] grid;
	System.Random pseudoRandom;
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
		fillAmount = 0;
		FillMap();
		Walk();
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

	void Walk()
	{
		currentPos = new Vector2Int(pseudoRandom.Next(1, gridWidth - 2), 
									pseudoRandom.Next(1, gridHeight - 2));
		grid[currentPos.x, currentPos.y] = 0;

		Vector2Int[] directions = {
			Vector2Int.up,
			Vector2Int.right,
			Vector2Int.down,
			Vector2Int.left
		};

		while(fillAmount < gridHeight * gridWidth * fillPercent / 100)
		{
			currentPos += directions[pseudoRandom.Next(0, 4)];
			currentPos.x = Mathf.Clamp(currentPos.x, 1, gridWidth - 2);
			currentPos.y = Mathf.Clamp(currentPos.y, 1, gridHeight - 2);
			if (grid[currentPos.x, currentPos.y] == 1)
			{
				grid[currentPos.x, currentPos.y] = 0;
				fillAmount++;
			}
		}
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
