using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            //kill player
            //game over
            //ui
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

}
