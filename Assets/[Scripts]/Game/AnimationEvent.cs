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
        if (CharController.instance.isWater)
        {
            Instantiate(Resources.Load("Waterball"), PlayerController.instance.spawnPos.position, Quaternion.identity);
        }
        else if (CharController.instance.isFire)
        {
            Instantiate(Resources.Load("Fireball"), PlayerController.instance.spawnPos.position, Quaternion.identity);
        }
    }
}
