using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * 200f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Door door = other.GetComponent<Door>();

        if (door)
        {
            door.value++;
            Destroy(transform.GetChild(1).gameObject,1.2f);
            transform.GetChild(1).parent = null;
            Destroy(gameObject);
        }
    }

}
