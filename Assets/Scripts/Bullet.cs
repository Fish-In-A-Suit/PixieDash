using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 20;
    public GameObject bulletImpactEffect;

    public float impactVolume = 1; //the volume of the bullet impact clip
    public AudioClip bulletImpactSoundDefault; //the default bullet sound effect
    public AudioClip bulletImpactSoundEnemy; //the sound effect when bullet hits enemy

    bool shotByPlayer = false; //used for controlling the bullet collision 

    //public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Normally,apply the velocity to the bullet here in the Start method, but
         * since we have to take velocity x and y components into account (accessible from
         * the weapon script), we get a reference to the bullet's rb in Weapon.cs and
         * use this script only for collision detection.
         * */
        //impactSound = GetComponent<AudioSource>();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("hitinfo.name =" + hitInfo.name);
        Debug.Log("Bullet.shotByPlayer = " + shotByPlayer);

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Player player = hitInfo.GetComponent<Player>();

        if (shotByPlayer)
        {
            //bullet impact
            if (!hitInfo.name.Equals("player-body1")) { //prevents bullet impact from registering player as collider
                if (enemy != null) {
                    AudioSource.PlayClipAtPoint(bulletImpactSoundEnemy, hitInfo.transform.position, impactVolume);
                }
                else {
                    AudioSource.PlayClipAtPoint(bulletImpactSoundDefault, hitInfo.transform.position, impactVolume);
                }
                Instantiate(bulletImpactEffect, transform.position, transform.rotation);
            }
        } else { //bullet was not shot by player --> check if collider is player, then he should take damage
            if(player!=null)
            {
                //bullet hit from enemy and hit player
                player.TakeDamage(bulletDamage);

                //todo: make a player wounded effect and play it here instead of default bullet sound
                AudioSource.PlayClipAtPoint(bulletImpactSoundDefault, hitInfo.transform.position, impactVolume);
                Instantiate(bulletImpactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if (hitInfo.name.Equals("Tilemap"))
        {
            Destroy(gameObject);
        }
    }

    public void setShotByPlayer(bool value)
    {
        shotByPlayer = value;
    }
}
