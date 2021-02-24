using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Camera cam;
    public int speed = 10;

    Vector2 mousePos;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 firePointVec2 = new Vector2(firePoint.position.x, firePoint.position.y);
            Vector2 projectileDirection = mousePos - firePointVec2;
            projectileDirection.Normalize();

            float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg - 90f; //angle = z rotation
            firePoint.rotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDirection.x * speed, projectileDirection.y*speed); 
        }
    }
}
