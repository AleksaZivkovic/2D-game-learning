using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInfo : MonoBehaviour {
    public bool collided = false;
    public float sec = 0.5f;

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player") &&
           collision.gameObject.transform.position.y < gameObject.transform.position.y) {
            GetComponent<BoxCollider2D>().enabled = false;
            collided = true;
            StartCoroutine(turnOn());
        }
    }

    IEnumerator turnOn() {
        yield return new WaitForSeconds(sec);
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void reset() {
        collided = false;
    }
}
