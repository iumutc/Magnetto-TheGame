using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject hsButton;
    [SerializeField] GameObject playButton;
    [SerializeField] bool hsToggle;
    [SerializeField] GameObject highScoresTextField;
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnHighscoresButtonClicked()
    {
        FetchData();
        hsToggle = !hsToggle;

        if (hsToggle)
        {
            playButton.SetActive(false);
            hsButton.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0,80);
            highScoresTextField.SetActive(true);
        }
        else
        {
            playButton.SetActive(true);
            hsButton.GetComponent<RectTransform>().anchoredPosition += new Vector2(0,80);
            highScoresTextField.SetActive(false);
        }

    }

    public void FetchData()
    {
        TextMeshProUGUI textRef = highScoresTextField.GetComponent<TextMeshProUGUI>();
        textRef.text = "";
        float i = 1;
        while (PlayerPrefs.HasKey("Level " + i))
        {
            textRef.text += ("Level " + i + " Highest Score: " + PlayerPrefs.GetFloat("Level " + i).ToString("F2") + " \n");
            i++;
        }
    }
}