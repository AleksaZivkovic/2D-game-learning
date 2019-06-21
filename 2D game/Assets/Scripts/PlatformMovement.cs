using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour{
    public float speed = 200f;
    public List<GameObject> platforms;

    void Start(){
        platforms = new List<GameObject>();
        fillPlatforms();
    }

    void Update(){
        fillPlatforms();
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
