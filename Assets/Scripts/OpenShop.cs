using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    [SerializeField] Canvas m_shop = null;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_shop.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_shop.enabled = false;
    }
}
