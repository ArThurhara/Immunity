using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {
    public GameObject[] bulletPrefabs;
    int currentBulletIndex = 0;

    public void changeGun(int index) {
        if (index >= 0 && index < bulletPrefabs.Length) {
            currentBulletIndex = index;
        } else {
            Debug.LogWarning("Invalid gun index");
        }
    }

    public void Shoot(Vector3 spawnPoint, Vector2 direction) {
        GameObject bullet = Instantiate(bulletPrefabs[currentBulletIndex], spawnPoint, Quaternion.identity);
        try
        {
            bullet.GetComponent<Bullet>().SetDirection(direction);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Bullet prefab does not have a Bullet component");
        }
    }
}