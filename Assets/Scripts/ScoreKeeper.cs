using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreTxt;
    bool canScore = true;
    public void GainScore()
    {
        if (canScore)
        {
            score++;
            scoreTxt.text = "Score: " + score;
            canScore = false;
        }
    }
    public void ResetScore()
    {
        score = 0;
        scoreTxt.text = "Score: " + score;
        canScore = true;
    }
    public void ResetElligibility()
    {
        canScore = true;
    }
}
