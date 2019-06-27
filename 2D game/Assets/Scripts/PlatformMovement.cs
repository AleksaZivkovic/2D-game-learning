using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public float xLimit1;
    public float xLimit2;
    public float xLimit = 1.6f;
    public float speed = 5;
    [Range(0.001f, 1f)]
    public float powerUpChance = 0.02f;
    public bool moving = true;
    public bool hasPowerUp;
    public bool first = true;

    public Transform powerUpPlaceholder;
    public Transform left;
    public Transform right;
    public GameObject powerUp;

    bool added = false;
    int dir = 0;
    GameObject powerUpObj;
    CoinManager coinManager;
    PlayerMovement player;

    void Start() {
        coinManager = FindObjectOfType<CoinManager>();
        player = FindObjectOfType<PlayerMovement>();

        do {
            dir = Random.Range(-1, 1);
        } while(dir == 0);

        hasPowerUp = Random.Range(0f, 1f) <= powerUpChance;

        if(hasPowerUp) {
            powerUpObj = Instantiate(powerUp, powerUpPlaceholder.position, powerUpPlaceholder.rotation);
        }

        setMovementBounds(player.transform.position.x + xLimit, player.transform.position.x - xLimit);
    }

    void Update(){
        if(moving) {
            transform.position = new Vector3(transform.position.x + dir * speed * Time.deltaTime, transform.position.y, transform.position.z);

            if(transform.position.x > xLimit1) {
                dir = -1;
            } else if(transform.position.x < xLimit2) {
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

        if(!first) {
            setPlatformBounds();
        }
    }

    public void setPlatformBounds() {
        player.right = right.position.x;
        player.left = left.position.x;
    }

    public void setMovementBounds(float a, float b) {
        xLimit1 = a;
        xLimit2 = b;
    }
}
