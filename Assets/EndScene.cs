using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndScene : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text new_score, prev_score, new_time, prev_time;
   void OnCollisionEnter2D(Collision2D col) {
       if(col.gameObject.tag == "Player") {
           Debug.Log("Player collided with End Scene");
           canvas.SetActive(true);
            new_score.text = Multiplier.getHighScore().ToString();
            prev_score.text = Multiplier.LoadHighScore().ToString();
            Multiplier.TimeStamp();
            new_time.text = Multiplier.getTimeElapsed().ToString();
            prev_time.text = Multiplier.LoadQuickTime().ToString();
            Destroy(this);
        //    Application.Quit();
       }
   }
}
