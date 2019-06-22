using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {
    public int diamonds;
    public int multiplyer = 1;

    UIControler uiCotroler;

    void Start() {
        diamonds = PlayerPrefs.GetInt("diamonds", 0);
        uiCotroler = FindObjectOfType<UIControler>();
    }

    void LateUpdate() {
        diamonds = PlayerPrefs.GetInt("diamonds", 0);
        uiCotroler.setDiamonds(diamonds);
    }

    public void add(int ammount) {
        diamonds += ammount * multiplyer;
        PlayerPrefs.SetInt("diamonds", diamonds);
    }

    public void remove(int ammount) {
        diamonds -= ammount;
        PlayerPrefs.SetInt("diamonds", diamonds);
    }
}
