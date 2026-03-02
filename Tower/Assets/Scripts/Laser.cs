using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Turret
{
    [SerializeField] private float damage;
    private LineRenderer lineRenderer;
    private void Awake()
    {       
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }
    protected override void ATK()
    {
        Transform transform = GetTransform();

        if (transform == null)
        {
            lineRenderer.enabled = false;
            return;
        }
        else
        {
            transform.GetComponent<Enemy>().TakeDamage(damage * Time.deltaTime);

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, GetBulletPosition().position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
