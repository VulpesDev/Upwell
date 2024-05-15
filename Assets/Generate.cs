using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public Collider2D col;
    public Transform spawnpos;
    public GameObject prefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Instantiate(prefab, spawnpos.position, Quaternion.identity);
    }

}
