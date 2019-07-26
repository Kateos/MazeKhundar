using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGame : MonoBehaviour
{
    #region Public variables

    // To access from anywhere
    public static ManagerGame instance;

    // Reference to my player
    public GameObject MyPlayer;

    // Reference to my exit
    public GameObject MyExit;

    // Score of the player
    public int Score = 1;

    // Is the first game ?
    public bool IsFirstGame = true;

    // Size of the grid/maze
    public int xSize = 4;
    public int ySize = 7;

    // To know if the next ySize is +1 or +2
    public bool upAverage = false;

    #endregion

    #region Unity callbacks

    // To be sure there is only one ManagerGame
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    #endregion

    #region ManagerGame functions

    /// <summary>
    /// Launch victory scene
    /// Calculate the new size of the maze
    /// </summary>
    public void Victory()
    {
        if(IsFirstGame)
        {
            IsFirstGame = false;
        }
        GenerateNewSize();
        SceneManager.LoadScene("Victory");
    }

    /// <summary>
    /// Calculate the new size of the maze
    /// </summary>
    private void GenerateNewSize()
    {
        xSize++;
        if (!upAverage)
        {
            ySize++;
            upAverage = true;
        }
        else
        {
            ySize += 2;
            upAverage = false;
        }
    }

    #endregion
}
