using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    Rigidbody2D rb;
    Animator animator;
    ScoreManager scoreManager;
    UIControler uIControler;
    bool isAbleToJump = true;
    bool moving = true;
    int dir = 0;

    public bool isEnabled = true;
    public bool jumped = false;
    public float jumpSpeed = 100f;
    public float speed = 2.5f;
    public float offset = 0.5f;
    public Transform left;
    public Transform right;

    void Start() {
        do {
            dir = Random.Range(-1, 1);
        } while(dir == 0);

        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>();
        uIControler = FindObjectOfType<UIControler>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(isEnabled) {
            if(isAbleToJump) {
                if(Input.GetKeyDown(KeyCode.Mouse0)) {
                    moving = false;
                    animator.SetBool("readyForJump", true);
                }

                if(Input.GetKeyUp(KeyCode.Mouse0)) {
                    moving = false;
                    animator.SetBool("isJumping", true);
                    rb.AddForce(new Vector2(0, jumpSpeed));
                    isAbleToJump = false;
                    jumped = true;
                }

                if(moving) {
                    transform.position = new Vector3(transform.position.x + dir * speed * Time.deltaTime, transform.position.y, transform.position.z);

                    if(transform.position.x > right.position.x - offset) {
                        dir = -1;
                    } else if(transform.position.x < left.position.x + offset) {
                        dir = 1;
                    }
                }
            }

            if(Mathf.Abs(transform.rotation.z * Mathf.Rad2Deg) >= 40) {
                die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("ground")) {
            isAbleToJump = true;
            jumped = false;
            moving = true;
            animator.SetBool("readyForJump", false);
            animator.SetBool("isJumping", false);
        }
    }

    public void die() {
        isEnabled = false;
        scoreManager.died();
        uIControler.died();
    }
}
