using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_bird : Bird
{
    private bool is_use = false;
    protected override void FlyingSkill()
    {
        base.FlyingSkill();
        if (is_use == false)
        {
            GameObject.Instantiate(Resources.Load("Boom"), transform.position, Quaternion.identity);
            rigidbody2d.velocity = rigidbody2d.velocity * 2;
            is_use = true;
        }
        
    }
    
}
