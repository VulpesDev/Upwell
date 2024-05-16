using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool active = false;
    private Collider2D attack_hitBox = null;
    
    /// <summary>
    /// when attack is active and hitbox collides with an object with HP script, the object takes damage
    /// </summary>
    void OnTriggerStay() {
        //HP hp = attack_hitBox.GetComponent<HP>();
        if (active && attack_hitBox != null /*&& hp */) {
            // hp.takeDamage(1);
        }
    }

    /// <summary>
    /// sets the attack to be active
    /// </summary>
    public void setActive(){
        active = true;
    }

    /// <summary>
    /// sets the attack to be inactive
    /// </summary>
    public void setInactive(){
        active = false;
    }

    /// <summary>
    /// sets the attack to be active for a certain amount of time
    /// </summary>
    /// <param name="time"></param>
    public void pingActive(float time){
        setActive();
        Invoke("setInactive", time);
    }
}
