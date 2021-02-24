using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is an alternative to the Weapon.cs script. Weapon.cs uses prefab-based shooting, whereas this script uses Raycast based shooting.
 * 
 * */
public class WeaponRaycast : MonoBehaviour
{

    public Transform firePoint; //the point where player is firing from
    public Camera cam; //main camera
    public int damage = 20; //bullet damage
    public float projectileDisplayDuration = 0.02f; //the amount of time the projectile line will be rendered
    public LayerMask colliderMask; //layers set in colliderMask will count as valid collisions
    public GameObject impactEffect;
    public LineRenderer lineRenderer; //line renderer of the projectile's line

    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shootDirection = mouse - firePoint.position;

        Debug.DrawRay(firePoint.position, shootDirection, Color.red, 5, false);
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, shootDirection, Mathf.Infinity, colliderMask);
        

        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            Debug.Log("name = " + hitInfo.transform.name);
            Debug.Log("enemy = " + enemy);
            if(enemy != null)
            {
                enemy.TakeDamage(20);
            }

            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        } else {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + shootDirection * 100);
        }

        //coroutines enable stops in code
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(projectileDisplayDuration);
        lineRenderer.enabled = false;
    }
}
