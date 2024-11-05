using UnityEngine;

public class PlayerMovment : MonoBehaviour {
    [Header("Player Movement Settings")]
    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask scaffoldLayer;

    [Header("Coyote Time")]
    [SerializeField]private float coyoteTime;
    [Header("Multiple Jumps")]
    [SerializeField]private int jumps;
    private int jumpCounter;
    private float coyoteCounter;
    [Header("Wall Jumping")]
    [SerializeField]private float wallJumpX;
    [SerializeField]private float wallJumpY;

    [SerializeField] private float jumpCooldownTime; // Cooldown duration in seconds
    private float jumpCooldown = 0;



    [Header("Sound Settings")]
    [SerializeField]private  AudioClip jumpSound;


    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private void Awake() {
        // Grab references to the components
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {

        horizontalInput = Input.GetAxis("Horizontal");

        if(jumpCooldown > 0) {
            jumpCooldown -= Time.deltaTime;
        }

        // Flip the player sprite based on the direction of movement
        if(horizontalInput > 0.01f) {
            transform.localScale = Vector3.one;
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Update the animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded() && !onScaffold());
        anim.SetBool("wall", onScaffold());

        

        if (onScaffold()  && !isGrounded() && horizontalInput != 0) {
            body.gravityScale = 5;
            body.velocity = Vector2.zero;
        } else {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        if(isGrounded() || onScaffold()) {
            coyoteCounter = coyoteTime;
            jumpCounter = jumps;
        } else {
            coyoteCounter -= Time.deltaTime;
        }
        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space) && jumpCooldown <= 0) {
            Jump();
            jumpCooldown = jumpCooldownTime;
        }
        //  Adjustable jump height
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y /  2);
        }
    }

    private void Jump() {
        if (jumpCounter <= 0 || coyoteCounter <= 0) return;

        SoundManager.instance.PlaySound(jumpSound);
        if (onScaffold()) {
            WallJump();
        } else {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        jumpCounter--;
        coyoteCounter = 0;
    }

    private void WallJump() {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onScaffold(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, scaffoldLayer);
        return raycastHit.collider != null;
    }
}
