using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameController : Singleton<MiniGameController>
{
    [SerializeField] GameObject m_miniGameCanvas = null;
    [SerializeField] GameObject m_miniGamePanel = null;
    [SerializeField] Image countdownImage = null;

    [SerializeField] GameObject m_moneyObject = null;
    [SerializeField] [Range(0.0f, 100.0f)] float m_minFlingSpeed = 0.0f;
    [SerializeField] [Range(0.0f, 100.0f)] float m_maxFlingSpeed = 50.0f;

    [SerializeField] Transform[] m_leftNodes = new Transform[2];
    [SerializeField] Transform[] m_rightNodes = new Transform[2];

    [SerializeField] Player m_player = null;
    public AgentData m_agentData = null;

    bool m_inGame = false;
    float m_totalTime = 0.0f;
    float m_runningTime = 0.0f;

    Dictionary<GameObject, float> m_objectsToSpawn = new Dictionary<GameObject, float>();
    List<GameObject> m_activeObjects = new List<GameObject>();

    //Hook Fields
    [SerializeField] GameObject m_miniGameHook = null;
    private float mousePosiX = 0.0f;
    private Vector2 rayHitWorldPosi = Vector2.zero;
    [SerializeField] Canvas m_miniGameCnvasTest = null;
    [SerializeField] Transform m_leftLimiter = null;
    [SerializeField] Transform m_rightLimiter = null;
    [SerializeField] Transform m_dropLimiter = null;
    [SerializeField] float m_hookSpeed = 1.0f;

    Vector3 m_hookStickPos = Vector3.zero;
    bool m_hookDroped = false;
    bool m_hookRaising = false;


    void Start()
    {
        m_hookStickPos = m_miniGameHook.transform.localPosition;

        CloseMiniGame();
        if (m_player == null) m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_miniGameCnvasTest.transform as RectTransform, Input.mousePosition, m_miniGameCnvasTest.worldCamera, out pos);
    }

    void Update()
    {
        if (!m_hookDroped)
        {
            Vector2 movePosition = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_miniGameCnvasTest.transform as RectTransform,
            Input.mousePosition, m_miniGameCnvasTest.worldCamera,
            out movePosition);

            movePosition.y = m_miniGameHook.transform.localPosition.y;

            if (movePosition.x < m_leftLimiter.localPosition.x) movePosition.x = m_leftLimiter.localPosition.x;
            if (movePosition.x > m_rightLimiter.localPosition.x) movePosition.x = m_rightLimiter.localPosition.x;

            m_miniGameHook.transform.position = m_miniGameCnvasTest.transform.TransformPoint(movePosition);

            if (Input.GetMouseButtonDown(0))
            {
                m_hookDroped = true;
                m_hookStickPos = m_miniGameHook.transform.localPosition;
            }
        }
        else
        {
            Vector3 movePosition = m_miniGameHook.transform.position;
            if (m_miniGameHook.transform.localPosition.y < m_dropLimiter.transform.localPosition.y) m_hookRaising = true;

            if (m_miniGameHook.transform.localPosition.y > m_hookStickPos.y)
            {
                m_hookRaising = false;
                m_hookDroped = false;
            }
            if (!m_hookRaising)
            {
                movePosition.y -= m_hookSpeed * Time.deltaTime;
            }
            else
            {
                movePosition.y += m_hookSpeed * Time.deltaTime;

            }
            m_miniGameHook.transform.position = movePosition;
        }
        m_runningTime += Time.deltaTime;
        UpdateObjects();
    }

    private void UpdateObjects()
    {
        List<GameObject> removals = new List<GameObject>();
        foreach (KeyValuePair<GameObject, float> entry in m_objectsToSpawn)
        {
            if (entry.Value > m_runningTime)
            {
                m_activeObjects.Add(entry.Key);
                removals.Add(entry.Key);
                WrapObject(entry.Key, (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f));
            }
        }

        for (int i = removals.Count - 1; i >= 0; i--)
        {
            m_objectsToSpawn.Remove(removals[i]);
            removals.RemoveAt(i);
        }

        foreach (GameObject go in m_activeObjects)
        {
            if (go.transform.position.x < m_leftNodes[0].position.x)
            {
                WrapObject(go, true);
                break;
            }
            if (go.transform.position.x > m_rightNodes[0].position.x)
            {
                WrapObject(go, false);
            }
            if (go.transform.position.y < m_leftNodes[1].position.y && go.transform.position.y < m_rightNodes[1].position.y)
            {
                if (go.transform.position.x - m_leftNodes[0].position.x > m_rightNodes[0].position.x - go.transform.position.x)
                {
                    WrapObject(go, false);
                }
                else
                {
                    WrapObject(go, true);
                }
            }
        }

        foreach (GameObject go in m_activeObjects)
        {
            if (m_miniGameHook.GetComponent<PolygonCollider2D>().IsTouching(go.GetComponent<Collider2D>()))
            {
                m_activeObjects.Remove(go);
                Destroy(go);
                m_agentData.Value--;
            }
        }
    }

    private void WrapObject(GameObject gameObject, bool wrapLeft)
    {
        if (wrapLeft)
        {
            float height = UnityEngine.Random.Range(m_leftNodes[0].position.y, m_leftNodes[1].position.y);
            Vector3 newPos = m_leftNodes[0].position;
            newPos.y = height;
            gameObject.transform.position = newPos;
            gameObject.GetComponent<Rigidbody2D>().velocity = (Vector3.right * UnityEngine.Random.Range(m_minFlingSpeed, m_maxFlingSpeed) * 10) + (Vector3.up * UnityEngine.Random.Range(30, 100));
        }
        else
        {
            float height = UnityEngine.Random.Range(m_leftNodes[0].position.y, m_leftNodes[1].position.y);
            Vector3 newPos = m_rightNodes[0].position;
            newPos.y = height;
            gameObject.transform.position = newPos;
            gameObject.GetComponent<Rigidbody2D>().velocity = (Vector3.left * UnityEngine.Random.Range(m_minFlingSpeed, m_maxFlingSpeed) * 10) + (Vector3.up * UnityEngine.Random.Range(30, 100));
        }
    }

    private void SpawnObjects()
    {
        m_runningTime = 0.0f;
        m_objectsToSpawn.Clear();
        for (int i = 0; i < m_agentData.Value; i++)
        {
            m_objectsToSpawn.Add(GameObject.Instantiate(m_moneyObject, m_miniGamePanel.transform), UnityEngine.Random.Range(0.0f, m_totalTime * 0.8f));
        }

        foreach (GameObject go in m_agentData.Items)
        {
            //m_objectsToSpawn.Add(GameObject.Instantiate(go), UnityEngine.Random.Range(0.0f, m_totalTime / 2));
        }
    }

    public void OpenMiniGame()
    {
        m_miniGameCanvas.SetActive(true);
        m_inGame = true;
        m_totalTime = m_player.PlayerData.time * (1 - m_agentData.Awarness);
        StartCoroutine(Countdown(m_totalTime));
        m_player.inControl = false;
        SpawnObjects();
    }

    public void CloseMiniGame()
    {
        if (m_agentData != null) m_agentData.Awarness += m_agentData.Awarness * 0.5f;
        ClearAll();
        m_miniGameHook.transform.localPosition = m_hookStickPos;

        m_miniGameCanvas.SetActive(false);
        m_inGame = false;
        m_player.inControl = true;
        m_activeObjects.Clear();
    }

    private IEnumerator Countdown(float time)
    {
        float duration = time;

        float normalizedTime = 0;
        while (normalizedTime <= 1f && m_inGame)
        {
            countdownImage.fillAmount = 1 - normalizedTime;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        CloseMiniGame();
    }

    void ClearAll()
    {
        List<GameObject> removals = new List<GameObject>();
        foreach (KeyValuePair<GameObject, float> entry in m_objectsToSpawn)
        {
            removals.Add(entry.Key);
            m_activeObjects.Add(entry.Key);
        }

        for (int i = removals.Count - 1; i >= 0; i--)
        {
            m_objectsToSpawn.Remove(removals[i]);
            removals.RemoveAt(i);
        }

        foreach (GameObject go in m_activeObjects)
        {
            Destroy(go);
        }


    }
}
