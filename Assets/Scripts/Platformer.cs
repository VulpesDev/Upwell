using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Collections;
using UnityEngine;

public class Platformer : MonoBehaviour
{
    Rigidbody2D rb = null;

    [Header("Movement Settings")]
    public float speed;

    [Space(10)]

    [Header("Jump Settings")]
    public float jumpForce;
    public float falldash_Multiplier = 2.5f;
    public float lowJumpdash_Multiplier = 2f;
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
    public float dash_multiplier;
    public float dash_duration, baseSpeed, dash_delay;
    bool isDashing = false, canDash = true;

    [Space(10)]
    [Header("Jet Pack Settings")]
    public float jet_power;
    public float jet_multiplier, jet_maxMultiplier;
    private float jet_baseMultiplier;
    private bool jet_activated = false;
    private sbyte jet_fuel = 0;
    public sbyte getFuelValue() { return jet_fuel; }
    [SerializeField][Range(0,100)]
    private sbyte max_fuel = 100;
    public sbyte getMaxFuel() { return max_fuel; }
    [SerializeField][Range(0,100)]
    private sbyte min_fuel = 0;
    public sbyte getMinFuel() { return min_fuel; }

    [SerializeField]
    private sbyte jet_fuel_discharge = 2;
    [SerializeField]
    private sbyte jet_fuel_recharge = 1;


    private float x_step = 0.0f;


    

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        baseSpeed = speed;
        jet_baseMultiplier = jet_multiplier;
        additionalJumps = defaultAdditionalJumps;
        StartCoroutine(JetpackDelay());
    }

    /// <summary>
    /// Here is where all the runtime checks are done (frame by frame)
    /// Also checking for input and redirecting to other functions
    /// (so it's cleaner and the pysics calculations are stable)
    /// </summary>
    void Update() {
        if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <=
            coyoteeTime || additionalJumps > 0))
                Jump();
        x_step = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime * 100;
        if (Input.GetButtonDown("Fire2")) {
            if (jet_fuel > min_fuel)
                Jump();
            jet_activated = true;
        }
        if (Input.GetButtonUp("Fire2")) {
            if (jet_fuel > min_fuel)
                Jump();
            jet_activated = false;
        }
        CheckIfGrounded();
        DashCheck();
    }

    /// <summary>
    /// Physics calculations are made in the FixedUpdate and calculated 
    /// using the time between frames for stable calculations across different hardware
    /// </summary>
    void FixedUpdate() {
        Move();
        JumpVelocityControl();
        if (jet_activated && jet_fuel > min_fuel)
            JetpackUpdate();
        else
            jet_multiplier = jet_baseMultiplier;
        

    }

    /// <summary>
    /// Applies horizontal movement
    /// </summary>
    void Move() {
        rb.velocity = new Vector2(x_step, rb.velocity.y);
    }

    /// <summary>
    /// Applies vertical movement
    /// </summary>
    void Jump()  {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        additionalJumps--;
    }

    /// <summary>
    /// Applies gravity to vertical movement
    /// </summary>
    void JumpVelocityControl() {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity * (falldash_Multiplier - 1) * Time.deltaTime;
        } 
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpdash_Multiplier - 1) * Time.deltaTime;
        }   
    }

    /// <summary>
    /// Checks if the player is on the ground
    /// </summary>
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

    /// <summary>
    /// Checks if the player can dash
    /// </summary>
    void DashCheck() {
        float fire1 = Input.GetAxisRaw("Fire1");
        if(fire1 >= 0.1f && canDash) {
            DashStart();
        }
    }

    /// <summary>
    /// Dashes the player
    /// </summary>
    void DashStart() {
        isDashing = true;
        canDash = false;
        speed *= dash_multiplier;
        Invoke("DashStop", dash_duration);
    }

    /// <summary>
    /// Stops dashing
    /// </summary>
    void DashStop() {
        speed = baseSpeed;
        isDashing = false;
        Invoke("DashDelay", dash_delay);
    }

    /// <summary>
    /// Waits for dash delay
    /// </summary>
    void DashDelay() {
        canDash = true;
    }

    /// <summary>
    /// waits 0.1 seconds before charging jetpack
    /// </summary>
    /// <returns></returns>
    IEnumerator JetpackDelay() {
        yield return new WaitForSeconds(0.1f);
        if (jet_fuel < max_fuel)
            jet_fuel += jet_fuel_recharge;
        StartCoroutine(JetpackDelay());
    }

    /// <summary>
    /// Applies jetpack force
    /// </summary>
    void JetpackUpdate() {
        if (jet_fuel - jet_fuel_discharge >= 0)
            jet_fuel -= jet_fuel_discharge;
        rb.velocity += Vector2.up * jet_power * jet_multiplier * Time.deltaTime * 100;
        if (jet_multiplier < jet_maxMultiplier)
            jet_multiplier += jet_baseMultiplier;
    }

    /// <summary>
    /// Gizmo for ground check
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos, checkGroundRadius);
    }
}
