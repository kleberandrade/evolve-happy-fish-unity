using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float m_Speed = 1.0f;
    public Vector3 m_Direction = -Vector3.right;
    public float m_Limit = -10.0f;

    private void Update()
    {
        transform.Translate(m_Direction * m_Speed * Time.deltaTime);
        if (transform.position.x < m_Limit)
        {
            gameObject.SetActive(false);
        }
    }
}
