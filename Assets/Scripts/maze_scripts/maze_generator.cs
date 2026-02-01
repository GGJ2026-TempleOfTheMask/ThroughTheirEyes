using System.Collections.Generic;
using UnityEngine;

public class maze_generator : MonoBehaviour
{
    [Range(5, 50)]
    public int mazeWidth = 10, mazeHeight = 10; //Dimensions of the maze
    public int startX, startY;  //Starting position in the maze
    maze_cell[,] maze;  //2D array to hold the maze cells
    Vector2Int currentCell; //Current cell position during generation

    public maze_cell[,] GetMaze()
    {
        maze = new maze_cell[mazeWidth, mazeHeight];
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new maze_cell(x, y);
            }
        }
        CarvePath(startX, startY);
        return maze;
    }

    List<Direction> directions = new List<Direction> {Direction.Up, Direction.Down, Direction.Left, Direction.Right};

    List<Direction> GetRandomDirections()
    {
        List<Direction> dir = new List<Direction>(directions);

        List<Direction> randomDirs = new List<Direction>();

        while (dir.Count > 0)
        {
            int rnd = Random.Range(0, dir.Count);
            randomDirs.Add(dir[rnd]);
            dir.RemoveAt(rnd);
        }
        return randomDirs;
    }

    bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || maze[x, y].visited) return false;
        else return true;
    }

    Vector2Int CheckNeighbour()
    {
        List<Direction> rndDir = GetRandomDirections();
        for (int i = 0; i < rndDir.Count; i++) {
            Vector2Int neighbour = currentCell;
            switch (rndDir[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
            }

            if (IsCellValid(neighbour.x, neighbour.y))
            {
                return neighbour;
            }
        }
        return currentCell;
    }

    void BreakWalls (Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x)
        {
            maze[primaryCell.x, primaryCell.y].leftWall = false;
        } else if (primaryCell.x < secondaryCell.x) {
            maze[secondaryCell.x, secondaryCell.y].leftWall = false;
        } else if (primaryCell.y > secondaryCell.y) {
            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        } else if (primaryCell.y < secondaryCell.y) {
            maze[primaryCell.x, primaryCell.y].topWall = false;
        }
    }

    void CarvePath (int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1)
        {
            x = y= 0;
        }

        currentCell = new Vector2Int(x, y);

        List<Vector2Int> path = new List<Vector2Int>();

        bool deadEnd = false;
        while (!deadEnd)
        {
            Vector2Int nextCell = CheckNeighbour();
            if (nextCell == currentCell)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbour();

                    if (nextCell != currentCell)
                    {
                        break;
                    }
                }

                if (nextCell == currentCell)
                {
                    deadEnd = true;
                }

            } else
            {
                BreakWalls(currentCell, nextCell);
                maze[currentCell.x, currentCell.y].visited = true;
                currentCell = nextCell;
                path.Add(currentCell);
            }
        }
    }
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class maze_cell
{
    public bool visited = false;
    public int x, y;
    public bool topWall, leftWall;

    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }

    public maze_cell(int x, int y)
    {
        this.x = x;
        this.y = y;

        visited = false;

        topWall = leftWall = true;
    }   
}
