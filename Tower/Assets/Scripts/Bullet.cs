using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 50;
    [SerializeField] private float speed = 10;
    [SerializeField] private GameObject effectPrefab;

    private Transform ATKtarget;

    private void Update()
    {
        if (ATKtarget == null) { Dead(); return; }
        transform.LookAt(ATKtarget);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position,ATKtarget.position) < 1)
        {
            ATKtarget.GetComponent<Enemy>().TakeDamage(damage);
            Dead();
        }
    }
    private void Dead()
    {
        Destroy(this.gameObject);
        GameObject gameObject = GameObject.Instantiate(effectPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject,1f);

        if (ATKtarget != null)
        {
            gameObject.transform.parent = ATKtarget.transform;
        }
    }
    public void SetTarget(Transform transform)
    {
        this.ATKtarget = transform;
    }
}
