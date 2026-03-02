using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_bird : Bird
{
    private bool is_use = false;
    public float boom_distance = 2.0f;
    public float boom_force = 10.0f;
    protected override void FullTimeSkill()
    {
        base.FullTimeSkill();
        if (is_use == false)
        {
            GameObject.Instantiate(Resources.Load("BigBoom"), transform.position, Quaternion.identity);
            is_use = true;

            //爆炸物理效果
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, boom_distance);
            foreach (Collider2D col in colliders)
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = rb.transform.position - transform.position;
                    rb.AddForce(dir.normalized * boom_force, ForceMode2D.Impulse);
                }
            }

            Fade();
            AudioManager.Instance.Boom(transform.position);
        }
        
    }
}
