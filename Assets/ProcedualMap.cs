using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualMap : MonoBehaviour
{
    private Transform player;
    public GameObject[] objects;
    public GameObject   end;
    public int distance = 20;

    MapCounter  mp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mp = GameObject.Find("MapCounter").GetComponent<MapCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.position.y - transform.position.y) <= distance){
            if (mp.end) {
                Instantiate(end, transform.position, Quaternion.identity, transform.parent.parent);
                mp.AddMap();
                Destroy(this.gameObject);
                return;
            }
            mp.AddMap();
            Instantiate(objects[UnityEngine.Random.Range(0, objects.Length)], transform.position, Quaternion.identity, transform.parent.parent);
            Destroy(this.gameObject);  
        }
    }
}
