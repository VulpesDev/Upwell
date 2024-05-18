using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class enemy_patrol : MonoBehaviour
{
    private float speed = 5;
    //public float distance;
    public float minTime = 1f; // Minimum time before changing direction
    public float maxTime = 5f; // Maximum time before changing direction
    private bool move_right = true;

    public Transform ground_detection;
    public Transform wall_detection;

    //private float waitTime = 3.0f;

    //void Start()
    //{
    //    InvokeRepeating("RandomMove", 0f, waitTime);
    //}

    //void RandomMove()
    //{
    //    int behaviour = Random.Range(0, 3);
    //    switch (behaviour)
    //    {
    //        case 0:
    //            transform.eulerAngles = new Vector3(0, -180, 0);
    //            move_right = false;
    //            break;
    //        case 1:
    //            transform.eulerAngles = new Vector3(0, 0, 0);
    //            move_right = true;
    //            break;
    //        //case 2:
    //            //rb.velocity = new Vector2(rb.velocity.x, Random.Range(1, 5));
    //            //break;
    //    }
    //}

    void Update()
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
        //else
            //RandomMove();
    }
}

//public class enemy_patrol : MonoBehaviour
//{
//    private float speed = 5;
//    //public float distance;
//    //public float minTime = 5f; // Minimum time before changing direction
//    //public float maxTime = 10f; // Maximum time before changing direction
//    //private Vector2 direction; // Current movement direction
//    private bool move_right = true;
//    //private float timer;

//    public Transform ground_detection;
//    public Transform wall_detection;

//    private void Start()
//    {
//        //direction = direction == Vector2.right ? Vector2.right : Vector2.left;
//        //direction = Random.insideUnitSphere;
//        //direction = Vector2.right;
//        //timer = Random.Range(minTime, maxTime);
//    }

//    void Update()
//    {
//        transform.Translate(Vector2.right * speed * Time.deltaTime);
//        //timer -= Time.deltaTime;
//        //Debug.Log("Timer: " + timer);
//        //Debug.Log("direction: " + direction);
//        Debug.Log("move right?: " + move_right);


//        RaycastHit2D ground_info = Physics2D.Raycast(ground_detection.position, Vector2.down, 2.0f, LayerMask.GetMask("Ground"));
//        RaycastHit2D wall_info = Physics2D.Raycast(wall_detection.position, Vector2.right, 0.5f);

//        if (ground_info.collider == null || wall_info.collider != null)
//        {
//            if (move_right == true)
//            {
//                transform.eulerAngles = new Vector3(0, -180, 0);
//                move_right = false;
//                //direction = Vector2.left;
//            }
//            else if (move_right == false)
//            {
//                transform.eulerAngles = new Vector3(0, 0, 0);
//                move_right = true;
//                //direction = Vector2.right;
//            }
//        }
//    }
//}

// tried to make patroling more interesting
//public class enemy_patrol : MonoBehaviour
//{
//    public float speed;
//    public Transform ground_detection;
//    public Transform wall_detection;

//    private bool move_right = true;
//    private Vector3 initialPosition;
//    private Vector3 targetPosition;
//    private float timer = 1.0f;
//    public float moveTimeRight = 0.5f; // Time to move right
//    public float moveTimeLeft = 0.5f; // Time to move left
//    public float waitTime = 0.5f; // Time to wait between movements

//    void Start()
//    {
//        initialPosition = transform.position;
//        targetPosition = initialPosition + Vector3.right; // Initially move to the right
//    }

//    void Update()
//    {
//        // Update timer
//        timer += Time.deltaTime;
//        Debug.Log("Timer: " + timer);
//        Debug.Log("Delta Time: " + Time.deltaTime);

//        // Check if it's time to change direction
//        if (timer >= (move_right ? moveTimeRight : moveTimeLeft))
//        {
//            // Change direction
//            move_right = !move_right;
//            timer = 0f;

//            // Set new target position
//            if (move_right)
//            {
//                targetPosition = initialPosition + Vector3.right;
//            }
//            else
//            {
//                targetPosition = initialPosition;
//            }
//        }

//        // Move towards the target position
//        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

//        // Check for wall detection
//        RaycastHit2D wall_info = Physics2D.Raycast(wall_detection.position, Vector2.right, 0.1f);
//        if (wall_info.collider != null)
//        {
//            // Change direction if a wall is detected
//            move_right = !move_right;
//            timer = 1.0f;
//            targetPosition = move_right ? initialPosition + Vector3.right : initialPosition;
//        }
//    }
//}
