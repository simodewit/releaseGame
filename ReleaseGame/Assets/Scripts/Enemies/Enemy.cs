using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region variables

    [Header("variables")]
    public Vector3 roamingRoom;
    public float safeDistance;

    //privates
    NavMeshAgent agent;

    #endregion

    #region start

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    #endregion

    #region states

    public void Roaming(Vector3 room)
    {
        float distance = DistanceCalculation(new Vector2(transform.position.x, transform.position.z), new Vector2(room.x, room.z));

        if (distance <= safeDistance)
        {
            Vector3 location = Randomize(-room, room);

            agent.destination = location;
        }
    }

    #endregion

    #region custom converters

    public float DistanceCalculation(Vector2 pos, Vector2 endPos)
    {
        float distance;

        distance = (pos.x - endPos.x) + (pos.y - endPos.y);

        return distance;
    }

    public Vector3 Randomize(Vector3 minValues, Vector3 maxValues)
    {
        Vector3 randomized;

        randomized.x = Random.Range(minValues.x, maxValues.x);
        randomized.y = Random.Range(minValues.y, maxValues.y);
        randomized.z = Random.Range(minValues.z, maxValues.z);

        return randomized;
    }

    #endregion
}
