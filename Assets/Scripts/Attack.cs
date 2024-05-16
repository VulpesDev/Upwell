using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Serializable]
    private struct circle {
        public Vector2 pos;
        public float x_offset;
        public float radius;
        public bool invert;
    };

    [SerializeField] private circle hitBox;

    public void AttackHit(int damage){
        HP hp = null;
        hitBox.pos = transform.position + new Vector3(hitBox.x_offset*(hitBox.invert? -1.0f : 1.0f), 0.0f, 0.0f);
        Collider2D[] colls = Physics2D.OverlapCircleAll(hitBox.pos, hitBox.radius);

        foreach (Collider2D hit in colls) {
            if (hit)
                hp = hit.GetComponent<HP>();
            if (hp != null)
                hp.takeDamage(damage);
        }
    }

    public void SetInvert(bool value) {
        hitBox.invert = value;
    }

    private void OnDrawGizmosSelected() {
        hitBox.pos = transform.position + new Vector3(hitBox.x_offset*(hitBox.invert? -1.0f : 1.0f), 0.0f, 0.0f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitBox.pos, hitBox.radius);

    }
}
