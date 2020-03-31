using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject turretBarrel;
    public GameObject enemy;
    public GameObject turretFirePoint;
    public bool inrange;
    public float range;
    public Transform target;
    public GameObject bullet;
    bool canShoot = true;
    public float fireRate;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    private void Update()
    {
        if (target == null)
            return;

        turretBarrel.transform.LookAt(target.transform.position);
        Shooting();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    public void Shooting()
    {
        if (canShoot)
        {
            Instantiate(bullet, turretFirePoint.transform.position, turretFirePoint.transform.rotation);
            canShoot = false;
            StartCoroutine(triggerDelay());
        }
        
    }

    IEnumerator triggerDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
