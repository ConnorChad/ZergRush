using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        BulletMovement();
    }

    public void BulletMovement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        StartCoroutine(despawnBullet());
    }

    IEnumerator despawnBullet()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(gameObject);
    }
}
