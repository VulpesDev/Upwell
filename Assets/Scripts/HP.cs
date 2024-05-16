using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]    private int maxHP = 100;
                        private int currentHP;

    private void Die() {
        //dying sequence
        //this includes animations and object management (delete)
        //plus optionally additional behaviour (like respawning... etc.)
        Destroy(gameObject, 0.1f);
    }
    
    void Start() {
        if (maxHP > 0)
            currentHP = maxHP;
        else
            Die();
    }

    public void takeDamage(int damage) {
        currentHP -= damage;
        if (currentHP <= 0) {
            Die();
        }
    }

    public void takeHeal(int heal) {
        currentHP += heal;
    }
}
