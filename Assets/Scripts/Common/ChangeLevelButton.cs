using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeLevelButton : MonoBehaviour
{

    [SerializeField] int m_nextLevel = 0;

    private Button m_button = null;

    void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameManager.Instance.LoadLevel(m_nextLevel);
    }
}
