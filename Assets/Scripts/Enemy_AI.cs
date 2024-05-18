using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ai : MonoBehaviour
{
    // look for player
    [Range(1, 15)]
    [SerializeField]
    private float view_radius = 5;
    [SerializeField]
    private float detection_check_delay = 0.1f;
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private LayerMask player_layermask;
    [SerializeField]
    private LayerMask visibility_layer;

    [field: SerializeField]
    public bool target_visible { get; private set; }

    public Transform detected
    {
        get => target;
        set
        {
            target = value;
            target_visible = false;
        }
    }

    // chasing
    public bool is_chasing;
    private Transform player_transform;

    // patrol
    private float speed = 5;
    private bool move_right = true;
    public Transform ground_detection;
    public Transform wall_detection;

    void Start()
    {
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(detection_delay());
    }

    // Update is called once per frame
    void Update()
    {
        if (detected != null)
            target_visible = check_target_visible();
        if (target_visible)
            is_chasing = true;
        else
            is_chasing = false;
        if (is_chasing)
        {
            rotate_towards_player();
        }
        else
        {
            enemy_patrol();
        }
        
    }

    // idle state
    private void enemy_patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground_info = Physics2D.Raycast(ground_detection.position, Vector2.down, 5.0f, LayerMask.GetMask("Ground"));
        RaycastHit2D wall_info = Physics2D.Raycast(wall_detection.position, transform.right, 0.2f, LayerMask.GetMask("Ground"));

        if (ground_info.collider == null || wall_info.collider != null)
        {
            if (move_right == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                move_right = false;
            }
            else if (move_right == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                move_right = true;
            }
        }
    }
    // player found, move there
    private void rotate_towards_player()
    {
        if (transform.position.x > player_transform.position.x)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (transform.position.x < player_transform.position.x)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    // look for player
    private bool check_target_visible()
    {
        var result = Physics2D.Raycast(transform.position, detected.position - transform.position, 30f,
            visibility_layer);
        if (result.collider != null)
        {
            //Debug.Log("Target visible");
            return (player_layermask & (1 << result.collider.gameObject.layer)) != 0;
        }
        //else
        //Debug.Log("Target is NOT visible");
        return false;
    }

    private void detect_target()
    {
        if (detected == null)
            check_if_player_in_range();
        else if (detected != null)
            detect_if_out_of_range();
    }

    private void detect_if_out_of_range()
    {
        // no target, target is dead, or target is far away
        if (detected == null || detected.gameObject.activeSelf == false || Vector2.Distance(transform.position,
            detected.position) > view_radius)
        {
            detected = null;
        }
    }

    private void check_if_player_in_range()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, view_radius, player_layermask);
        if (collision != null)
        {
            detected = collision.transform;
            //Debug.Log("Target detected");
        }
    }

    IEnumerator detection_delay()
    {
        yield return new WaitForSeconds(detection_check_delay);
        detect_target();
        StartCoroutine(detection_delay());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, view_radius);
        if (detected != null)
            Gizmos.DrawLine(transform.position, detected.position - transform.position);
    }
}

