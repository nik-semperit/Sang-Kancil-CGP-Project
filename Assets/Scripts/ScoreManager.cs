using UnityEngine;
using TMPro; // Needed for your ScoreText

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Makes it easy for fruits to find this script

    public TextMeshProUGUI scoreText;
    public int score = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "SCORE: " + score;
    }
}