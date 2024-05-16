using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField]    private Slider      hpBar;
    [SerializeField]    private GameObject  drop = null;
    [SerializeField]    private int         maxHP = 100;
                        private int         currentHP;

    private void Die() {
        //dying sequence
        //this includes animations and object management (delete)
        //plus optionally additional behaviour (like respawning... etc.)
        if (drop != null)
            Instantiate(drop, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }

    void Start() {
        if (maxHP > 0)
            currentHP = maxHP;
        else
            Die();
        hpBar.value = currentHP;
    }

    public void takeDamage(int damage) {
        currentHP -= damage;
        if (currentHP <= 0) {
            Die();
        }
        hpBar.value = currentHP;
    }

    public void takeHeal(int heal) {
        currentHP += heal;
    }
}
