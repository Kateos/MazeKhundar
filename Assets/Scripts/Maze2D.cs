using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell2D
{
    public bool IsVisited = false;
    public GameObject Top; // 1
    public GameObject Right; // 2
    public GameObject Bottom; // 3
    public GameObject Left; // 4
    public Vector3 Center;
}

public class Maze2D : MonoBehaviour
{
    #region Public Variables

    public GameObject Wall;
    public GameObject Floor;
    public GameObject Player;
    public GameObject Exit;

    public float WallLength = 1.0f;

    public int xSize = 4;
    public int ySize = 7;

    #endregion

    #region Private variables

    private Vector3 initialPos;

    private GameObject wallHolder;
    private GameObject floorHolder;

    private Cell[] cells;

    private List<int> lastCells;

    private int currentCell = 0;
    private int totalCells;
    private int visitedCells = 0;
    private int currentNeighbour = 0;
    private int backingUp = 0;
    private int wallToBreak = 0;

    private bool startedBuilding = false;

    #endregion

    #region Unity callbacks

    // Start is called before the first frame update
    void Start()
    {
        CreateWalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Other functions

    /// <summary>
    ///  Instantiate walls depending on xSize and ySize
    /// </summary>
    void CreateWalls()
    {
        // Create a holder for walls
        wallHolder = new GameObject();
        wallHolder.name = "Maze Walls";

        // Find the location of the first cell with the center of the maze in (0,0,0)
        initialPos = new Vector3((-xSize / 2) + WallLength / 2, 0.0f, (-ySize / 2) + WallLength / 2);
        Vector3 currentPos = initialPos;

        // Used to attribute parent
        GameObject tempWall;

        // For X Axis - Vertical walls
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                currentPos = new Vector3(initialPos.x + (j * WallLength) - WallLength / 2, 0.0f, initialPos.z + (i * WallLength) - WallLength / 2);
                tempWall = Instantiate(Wall, currentPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }

        // For Y Axis - Horizontal walls
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                currentPos = new Vector3(initialPos.x + (j * WallLength), 0.0f, initialPos.z + (i * WallLength) - WallLength);
                tempWall = Instantiate(Wall, currentPos, Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f))) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }

        CreateCells();
    }

    /// <summary>
    /// Determinate cells by separate walls in 4 position: Top, Right, Bottom, Left
    /// </summary>
    void CreateCells()
    {
        // Initialization
        int verticalWalls = 0;
        int currentLeftWall = 0;
        int numberOfWallsOnARow = 0;
        int numberOfWalls = wallHolder.transform.childCount;
        GameObject[] allWalls = new GameObject[numberOfWalls];
        totalCells = xSize * ySize;
        cells = new Cell[totalCells];

        // Create a holder for floors
        floorHolder = new GameObject();
        floorHolder.name = "Maze Floors";

        // Get all walls
        for (int i = 0; i < numberOfWalls; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        // Assigns walls to the cells by sorting the walls in the wallHolder
        for (int cell = 0; cell < cells.Length; cell++)
        {
            if (numberOfWallsOnARow == xSize)
            {
                verticalWalls++;
                numberOfWallsOnARow = 0;
            }

            cells[cell] = new Cell();
            cells[cell].Left = allWalls[verticalWalls];
            cells[cell].Bottom = allWalls[currentLeftWall + (xSize + 1) * ySize];

            verticalWalls++;
            numberOfWallsOnARow++;

            cells[cell].Right = allWalls[verticalWalls];
            cells[cell].Top = allWalls[(currentLeftWall + (xSize + 1) * ySize) + xSize];
            cells[cell].Center = cells[cell].Top.transform.position - new Vector3(0.0f, 0.0f, .5f);

            Instantiate(Floor, cells[cell].Center - new Vector3(0.0f, .5f, 0.0f), Quaternion.identity, floorHolder.transform);

            currentLeftWall++;
        }

        CreateMaze();
    }

    /// <summary>
    /// Create maze by checking if a cell has been visited, if not go in and break a wall
    /// Use depth first search system
    /// </summary>
    void CreateMaze()
    {
        // If we need to back to the last cell because the current is already visited
        lastCells = new List<int>();
        lastCells.Clear();

        while (visitedCells < totalCells)
        {
            if(startedBuilding)
            {
                // Check neighbours of the current cell and choose one
                SelectNeighbour();
                if(!cells[currentNeighbour].IsVisited && cells[currentCell].IsVisited)
                {
                    BreakWall();
                    cells[currentNeighbour].IsVisited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbour;
                    if(lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
                
            }
            else
            {
                // Define randomly the first cell to start the maze
                currentCell = Random.Range(0, totalCells);
                cells[currentCell].IsVisited = true;
                visitedCells++;
                startedBuilding = true;
            }
        }

        Instantiate(Player, cells[Random.Range(cells.Length - xSize, cells.Length)].Center, Quaternion.Euler(90f, 0.0f, 0.0f));
        Instantiate(Exit, cells[Random.Range(0, xSize)].Center, Quaternion.Euler(90f, 0.0f, 0.0f));
    }

    /// <summary>
    /// Check neighbours of a cell and select one with the wall to destroy
    /// If not, go back to the previous cell and try another way
    /// </summary>
    void SelectNeighbour()
    {
        // Initialization
        int neighbourAmount = 0;
        int[] neighbours = new int[4];
        
        // 1 = Top / 2 = Right / 3 = Bottom / 4 = Left
        int[] connectedwall = new int[4]; 

        // Top
        if (currentCell + xSize < totalCells)
        {
            if(!cells[currentCell + xSize].IsVisited)
            {
                neighbours[neighbourAmount] = currentCell + xSize;
                connectedwall[neighbourAmount] = 1;
                neighbourAmount++;
            }
        }

        // Right
        if(!((currentCell + 1) % xSize == 0))
        {
            if(!cells[currentCell + 1].IsVisited)
            {
                neighbours[neighbourAmount] = currentCell + 1;
                connectedwall[neighbourAmount] = 2;
                neighbourAmount++;

            }
        }

        // Bottom
        if (currentCell - xSize >= 0)
        {
            if (!cells[currentCell - xSize].IsVisited)
            {
                neighbours[neighbourAmount] = currentCell - xSize;
                connectedwall[neighbourAmount] = 3;
                neighbourAmount++;
            }
        }

        // Left
        if (!(currentCell % xSize == 0))
        {
            if (!cells[currentCell - 1].IsVisited)
            {
                neighbours[neighbourAmount] = currentCell - 1;
                connectedwall[neighbourAmount] = 4;
                neighbourAmount++;
            }
        }

        if(neighbourAmount != 0)
        {
            int selectedNeighbour = Random.Range(0, neighbourAmount);
            currentNeighbour = neighbours[selectedNeighbour];
            wallToBreak = connectedwall[selectedNeighbour];
        }
        else
        {
            if(backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    /// <summary>
    /// Break the wall selected
    /// </summary>
    void BreakWall()
    {
        switch (wallToBreak)
        {
            case 1:
                cells[currentCell].Top.gameObject.SetActive(false);
                break;
            case 2:
                cells[currentCell].Right.gameObject.SetActive(false);
                break;
            case 3:
                cells[currentCell].Bottom.gameObject.SetActive(false);
                break;
            case 4:
                cells[currentCell].Left.gameObject.SetActive(false);
                break;
        }
    }
    #endregion
}
