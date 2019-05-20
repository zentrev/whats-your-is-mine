using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPocketCollider : MonoBehaviour
{
    [HideInInspector] public AgentData agent;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Agent")
        {
            agent = collision.gameObject.GetComponent<AgentBehavior>().AgentData;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        agent = null;
    }

}
