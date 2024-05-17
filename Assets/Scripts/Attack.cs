using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Serializable]
    private struct circle {
        public float x_offset;
        public float y_offset;
        public float radius;
        public bool x_invert;
        public bool y_invert;
    };

    [SerializeField] private circle[] hitBoxes;

    public bool AttackHit(int damage){
        foreach (circle hitBox in hitBoxes) {
            HP hp = null;
            Vector2 pos = transform.position + new Vector3(hitBox.x_offset*(hitBox.x_invert? -1.0f : 1.0f),
                hitBox.y_offset*(hitBox.y_invert? -1.0f : 1.0f), 0.0f);
            Collider2D[] colls = Physics2D.OverlapCircleAll(pos, hitBox.radius);

            foreach (Collider2D hit in colls) {
                if (hit)
                    hp = hit.GetComponent<HP>();
                if (hp != null) {
                    hp.takeDamage(damage);
                    return true;
                }
            }
        }
        return false;
    }

    public bool AttackHit(int damage, int element){
        HP hp = null;
        if (element >= 0 && element < hitBoxes.Length)
        {
            circle hitBox = hitBoxes[element];
            Vector2 pos = transform.position + new Vector3(hitBox.x_offset*(hitBox.x_invert? -1.0f : 1.0f),
                hitBox.y_offset*(hitBox.y_invert? -1.0f : 1.0f), 0.0f);
            Collider2D[] colls = Physics2D.OverlapCircleAll(pos, hitBox.radius);

            foreach (Collider2D hit in colls) {
                if (hit)
                    hp = hit.GetComponent<HP>();
                if (hp != null) {
                    hp.takeDamage(damage);
                    return true;
                }
            }
        }
        return false;
    }

    public IEnumerator SetInvertX(bool value, float delay, int element) {
        yield return new WaitForSeconds(delay);
            hitBoxes[element].x_invert = value;
    }
    public IEnumerator SetInvertY(bool value, float delay, int element) {
        yield return new WaitForSeconds(delay);
            hitBoxes[element].y_invert = value;
    }

    private void OnDrawGizmosSelected() {
        foreach (circle hitBox in hitBoxes) {
            Vector2 pos = transform.position + new Vector3(hitBox.x_offset*(hitBox.x_invert? -1.0f : 1.0f),
                hitBox.y_offset*(hitBox.y_invert? -1.0f : 1.0f), 0.0f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pos, hitBox.radius);
        }
    }
}
