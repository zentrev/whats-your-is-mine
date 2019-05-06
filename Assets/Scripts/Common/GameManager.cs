

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum eSet
    {
        NONE,
        MENU,
        GAME
    }

    [SerializeField] eSet m_curentSet = eSet.NONE;
    public eSet CurentSet { get => m_curentSet; set => m_curentSet = value; }

    public void SetSet(eSet set)
    {
        CurentSet = set;
    }

    void Update()
    {
        switch (CurentSet)
        {
            case eSet.NONE:
                break;
            case eSet.MENU:
                break;
            case eSet.GAME:
                break;
            default:
                break;
        }
    }

    public void LoadLevel(string level, eSet set = eSet.GAME)
    {
        m_curentSet = set;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void LoadLevel(int level, eSet set = eSet.GAME)
    {
        m_curentSet = set;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}
