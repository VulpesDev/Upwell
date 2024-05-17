using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ai : MonoBehaviour
{
    public Transform player_transform;
    public enemy_patrol enemyPatrol;
    public bool is_chasing;
    public float chase_distance;

    private float speed = 5;
    private bool move_right = true;

    public Transform ground_detection;
    public Transform wall_detection;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_chasing)
        {

        }
        else
        {
            // player not found so patrol or look for player
            //if (Vector2.Distance(transform.position, player_transform.position) < chase_distance)
                //is_chasing = true;

            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D ground_info = Physics2D.Raycast(ground_detection.position, Vector2.down, 2.0f, LayerMask.GetMask("Ground"));
            RaycastHit2D wall_info = Physics2D.Raycast(wall_detection.position, Vector2.right, 0.1f);

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
        
    }
}
