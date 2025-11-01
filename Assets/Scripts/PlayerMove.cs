using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    public bool betterJump = false;
    public float fallMultipler = 0.5f;
    public float lowJumperMultipler = 1f;


    // Animaciones
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    Rigidbody2D rb2D;
    CheckGround checkGround;



    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        checkGround = GetComponentInChildren<CheckGround>();
    }

    


    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);

        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

        if (Input.GetKey("space") && checkGround != null && checkGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }
        if (checkGround.isGrounded==false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        if (checkGround.isGrounded == true)
        {
            animator.SetBool("Jump", false);
        }

        if (betterJump)
        {
            if (rb2D.velocity.y<0)
            {
                rb2D.velocity += Vector2.up*Physics2D.gravity.y*(fallMultipler)*Time.deltaTime;
            }
            if (rb2D.velocity.y > 0 && !Input.GetKey("space")) {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumperMultipler) * Time.deltaTime;
            }

        }
    }
}
