using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // VARIÁVEIS PRIVADAS
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float moveX;

    // VARIÁVEIS PÚBLICAS
    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float jumpForce;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        //moveX = Input.GetAxisRaw("Horizontal");
    }

    // Update is called once per frame
    void Update() {
        moveX = Input.GetAxisRaw("Horizontal");
        Flip();
    }

    void FixedUpdate() {
        Move();

        rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.linearVelocity.y);

        if (isGrounded == true) {
            addJumps = 1;
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        }
        else {
            if(Input.GetButtonDown("Jump") && addJumps > 0) {
                addJumps--;
                Jump();
            }
        }
    }

    void Move() {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }

    void Jump() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
            isGrounded = false;
        }
        
    }

    void Flip() {
        if (moveX > 0) {
            sprite.flipX = false; // Mantém o sprite normal
        } else if (moveX < 0) {
            sprite.flipX = true; // Inverte o sprite horizontalmente
        }
    }
}
