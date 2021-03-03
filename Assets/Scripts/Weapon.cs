using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Camera cam;
    public int speed = 10;
    public float projectileSpawnOffset = 1f; //bullet should be offset (so as not to spawn inside the player)

    AudioSource audioSource;

    Vector2 mousePos;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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

            Vector3 projectileOffset = new Vector3(projectileDirection.x * projectileSpawnOffset, projectileDirection.y * projectileSpawnOffset, 0);
            Vector3 projectileSpawnPoint = firePoint.position + projectileOffset;
            GameObject bullet = Instantiate(bulletPrefab, projectileSpawnPoint, firePoint.rotation);

            audioSource.Play(0);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDirection.x * speed, projectileDirection.y*speed); 
        }
    }
}
