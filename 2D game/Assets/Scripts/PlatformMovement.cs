using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    public float xLimit;
    public float speed = 5;
    public bool moving = true;
    int dir = 0;
    bool done = true;

    void Start() {
        dir = Random.Range(-1, 1);
    }

    void Update(){
        if(moving) {
            if(done) {
                dir *= -1;
                done = false;
            }

            transform.position = new Vector3(transform.position.x + dir * speed * Time.deltaTime, transform.position.y, transform.position.z);

            if(Mathf.Abs(transform.position.x) > xLimit) {
                done = true;
            }
        }
    }
}
