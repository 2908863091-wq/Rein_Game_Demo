using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float atkRate = 2f;
    [SerializeField] private Transform bulletPosition;
    private float atkTimer = 0;

    private List<GameObject> enemyList = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" )
        {
            enemyList.Add( other.gameObject );
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }
    private void Update()
    {
        ATK();
        DirectionControl();
    }
    protected virtual void ATK()
    {
        if (enemyList.Count== 0 || enemyList == null) return;
        atkTimer += Time.deltaTime;
        if (atkTimer >= atkRate)
        {
            Transform transform = GetTransform();
            if (transform != null)
            {
                atkTimer = 0;
                GameObject gameObject = GameObject.Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
                gameObject.GetComponent<Bullet>().SetTarget(enemyList[0].transform);
            }
        }
    }
    private void DirectionControl()
    {
        Transform transform = GetTransform();
        if (transform == null) return;
        
        Vector3 targetPos = transform.position;
        targetPos.y = transform.position.y;

        gameObject.transform.LookAt(targetPos);
    }
    public Transform GetTransform()
    {
        List<int> indexList = new List<int>();
        for (int i = 0; i < enemyList.Count; i++)
        {
            if(enemyList[i] == null || enemyList[i].Equals(null))
            {
                indexList.Add(i);
            }
        }
        for (int i = indexList.Count -1; i>=0; i--)
        {
            enemyList.RemoveAt(indexList[i]);
        }
        if(enemyList != null && enemyList.Count != 0)
        {
            return enemyList[0].transform;
        }
        return null;
    }
    protected Transform GetBulletPosition()
    {
        return bulletPosition;
    }
}
