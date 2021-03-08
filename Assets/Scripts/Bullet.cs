using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    public int bulletDamage = 20;
    public GameObject bulletImpact;
    public float impactVolume = 1; //the volume of the bullet impact clip

    AudioSource impactSound;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Normally,apply the velocity to the bullet here in the Start method, but
         * since we have to take velocity x and y components into account (accessible from
         * the weapon script), we get a reference to the bullet's rb in Weapon.cs and
         * use this script only for collision detection.
         * */
        impactSound = GetComponent<AudioSource>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("hitinfo.name =" + hitInfo.name);
        AudioSource.PlayClipAtPoint(impactSound.clip, hitInfo.transform.position, impactVolume);

        //bullet impact
        if(!hitInfo.name.Equals("player-body1"))
        {
            Instantiate(bulletImpact, transform.position, transform.rotation);
        }
        
        if(hitInfo.name.Equals("Tilemap"))
        {
            Destroy(gameObject);
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
