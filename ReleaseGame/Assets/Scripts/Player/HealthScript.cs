using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    int health;
    bool isPlayer;

    public void Health(int damage)
    {
        health -= damage;

        if (health < 0 && isPlayer)
        {
            //goes into kill cam or something
        }
        else if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}
