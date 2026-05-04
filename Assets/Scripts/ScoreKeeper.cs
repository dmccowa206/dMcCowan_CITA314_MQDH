using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreTxt;
    public void GainScore()
    {
        score++;
        scoreTxt.text = "Score: " + score;
    }
    public void ResetScore()
    {
        score = 0;
        scoreTxt.text = "Score: " + score;
    }
}
