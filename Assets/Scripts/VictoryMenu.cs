using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryMenu : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public Slider ScoreSlider;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = (ManagerGame.instance.Score - 1).ToString();
        StartCoroutine(Co_FillSlider());
    }

    IEnumerator Co_FillSlider()
    {
        yield return new WaitForSeconds(0.5f);
        while (ScoreSlider.value < 1)
        {
            ScoreSlider.value += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        ManagerGame.instance.Score++;
        ScoreText.text = (ManagerGame.instance.Score - 1).ToString();
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnHomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
