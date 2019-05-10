using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{

    public void OnSceneButtonClick(string nextScene)
    {
        GameManager.Instance.LoadLevel(nextScene);
    }
}
