using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float max_health = 500;
    [SerializeField] private int value = 10;
    private HealthUI healthUI;
    private float ima_health;
    private int pointIndex = 0;
    private Vector3 targetPosition = Vector3.zero;
    

    private void Awake()
    {
        ima_health = max_health;
        healthUI = GetComponentInChildren<HealthUI>();
    }
    private void Start()
    {
        targetPosition = WayPoint.Instance.GetPoint(pointIndex);
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate((targetPosition - transform.position).normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            MoveNextPoint();
        }
    }
    private void MoveNextPoint()
    {
        if (pointIndex < WayPoint.Instance.GetLength() - 1)
        {
            pointIndex++;
            targetPosition = WayPoint.Instance.GetPoint(pointIndex);
        }
        else 
        {
            Die();
            GameManager.Instance.Lose();
        }

    }
    public void TakeDamage(float damage)
    {
        ima_health -= damage;
        healthUI.UpdateProcess((float)ima_health/max_health);

        if (ima_health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
        BuildManager.Instance.AddMoney(value);
    }
}
