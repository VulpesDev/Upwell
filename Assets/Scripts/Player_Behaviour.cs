using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Collections;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{
    #region Variables
    Rigidbody2D rb = null;

    AudioManager audio_man;

    [Header("Movement Settings")]
    public float    speed = 30.0f;
    private float   x_step = 0.0f;
    [Space(10)]


        #region Jump Settings
            ///////////////// Jump Settings ////////////////

    [Header("Jump Settings")]
    [SerializeField]    private float       jumpForce = 42.0f;
    [SerializeField]    private float       lowFall_Multiplier = 2.5f;
    [SerializeField]    private float       lowJum_Multiplier = 2f;
    [SerializeField]    private float       checkGroundRadius = 0.24f;
    [SerializeField]    private LayerMask   groundLayer;
    [SerializeField]    private float       coyoteeTime = 0.1f;
    [SerializeField]    private int         additionalJumps = 0;
                        private bool        isGrounded = false;
                        private Vector3     groundCheckPos = Vector3.zero;
                        private float       lastTimeGrounded;
                        private int         defaultAdditionalJumps;
    [Space(10)]
        #endregion


    
        #region Dash Settings
            /////////////// Dash Settings ////////////////
    [Header("Dash Settings")]
    [SerializeField]    private float   dash_multiplier = 4.0f;
    [SerializeField]    private float   dash_duration = 0.2f;
    [SerializeField]    private float   dash_delay = 0.5f;
                        private float   baseSpeed;
                        private bool isDashing = false, canDash = true;
    [Space(10)]
        #endregion



        #region Jet Pack Settings
            ///////////// Jet Pack Settings //////////
    [Header("Jet Pack Settings")]
    [SerializeField]    private float   jet_power = 0.25f;
    [SerializeField]    private float   jet_multiplier = 1;
    [SerializeField]    private float   jet_maxMultiplier = 100;
                        private float   jet_initMaxMultiplier = 0;
                        private float   jet_baseMultiplier;
                        private bool    jet_activated = false;
                        private short   jet_fuel = 0;
    
    [SerializeField][Range(0,100)]
                        private short   max_fuel = 100;
    [SerializeField][Range(0,100)]
                        private short   min_fuel = 0;

    [SerializeField]    private short   jet_fuel_discharge = 2;
    [SerializeField]    private short   jet_fuel_recharge = 1;
    [SerializeField]    private float   jet_recharge_delay = 1f;
                        private bool    jet_flag = false;
                        private bool    hyper_state = false;
    public AudioSource jetpack;
    float desiredEnginePitch = 0.5f;

    [Space(10)]
        #endregion

        
        
        #region Combat Settings
            ///////////// Combat Settings //////////
    [Header("Combat Settings")]
                        private Attack  attackManager = null;
    [SerializeField]    private float   delayColInvert = 0.2f;
    [SerializeField]    private int     damage = 100;
    [SerializeField]    private int     damage_on_fall = 100;

    Animator    anim;
        #endregion
#endregion


#region Getters and Setters
/// <summary>
/// ////////////////// GETTERS AND SETTERS //////////////////
/// </summary>

    public short getFuelValue() { return jet_fuel; }
    public short getMaxFuel() { return max_fuel; }
    public short getMinFuel() { return min_fuel; }
    
    public void setFuelValue(short value) { jet_fuel = value; }
    public void addFuelValue(short value) { jet_fuel += value; }

    /////////////////// END OF GETTERS AND SETTERS //////////////////   
    #endregion

    private void Awake()
    {
        audio_man = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    #region Start and Update
    ///////////////// Start and Update /////////////////
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        baseSpeed = speed;
        jet_baseMultiplier = jet_multiplier;
        additionalJumps = defaultAdditionalJumps;
        jet_initMaxMultiplier = jet_maxMultiplier;
        attackManager = GetComponent<Attack>();
        StartCoroutine(JetpackDelay());
    }

    /// <summary>
    /// Here is where all the runtime checks are done (frame by frame)
    /// Also checking for input and redirecting to other functions in fixed time
    /// (so it's cleaner and the pysics calculations are stable)
    /// </summary>
    void Update() {
        if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <=
            coyoteeTime || additionalJumps > 0)) {
            audio_man.play_sfx(audio_man.jump);
                Jump();
                jet_flag = true;
            }
        x_step = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump") && !isGrounded) {
            //here it's jetpack
            if (jet_fuel > min_fuel)
            {
                Jump();
            }
            jet_activated = true;
        }
        if (Input.GetButtonUp("Jump")) {
            //here it's end of using jetpack (it pushes the player up a bit, before stopping the jet)
            if (jet_fuel > min_fuel && jet_activated)
                Jump();
            jet_activated = false;
        }

        if (Input.GetButtonDown("Fire3")) {
            attackManager.AttackHit(damage, 0);
        }
        if (!isGrounded)
            update_engine();
        else
        {
            jetpack.volume = Mathf.Lerp(jetpack.volume, 0, Time.deltaTime * 10);
            jetpack.pitch = Mathf.Lerp(jetpack.pitch, 0.5f, Time.deltaTime * 1.5f);
        }
        if (!isGrounded && rb.velocity.y > 0) {
            anim.SetBool("fly", true);
            anim.SetBool("fall", false);

        }
        else if (!isGrounded && rb.velocity.y < 0) {
            anim.SetBool("fall", true);
            anim.SetBool("fly", false);

        }
        else {
            anim.SetBool("fly", false);
            anim.SetBool("fall", false);

        }
        
        CheckIfGrounded();
        DashCheck();
        CheckHyperState();
    }

    /// <summary>
    /// Physics calculations are made in the FixedUpdate and calculated 
    /// using the time between frames for stable calculations across different hardware
    /// </summary>
    void FixedUpdate() {
        Move();
        JumpVelocityControl();
        if (jet_activated && jet_fuel > min_fuel)
        {
            JetpackUpdate();
        }
        else
        {
            jet_multiplier = jet_baseMultiplier;
        }


    }
#endregion

    #region Basic Physics Movement
        //////////////// Basic Physics Movement //////////////////
    /// <summary>
    /// Applies horizontal movement
    /// </summary>
    void Move() {
        StartCoroutine(attackManager.SetInvertX((x_step < 0), delayColInvert, 0));
        rb.velocity = new Vector2(x_step * Time.deltaTime * 100, rb.velocity.y);
        if (rb.velocity.x < -0.1f)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else if (rb.velocity.x > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 0);

    }

    /// <summary>
    /// Applies vertical movement
    /// </summary>
    void Jump()  {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        additionalJumps--;
    }
    #endregion

    #region Passive Checks
        ////////////////// Passive Checks //////////////////
    /// <summary>
    /// Applies gravity to vertical movement
    /// </summary>
    void JumpVelocityControl() {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowFall_Multiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump")) {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJum_Multiplier - 1) * Time.deltaTime;
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
            Multiplier.setMultiplier(1);
            jet_flag = false;
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        } 
        else {
            if (isGrounded) {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
        if (attackManager.AttackHit(damage_on_fall, 1)) {
            Jump();
        }
    }

    void CheckHyperState() {
        
    }

    #endregion

    #region Dash
        //////////////// Dash //////////////////
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
    #endregion

    #region Jetpack
        //////////////// Jetpack //////////////////
    /// <summary>
    /// waits 0.1 seconds before charging jetpack
    /// </summary>
    /// <returns></returns>
    IEnumerator JetpackDelay() {
        yield return new WaitForSeconds(jet_recharge_delay);
        if (jet_fuel < max_fuel)
            jet_fuel += jet_fuel_recharge;
        StartCoroutine(JetpackDelay());
    }

    /// <summary>
    /// Applies jetpack force
    /// </summary>
    void JetpackUpdate() {
        if (jet_fuel >= min_fuel) {
            if (jet_fuel > max_fuel)
                jet_fuel = max_fuel;
            jet_fuel -= jet_fuel_discharge;
            //audio_man.play_sfx(audio_man.jetpackmid);
        }
        else
            jet_fuel = min_fuel;
        rb.velocity += Vector2.up * jet_power * jet_multiplier * Time.deltaTime * 100;
        if (jet_multiplier < jet_maxMultiplier)
            jet_multiplier += jet_baseMultiplier;
    }
    #endregion

    /// <summary>
    /// Gizmo for ground check
    /// </summary>
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPos, checkGroundRadius);
    }

    private void update_engine()
    {
        float velocityMagnitude = rb.velocity.y;
        float DesiredEngineVolume = velocityMagnitude * 0.5f;
        DesiredEngineVolume = Mathf.Clamp(DesiredEngineVolume, 0.2f, 0.1f);
        jetpack.volume = Mathf.Lerp(jetpack.volume, DesiredEngineVolume, Time.deltaTime * 10);
        //desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = 2.0f - (velocityMagnitude * 0.2f);
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 2.0f, 0.5f);
        jetpack.pitch = Mathf.Lerp(jetpack.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }
}
