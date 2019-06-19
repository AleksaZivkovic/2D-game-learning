using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    Rigidbody2D rb;
    bool isAbleToJump = true;

    public Animator animator;
    public float jumpSpeed = 100f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(isAbleToJump) {
                animator.SetBool("readyForJump", true);
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)) {
            if(isAbleToJump) {
                animator.SetBool("isJumping", true);
                rb.AddForce(new Vector2(0, jumpSpeed));
                isAbleToJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.collider.tag);
        if(collision.collider.tag == "ground") {
            isAbleToJump = true;
            animator.SetBool("readyForJump", false);
            animator.SetBool("isJumping", false);
        }
    }
}
