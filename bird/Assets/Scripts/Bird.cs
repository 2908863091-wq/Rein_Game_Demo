using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum birdState
{
    Waiting,
    Before_shoot,
    In_shoot,
    Die

}

public class Bird : MonoBehaviour
{
    public birdState state = birdState.Waiting;
    private bool Mouse_in_bird = false;
    public float maxDistance = 2.4f;
    public float speed = 5.0f;

    protected Rigidbody2D rigidbody2d;
    private bool isFly = true;

    void Start()
    {
        isFly=true;
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        switch (state)
        {
            case birdState.Waiting:
                WaitingControl();
                break;
            case birdState.Before_shoot:
                MouseController();
                break; 
            case birdState.In_shoot:
                StopControll();
                SkillControl();
                break;
            case birdState.Die:
                break;
            default:
                break;
        }
        
    }
    private void OnMouseDown()
    {
        if (state == birdState.Before_shoot)
        {
            Mouse_in_bird = true;
            Shooter.Instance.StartDraw(transform);
            AudioManager.Instance.Bird_select(transform.position);
        }
    }
    private void OnMouseUp()
    {
        if (state == birdState.Before_shoot)
        {
            Mouse_in_bird = false;
            Shooter.Instance.EndDraw();
            Fly();
        }
    }
    private void MouseController() 
    {   if (Mouse_in_bird)
        {
            transform.position = GetMousePosition();
        }
    }
    private Vector3 GetMousePosition()
    {
        Vector3 centerPosition = Shooter.Instance.getCenterPosition();
        Vector3 bird_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bird_position.z = 0;
        Vector3 mouseDir = bird_position - centerPosition;
        float distance = mouseDir.magnitude;
        if (distance > maxDistance)
        {
            bird_position = mouseDir.normalized * maxDistance + centerPosition;
        }

        return bird_position;
    }
    private void Fly()
    {
        float FlyForce = GetDistanceVectorToCenter();
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2d.velocity = (Shooter.Instance.getCenterPosition()-transform.position).normalized * speed * FlyForce;
        AudioManager.Instance.Bird_fly(transform.position);
        state = birdState.In_shoot;
    }
    private float GetDistanceVectorToCenter()
    {
        Vector3 centerPosition = Shooter.Instance.getCenterPosition();
        Vector3 birdPosition = transform.position;
        float distance = Vector3.Distance(centerPosition, birdPosition);
        return (distance);
    }
    public void GoStage(Vector3 position)
    {
        state = birdState.Before_shoot;
        transform.position = position;
    }
    private void StopControll()
    {
        if (rigidbody2d.velocity.magnitude < 0.1f)
        {
            state = birdState.Die;
            Invoke("Fade", 1f);
        }
        
    }
    public void Fade()
    {
        GameObject.Instantiate(Resources.Load("Boom"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        GameManager.Instance.NextBird();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == birdState.In_shoot)
        {
            isFly = false;
        }
        if (state == birdState.In_shoot && collision.relativeVelocity.magnitude > 7)
        {
            AudioManager.Instance.Bird_c(transform.position);
        }
    }
    
    private void SkillControl()
    {
        if (isFly == true && Input.GetMouseButtonDown(0))
        {
            FlyingSkill();
        }
        if (Input.GetMouseButtonDown(0))
        {
            FullTimeSkill();
        }
    }

    protected virtual void FlyingSkill()
    {

    }

    protected virtual void FullTimeSkill()
    {

    }

    private void WaitingControl()
    {

    }
}
