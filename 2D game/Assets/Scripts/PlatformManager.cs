using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {
    public GameObject platform;
    public float yOffset = 2f;
    public float xLimit = 3f;
    public Vector2 offset = Vector2.zero;
    public float sec = 0.5f;

    List<GameObject> platforms;
    GameObject highestPlatform;
    GameObject secondHighestPlatform;
    ScoreManager scoreManager;
    PlayerMovement player;
    bool dead = false;
    bool doneChecking = true;

    void Start() {
        platforms = new List<GameObject>();
        player = FindObjectOfType<PlayerMovement>();
        scoreManager = FindObjectOfType<ScoreManager>();
        fillPlatforms();
    }

    void Update() {
        getHighest();
        if(player.jumped) {
            CollisionInfo collisionInfo = highestPlatform.GetComponent<CollisionInfo>();
            if(collisionInfo.collided) {
                generatePlatform();
                collisionInfo.reset();
            } else if(player.transform.position.y > highestPlatform.transform.position.y) {
                dead = true;
                Debug.Log("Not collided");
            }
        }

        if(doneChecking) {
            StartCoroutine(checkUnder());
        }

        if(dead) {
            player.die();
        }
    }

    IEnumerator checkUnder() {
        doneChecking = false;
        Debug.Log("Check 1");
        yield return new WaitForSeconds(sec);

        Debug.Log("Check 2");
        if(player.transform.position.y < secondHighestPlatform.transform.position.y) {
            dead = true;
            Debug.Log("Under second highest");
        }
        doneChecking = true;
    }

    void generatePlatform() {
        Vector2 position = new Vector2(Random.Range(-xLimit, xLimit), highestPlatform.transform.position.y + yOffset);
        Instantiate(platform, position, Quaternion.identity);
        scoreManager.addPoint();
    }

    void getHighest() {
        fillPlatforms();
        GameObject[] platformArray = platforms.ToArray();
        float highest = float.MinValue;

        foreach(GameObject platform in platformArray) {
            if(highest < platform.transform.position.y) {
                highest = platform.transform.position.y;
                highestPlatform = platform;
            }
        }

        highest = float.MinValue;

        foreach(GameObject platform in platformArray) {
            if(highest < platform.transform.position.y && platform != highestPlatform) {
                highest = platform.transform.position.y;
                secondHighestPlatform = platform;
            }
        }

        secondHighestPlatform.GetComponent<PlatformMovement>().moving = false;
        highestPlatform.GetComponent<PlatformMovement>().moving = true;
        StartCoroutine(removePlatforms());
    }

    IEnumerator removePlatforms() {
        GameObject[] platformArray = platforms.ToArray();

        foreach(GameObject platform in platformArray) {
            if(platform != highestPlatform && platform != secondHighestPlatform) {
                yield return new WaitForSeconds(sec);
                Destroy(platform);
            }
        }
    }

    void fillPlatforms() {
        platforms.Clear();
        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));

        foreach(GameObject obj in gameObjects) {
            if(obj.CompareTag("ground")) {
                platforms.Add(obj);
            }
        }
    }
}
