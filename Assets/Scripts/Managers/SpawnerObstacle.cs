using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerObstacle : MonoBehaviour
{
    public GameObject m_Obstacle;
    public int m_ObstacleAmount = 6;
    private List<GameObject> m_ObstaclesPooled = new List<GameObject>();

    public Transform m_TopLimiter;
    public Transform m_BottomLimiter;

    public List<GameObject> ActiveObstacles => m_ObstaclesPooled.Where(x => x.activeInHierarchy).ToList();

    private void Start()
    {
        for (int i = 0; i < m_ObstacleAmount; i++)
        {
            GameObject go = (GameObject)Instantiate(m_Obstacle);
            go.SetActive(false);
            m_ObstaclesPooled.Add(go);
        }
    }

    public void DisableAll()
    {
        for (int i = 0; i < m_ObstaclesPooled.Count; i++)
        {
            m_ObstaclesPooled[i].SetActive(false);
        }
    }

    private GameObject NextObject()
    {
        for (int i = 0; i < m_ObstaclesPooled.Count; i++)
        {
            if (!m_ObstaclesPooled[i].activeInHierarchy)
                return m_ObstaclesPooled[i];
        }

        return null;
    }

    public void Spawn(float speed)
    {
        GameObject go = NextObject();

        if (go == null)
            return;

        Vector3 position = m_BottomLimiter.position;
        position.y = Random.Range(m_BottomLimiter.position.y, m_TopLimiter.position.y);

        go.transform.position = position;
        go.transform.rotation = transform.rotation;
        go.SetActive(true);

        var script = go.GetComponent<Obstacle>();
        script.enabled = true;
        script.m_Speed = speed;
    }
}
