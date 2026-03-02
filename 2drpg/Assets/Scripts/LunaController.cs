using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LunaController : MonoBehaviour
{
    public float speed;
    private int max_health = 5;
    private int ima_health = 5;
    private bool is_move = false;


    private Rigidbody2D rigidbody2d;
    private Animator animator;
    public AudioSource hurt;
    public AudioSource resotre;
    public int Max_health { get => max_health; }
    public int Ima_health { get => ima_health; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        ima_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Movevalue_x", horizontal);
        animator.SetFloat("Movevalue_y", vertical);
        animator.SetBool("can_move",is_move);

        Vector2 position = transform.position;
        position.x += speed * horizontal*Time.deltaTime ;
        position.y += speed * vertical*Time.deltaTime;


        if (horizontal == 0 && vertical ==0)
        {
            is_move = false;
            
        }
        else
        {
            is_move = true;
        }

        rigidbody2d.MovePosition(position);

    }

    public void ChangeHealth(int amount)
    {
        ima_health = Mathf.Clamp(ima_health + amount, 0, max_health);
        Debug.Log(Ima_health + "/" + Max_health);

        if (amount < 0)
        {
            transform.DOShakePosition(0.1f, 0.1f);
            hurt.Play();

        }
        else
        {
            transform.DOShakePosition(1f, 1f);
            resotre.Play();
        }
    }


}
