using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region variables

    [Header("Gun info")]
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float reloadTime;
    [Tooltip("The ammo a full mag has of this gun")]
    public int ammoCount;
    [Tooltip("The place where the bullet spawns")]
    public Transform endOfBarrel;
    [Tooltip("The type this gun starts as")]
    public gunType type;
    [Tooltip("The total bullets before it stops shooting")]
    public int totalBurstShots;
    [Tooltip("The interval between bullets shot (not relevant to semi-automatic)")]
    public float shootInterval;

    [Header("switch types")]
    [Tooltip("The ability for a player to change this gun into a different type of shooting")]
    public bool canSwitch;
    [Tooltip("The types the player is able to switch between (only works if canSwitch is enabled) ALWAYS PUT THE FIRST ONE IN THE ARRAY THE SAME AS THE START TYPE")]
    public gunType[] switchableTypes;

    //privates
    int burstShots;
    bool hasShot;
    float timer;
    int currentType;
    bool isReloading;
    float reloadTimer;
    int currentAmmo;

    public enum gunType
    {
        automatic,
        burst,
        semiAutomatic
    }

    #endregion

    #region update

    public void Update()
    {
        Inputs();
        SwapGunType();
        Reload();
    }

    #endregion

    #region inputs

    public void Inputs()
    {
        if (Input.GetMouseButton(0))
        {
            timer -= Time.deltaTime;

            if (isReloading)
            {
                return;
            }

            if (currentAmmo == 0)
            {
                return;
            }

            if (type == gunType.automatic && timer <= 0)
            {
                timer = shootInterval;
                Shoot();
            }
            if (type == gunType.burst && burstShots != totalBurstShots && timer <= 0)
            {
                timer = shootInterval;
                burstShots += 1;
                Shoot();
            }
            if (type == gunType.semiAutomatic && hasShot != true)
            {
                hasShot = true;
                Shoot();
            }
        }
        else
        {
            burstShots = 0;
            hasShot = false;
            timer = 0;
        }
    }

    #endregion

    #region swap

    public void SwapGunType()
    {
        if (Input.GetKeyDown(KeyCode.B) && canSwitch)
        {
            if (switchableTypes.Length == currentType + 1)
            {
                currentType = 0;
                type = switchableTypes[currentType];
            }
            else
            {
                currentType += 1;
                type = switchableTypes[currentType];
            }
        }
    }

    #endregion

    #region reload

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
        }

        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
        }

        if (reloadTimer <= 0)
        {
            isReloading = false;
            reloadTimer = reloadTime;
            currentAmmo = ammoCount;
        }
    }

    #endregion

    #region shoot

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, endOfBarrel.position, Quaternion.identity);
        bullet.transform.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        currentAmmo -= 1;
    }

    #endregion
}
