using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    Rigidbody2D rb;
    bool isAbleToJump = true;
    Animator animator;
    ScoreManager scoreManager;
    UIControler uIControler;

    public bool isEnabled = true;
    public bool jumped = false;
    public float jumpSpeed = 100f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>();
        uIControler = FindObjectOfType<UIControler>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(isEnabled) {
            if(Input.GetKeyDown(KeyCode.Mouse0)) {
                if(isAbleToJump) {
                    animator.SetBool("readyForJump", true);
                }
            }

            if(Input.GetKeyUp(KeyCode.Mouse0)) {
                if(isAbleToJump) {
                    animator.SetBool("isJumping", true);
                    rb.AddForce(new Vector2(0, jumpSpeed));
                    isAbleToJump = false;
                    jumped = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("ground")) {
            isAbleToJump = true;
            jumped = false;
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
