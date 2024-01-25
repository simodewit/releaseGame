using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    public void Start()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
