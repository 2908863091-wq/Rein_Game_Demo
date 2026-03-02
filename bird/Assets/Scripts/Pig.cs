using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Pig : Destructible 
{
    private Vector3 score_position;

    public override void Dead()
    {
        GameObject.Instantiate(boomPrefab, transform.position, Quaternion.identity);
        GameManager.Instance.AddScore(500);
        Destroy(gameObject);
        GameManager.Instance.Pigdie();
        score_position = transform.position;
        score_position.y += 0.75f;
        GameObject.Instantiate(Resources.Load("Score_fx"),score_position, Quaternion.identity);
        GameManager.Instance.AddScore(5000);
        AudioManager.Instance.Pig_c2(transform.position);
    }
    protected override void C_sound()
    {
        AudioManager.Instance.Pig_c1(transform.position);
    }


}

