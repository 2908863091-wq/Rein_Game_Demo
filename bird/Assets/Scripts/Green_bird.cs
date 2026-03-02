using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_bird : Bird
{
    private bool is_use = false;
    protected override void FlyingSkill()
    {
        base.FlyingSkill();
        if (is_use == false)
        {
            Vector2 velocity = rigidbody2d.velocity;
            velocity.x = -velocity.x * 1.2f;
            velocity.y = velocity.y * 0.5f ;

            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;

            rigidbody2d.velocity = velocity;
            GameObject.Instantiate(Resources.Load("Boom"), transform.position, Quaternion.identity);
            is_use = true;
        }
        
    }
    
}
