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

    private CameraShake shake;

    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("CameraShakeManager").GetComponent<CameraShake>();
    }

    public void TakeDamage(int damage)
    {
        shake.CamShake();

        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //camRipple.RippleEffect();
        //audioSource.PlayOneShot(deathSound, 1f);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
