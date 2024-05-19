using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]    private int     value = 20;
    [SerializeField]    private float     collect_speed = 20.0f;
    [SerializeField]    private float   magnetic_range = 0.5f;
    [SerializeField]    private Type    type = Type.fuel;
    private enum    Type { fuel, coins };

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Player_Behaviour player = other.GetComponent<Player_Behaviour>();
            if (type == Type.fuel && player)
                player.addFuelValue((sbyte)value);
            Destroy(gameObject);
        }
    }

    void Update() {
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= magnetic_range)
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, Time.deltaTime * collect_speed);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magnetic_range);
    }
}
