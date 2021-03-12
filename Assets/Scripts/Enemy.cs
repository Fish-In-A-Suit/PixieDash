using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;

    AudioSource audioSource;
    public AudioClip deathSound;
    public float deathSoundVolume = 1f;  

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //audioSource.PlayOneShot(deathSound, 1f);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
