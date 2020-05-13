using UnityEngine;

public class PlantRotate : MonoBehaviour
{
    public float m_MaxRotate = 90.0f;
    public float m_RotateSmooth = 0.7f;
    public Vector3 m_Axis = Vector3.forward;

    private float m_StartTime;
    private Vector3 m_StartRotate;

    private void Start()
    {
        m_StartRotate = transform.eulerAngles;
        m_StartTime = Random.Range(0.0f, 1000.0f);
    }

    private void Update()
    {
        var rotate = m_StartRotate + m_Axis * (Mathf.Sin((m_StartTime + Time.time) * m_RotateSmooth) * m_MaxRotate);
        transform.eulerAngles = rotate;
    }
}
