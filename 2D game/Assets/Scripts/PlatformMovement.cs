using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public float xLimit;
    public float speed = 5;
    [Range(0.001f, 1f)]
    public float powerUpChance = 0.02f;
    public bool moving = true;
    public bool hasPowerUp;

    public Transform powerUpPlaceholder;
    public GameObject powerUp;

    bool added = false;
    int dir = 0;
    GameObject powerUpObj;
    CoinManager coinManager;

    void Start() {
        coinManager = FindObjectOfType<CoinManager>();

        do {
            dir = Random.Range(-1, 1);
        } while(dir == 0);

        hasPowerUp = Random.Range(0f, 1f) <= powerUpChance;

        if(hasPowerUp) {
            powerUpObj = Instantiate(powerUp, powerUpPlaceholder.position, powerUpPlaceholder.rotation);
        }
    }

    void Update(){
        if(moving) {
            transform.position = new Vector3(transform.position.x + dir * speed * Time.deltaTime, transform.position.y, transform.position.z);

            if(transform.position.x > xLimit) {
                dir = -1;
            } else if(transform.position.x < -xLimit) {
                dir = 1;
            }

            if(hasPowerUp) {
                powerUpObj.transform.position = powerUpPlaceholder.position;
            }
        } else {
            if(hasPowerUp && !added) {
                added = true;
                Destroy(powerUpObj);
                coinManager.add(1);
            }
        }

    }
}
