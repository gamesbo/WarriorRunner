using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Shoot()
    {
        Instantiate(Resources.Load("Fireball"), PlayerController.instance.spawnPos.position, Quaternion.identity);
    }
}
