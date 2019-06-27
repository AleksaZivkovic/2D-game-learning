using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {
    public GameObject platform;
    public float yOffset = 2f;
    public float xLimit = 1.6f;
    public float xLimit1 = 1.6f;
    public float xLimit2 = -1.6f;
    public Vector2 offset = Vector2.zero;
    public float sec = 0.5f;

    List<GameObject> platforms;
    GameObject highestPlatform;
    GameObject secondHighestPlatform;
    GameObject lowestPlatform;
    ScoreManager scoreManager;
    PlayerMovement player;
    bool dead = false;

    void Start() {
        platforms = new List<GameObject>();
        player = FindObjectOfType<PlayerMovement>();
        scoreManager = FindObjectOfType<ScoreManager>();

        getHighest();
    }

    void Update() {
        getHighest();

        xLimit1 = player.transform.position.x + xLimit;
        xLimit2 = player.transform.position.x - xLimit;

        if(player.jumped) {
            CollisionInfo collisionInfo = highestPlatform.GetComponent<CollisionInfo>();
            if(collisionInfo.collided) {
                generatePlatform();
                collisionInfo.reset();
            } else if(player.transform.position.y > highestPlatform.transform.position.y + yOffset) {
                dead = true;
            }
        }

        if(player.transform.position.y < lowestPlatform.transform.position.y) {
            dead = true;
        }

        if(dead) {
            player.die();
        }
    }

    void generatePlatform() {
        Vector2 position = new Vector2(Random.Range(xLimit2, xLimit1), highestPlatform.transform.position.y + yOffset);
        Instantiate(platform, position, Quaternion.identity);
        scoreManager.addPoint();

        getHighest();
        highestPlatform.GetComponent<PlatformMovement>().setMovementBounds(xLimit1, xLimit2);
    }

    void getHighest() {
        fillPlatforms();
        GameObject[] platformArray = platforms.ToArray();
        float highest = float.MinValue;
        float lowest = float.MaxValue;

        foreach(GameObject platform in platformArray) {
            if(highest < platform.transform.position.y) {
                highest = platform.transform.position.y;
                highestPlatform = platform;
            }

            if(lowest > platform.transform.position.y) {
                lowest = platform.transform.position.y;
                lowestPlatform = platform;
            }
        }

        highest = float.MinValue;

        foreach(GameObject platform in platformArray) {
            if(highest < platform.transform.position.y && platform != highestPlatform) {
                highest = platform.transform.position.y;
                secondHighestPlatform = platform;
            }
        }

        Debug.DrawLine(lowestPlatform.transform.position, secondHighestPlatform.transform.position, Color.red);
        Debug.DrawLine(secondHighestPlatform.transform.position, highestPlatform.transform.position, Color.green);


        secondHighestPlatform.GetComponent<PlatformMovement>().moving = false;
        secondHighestPlatform.GetComponent<PlatformMovement>().first = false;

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
