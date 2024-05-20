using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapCounter : MonoBehaviour
{
    public bool end = false;
    private int mapCount = 0;
    public  int mapSize = 5;

    private GameObject[] spawners;

    public void AddMap(){
        mapCount++;
        if (mapCount >= mapSize){
            end = true;
            spawners = GameObject.FindGameObjectsWithTag("Spawner");
            if (spawners != null) {
                foreach (GameObject spawner in spawners){
                    spawner.SetActive(false);
                }
            }
        }
    }
}
