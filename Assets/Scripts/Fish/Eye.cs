using UnityEngine;

public class Eye : MonoBehaviour
{
    [Header("Renderer")]
    public Renderer[] m_Renderers;
    public Texture[] m_Textures;

    [Header("Blink Time")]
    public float m_MinOpenTime = 2.0f;
    public float m_MaxOpenTime = 6.0f;
    public float m_MinCloseTime = 0.2f;
    public float m_MaxCloseTime = 1.0f;

    private float m_BlinkTime;
    private int m_TextureIndex;

    private void Start()
    {
        m_BlinkTime = Time.time + Random.Range(m_MinOpenTime, m_MaxOpenTime);
    }

    public void Revive()
    {
        m_TextureIndex = 0;
    }

    public void Die()
    {
        m_TextureIndex = 2;
        ChangeTexture(m_Textures[m_TextureIndex]);
    }

    private void Update()
    {
        if (m_TextureIndex == 2)
            return;


        if (Time.time > m_BlinkTime)
        {
            if (m_TextureIndex == 0)
            {
                m_TextureIndex = 1;
                m_BlinkTime = Time.time + Random.Range(m_MinCloseTime, m_MaxCloseTime);
            }
            else
            {
                m_TextureIndex = 0;
                m_BlinkTime = Time.time + Random.Range(m_MinOpenTime, m_MaxOpenTime);
            }

            ChangeTexture(m_Textures[m_TextureIndex]);
        }
    }

    private void ChangeTexture(Texture texture)
    {
        foreach (Renderer r in m_Renderers)
            r.material.SetTexture("_BaseMap", texture);
    }
}