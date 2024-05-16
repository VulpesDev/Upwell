using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D target_hitBox;
    void OnTriggerStay() {
        if (target_hitBox != null) {
            // After Cosmo finishes the HP script
            // target_hitBox.GetComponent<HP>().takeDamage(1);
        }
    }
}
