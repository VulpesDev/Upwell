using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_ai : MonoBehaviour
{
    private float speed = 15;
    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Vector3 direction = player.position - transform.position;
        float bullet_rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, bullet_rotation + 90);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            destroy_bullet();
            Debug.Log("destroyed");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            destroy_bullet();
        }
    }

    void destroy_bullet()
    {
        Destroy(gameObject);
    }
}
