using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject endLevelUI;
    public TextMeshProUGUI timeSpentText;
    private float startTime;
    private float timer;
    private bool levelCompleted;

    void Start()
    {
        timer = 0f;
        startTime = Time.time;
        endLevelUI.SetActive(false);
        levelCompleted = false;
    }

    void Update()
    {
        if (levelCompleted)
            return;
        timer += Time.deltaTime;
        timeSpentText.text = "Time: " + timer.ToString("F2");
    }

    public void OnReachFinalSpot()
    {
        bool beatHighScore = false;
        float tmpHS = 0;
        levelCompleted = true;
        float timeSpent = Time.time - startTime;
        endLevelUI.SetActive(true);
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            if (PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name) > timeSpent)
            {
                beatHighScore = true;
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, timeSpent);
                
            }
            else
            {
                tmpHS = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
            }
        } 
        else
        {
            beatHighScore = true;
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, timeSpent);

        }
        timeSpentText.text = "Time Spent: " + timeSpent.ToString("F2") + " seconds \n";
        if (beatHighScore) timeSpentText.text += "NEW HIGHSCORE";
        else { timeSpentText.text += "HIGHSCORE: " + tmpHS.ToString(); }
        Time.timeScale = 0;
    }

    public void OnNextLevelButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}