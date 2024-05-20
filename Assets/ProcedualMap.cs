using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualMap : MonoBehaviour
{
    private Transform player;
    public GameObject[] objects;
    public int distance = 20;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.position.y - transform.position.y) <= distance){
            Instantiate(objects[UnityEngine.Random.Range(0, objects.Length)], transform.position, Quaternion.identity, transform.parent.parent);
            Destroy(this.gameObject);  
        }
    }
}
