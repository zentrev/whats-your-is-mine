using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float money = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetAxis("Interact") > 0)
        {
            money += other.gameObject.GetComponent<AgentData>().Value;
        }
    }
}
