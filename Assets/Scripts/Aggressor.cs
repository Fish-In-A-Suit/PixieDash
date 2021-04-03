using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script should be attached to enemies. handles firing at the player
public class Aggressor : MonoBehaviour
{
    Transform player;
    public Transform firepoint;
    public LayerMask raycastIgnoreLayer; //raycast should ignore the enemy!

    public float projectileSpawnOffset = 1f;
    public int bulletSpeed = 20;
    public GameObject bulletPrefab;

    public AudioClip shootSound;
    public float shootSoundVolume = 1f;

    public float shotCooldown = 1f;
    float nextFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //find player
        player = GameObject.Find("player-body1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFire)
        {
            if (isPlayerVisible())
            {
                nextFire = Time.time + shotCooldown;

                //shoot
                Vector2 playerLocation = new Vector2(player.position.x, player.position.y);
                Vector2 firepointLocation = new Vector2(firepoint.position.x, firepoint.position.y);
                Vector2 projectileDirection = playerLocation - firepointLocation;
                projectileDirection.Normalize();

                float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg - 90f;
                firepoint.rotation = Quaternion.Euler(0, 0, angle);

                Vector3 projectileOffset = new Vector3(projectileDirection.x * projectileSpawnOffset, projectileDirection.y * projectileSpawnOffset, 0);
                Vector3 projectileSpawnPoint = firepoint.position + projectileOffset;
                GameObject bullet = Instantiate(bulletPrefab, projectileSpawnPoint, firepoint.rotation);

                //play audio
                AudioSource.PlayClipAtPoint(shootSound, firepointLocation, shootSoundVolume);

                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDirection.x * bulletSpeed, projectileDirection.y * bulletSpeed);
            }
        }
        
    }

    bool isPlayerVisible()
    {
        Vector2 rayDirection = player.position - transform.position;
        Debug.DrawLine(player.position, transform.position, Color.green, 2, false); //enable gizmos if line isn't drawn
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, rayDirection, 1000, ~raycastIgnoreLayer); //~ inverts the layer mask!
        Debug.Log(rayHit.collider.name);
        if(rayHit.collider != null)
        {
            if(rayHit.collider.name.Equals("player-body1")) {
                Debug.Log("player visible");
                return true;
            } else
            {
                Debug.Log("player not visible");
                return false;
            }
        } else
        {
            Debug.Log("player not visible");
            return false;
        }
    }
}
