using UnityEngine;
using UnityEditor;

public class maze_renderer : MonoBehaviour
{
    [SerializeField] maze_generator generator;
    [SerializeField] GameObject cellPrefab;
    public float CellSize = 4f;
    
    [ContextMenu("Generate Maze")]
    public void GenerateMaze()
    {
        // Clear existing children
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        maze_cell[,] maze = generator.GetMaze();

        for (int x = 0; x < generator.mazeWidth; x++)
        {
            for (int y = 0; y < generator.mazeHeight; y++)
            {
                Vector3 position = new Vector3(x * CellSize, 0, y * CellSize);
                GameObject newCell = (GameObject)PrefabUtility.InstantiatePrefab(cellPrefab);
                newCell.transform.position = position;
                newCell.transform.parent = transform;

                maze_cell_object mazeCell = newCell.GetComponent<maze_cell_object>();

                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;

                bool right = false;
                bool bottom = false;

                if (x == generator.mazeWidth - 1)
                {
                    right = true;
                }
                if (y == 0)
                {
                    bottom = true;
                }
                mazeCell.Init(top, bottom, right, left);       
            }
        }
    }
}
