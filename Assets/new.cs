using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Collision: " + col.gameObject.name);
    }
    void OnTriggerEnter2D(Collider2D col) {
        
    }
}
