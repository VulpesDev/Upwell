using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Collections;
using UnityEngine;

public class Platformer : MonoBehaviour
{
    Rigidbody2D rb = null;
    // Manager mngr;

    [Header("Movement Settings")]
    public float speed;

    [Space(10)]

    [Header("Jump Settings")]
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool isGrounded = false;
    private Vector3 groundCheckPos = Vector3.zero;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float coyoteeTime;
    float lastTimeGrounded;
    public int defaultAdditionalJumps;
    int additionalJumps;
    
    [Space(10)]
    
    [Header("Dash Settings")]
    public float multiplier;
    public float duration, baseSpeed, delay;
    bool isDashing = false, canDash = true;

    private float x_step = 0.0f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        baseSpeed = speed;
        additionalJumps = defaultAdditionalJumps;

        // GameObject manager = Instantiate(Resources.Load<GameObject>("Manager"));
        // mngr = manager.GetComponent<Manager>();
        
    }
    void Update() {
        Jump();
        x_step = Input.GetAxisRaw("Horizontal") * speed;
        CheckIfGrounded();
        DashCheck();
    }
    void FixedUpdate() {
        Move();
        JumpVelocityControl();
    }

    void Move() {
        rb.velocity = new Vector2(x_step, rb.velocity.y);
    }
    void Jump()  {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <=
            coyoteeTime || additionalJumps > 0)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            additionalJumps--;
        }
    }
    void JumpVelocityControl() {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        } 
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }   
    }
    void CheckIfGrounded() {
        groundCheckPos = new Vector2(transform.position.x, transform.position.y - transform.lossyScale.y);
        Collider2D colliders = Physics2D.OverlapCircle(groundCheckPos, checkGroundRadius,
            groundLayer);

        if (colliders != null) {
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        } 
        else {
            if (isGrounded) {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    void DashCheck() {
        float fire1 = Input.GetAxisRaw("Fire1");
        if(fire1 >= 0.1f && canDash) {
            DashStart();
        }
    }
    void DashStart() {
        isDashing = true;
        canDash = false;
        speed *= multiplier;
        Invoke("DashStop", duration);
    }
    void DashStop() {
        speed = baseSpeed;
        isDashing = false;
        Invoke("DashDelay", delay);
    }
    void DashDelay() {
        canDash = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos, checkGroundRadius);
    }
}
