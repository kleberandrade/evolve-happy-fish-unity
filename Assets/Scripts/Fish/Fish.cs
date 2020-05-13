using UnityEngine;

public class Fish : MonoBehaviour
{
    public float m_Force = 8.0f;
    public float m_Gravity = -6.0f;
    public float m_Speed = 15.0f;
    
    private Rigidbody m_Body;
    private Animator m_Animator;
    private Eye m_Eye;

    private Collider m_Collider;
    public bool Jump { get; set; }

    private void Awake()
    {
        m_Eye = GetComponent<Eye>();
        m_Body = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Jump)
        {
            Jump = false;
            m_Body.velocity = Vector3.zero;
            m_Body.AddForce(Vector3.up * m_Force, ForceMode.Impulse);
        }
        else
        {
            m_Body.AddForce(Vector3.up * m_Gravity);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.right * m_Speed + m_Body.velocity);
    }

    public void Die()
    {
        m_Collider.enabled = false;
        m_Eye.Die();
        m_Animator.SetBool("Die", true);
        this.enabled = false;
    }
}

