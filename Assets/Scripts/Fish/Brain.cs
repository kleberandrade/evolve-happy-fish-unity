using UnityEngine;

public class Brain : MonoBehaviour
{
    public float m_Fitness;
    public float[] m_Inputs = new float[2];
    public float[] m_Chromosome = new float[2]; 

    private Fish m_Fish;

    private void Start()
    {
        m_Fish = GetComponent<Fish>();
    }

    public bool Jump()
    {
        float sum = 0.0f;
        for (int i = 0; i < m_Inputs.Length; i++)
            sum += m_Inputs[i] * m_Chromosome[i];

        return sum >= 0.5f;
    }

    private void Update()
    {
        m_Inputs[0] = GameManager.Instance.Obstacle.position.x - transform.position.x;
        m_Inputs[1] = GameManager.Instance.Obstacle.position.y - transform.position.y;

        m_Fish.Jump = Jump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.Survivors--;
        m_Fitness = GameManager.Instance.ElapsedTime;
        m_Fish.Die();
    }
}
