using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int max_health;
    private int ima_health;

    public List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;
    public GameObject boomPrefab;
    public int damage = 4;

    private void Start()
    {
        ima_health = max_health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
        boomPrefab = Resources.Load<GameObject>("Boom");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ima_health -= (int)collision.relativeVelocity.magnitude * damage;

        if (ima_health <= 0)
        {
            Dead();
        }
        if (ima_health <= max_health/2 && ima_health >0) 
        {
            spriteRenderer.sprite = sprites[1];
            C_sound();
        }

    }
    protected virtual void C_sound()
    {
        AudioManager.Instance.Wood_c(transform.position);
    }

    public virtual void Dead()
    {
        GameObject.Instantiate(boomPrefab,transform.position,Quaternion.identity);
        GameManager.Instance.AddScore(500);
        AudioManager.Instance.Wood_d(transform.position);
        Destroy(gameObject);

    }


}
