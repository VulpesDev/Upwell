using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_chase : MonoBehaviour
{
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

    private void Start()
    {
        StartCoroutine(detection_delay());
    }

    private void Update()
    {
        if (detected != null)
            target_visible = check_target_visible();
    }

    private bool check_target_visible()
    {
        var result = Physics2D.Raycast(transform.position, detected.position - transform.position, 30f,
            visibility_layer);
        if (result.collider != null)
        {
            Debug.Log("Target visible");
            return (player_layermask & (1 << result.collider.gameObject.layer)) != 0;
        }
        else
            Debug.Log("Target is NOT visible");
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
            Debug.Log("Target detected");
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
