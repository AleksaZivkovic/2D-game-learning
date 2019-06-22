using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;

    public void addPoint() {
        int points = int.Parse(score.text) + 1;
        score.text = points.ToString();

        if(PlayerPrefs.GetInt("bestScore", 0) < points) {
            PlayerPrefs.SetInt("bestScore", points);
        }

        bestScore.text = PlayerPrefs.GetInt("bestScore", 0).ToString();
    }

    public void died() {
        int points = int.Parse(score.text);
        score.text = points.ToString();

        if(PlayerPrefs.GetInt("bestScore", 0) < points) {
            PlayerPrefs.SetInt("bestScore", points);
        }

        bestScore.text = PlayerPrefs.GetInt("bestScore", 0).ToString();
    }
}
