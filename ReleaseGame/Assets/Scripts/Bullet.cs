using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthScript>() != null)
        {
            other.GetComponent<HealthScript>().Health(bulletDamage);
        }

        Destroy(gameObject);
    }
}
