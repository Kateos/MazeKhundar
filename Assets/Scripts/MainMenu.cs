using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    #region Public variables

    // Reference to the text of the score
    public TextMeshProUGUI ScoreText;

    #endregion

    #region Unity callbacks

    private void Start()
    {
        DisplayScore();
    }

    #endregion

    #region MainMenu functions

    /// <summary>
    /// Display the score in the star
    /// </summary>
    public void DisplayScore()
    {
        ScoreText.text = (ManagerGame.instance.Score - 1).ToString();
    }

    /// <summary>
    /// Load the game
    /// </summary>
    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Exit the game
    /// </summary>
    public void ExitGameButton()
    {
        Application.Quit();
    }

    #endregion
}
