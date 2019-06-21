using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControler : MonoBehaviour {
    public GameObject menuPanel;
    public GameObject pausePanel;
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI best;

    GameObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        pause();
    }



    public void pause() {
        score.gameObject.SetActive(false);
        bestScore.gameObject.SetActive(false);
        best.gameObject.SetActive(false);
        menuPanel.SetActive(true);
        pausePanel.SetActive(false);

        player.GetComponent<PlayerMovement>().isEnabled = false;
    }

    public void play() {
        score.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(true);
        best.gameObject.SetActive(true);
        menuPanel.SetActive(false);
        pausePanel.SetActive(true);

        getBestScore();

        StartCoroutine(wait());
    }

    public void restartBestScore() {
        PlayerPrefs.SetInt("bestScore", 0);
        getBestScore();
    }

    void getBestScore() {
        best.text = PlayerPrefs.GetInt("bestScore", 0).ToString();
    }

    public void exit() {
        if(int.Parse(score.text) > PlayerPrefs.GetInt("bestScore", 0)) {
            PlayerPrefs.SetInt("bestScore", int.Parse(score.text));
        }

        Application.Quit();
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerMovement>().isEnabled = true;
    }
}
