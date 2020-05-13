using UnityEngine;

public class PlantTexture : MonoBehaviour
{
    private Renderer m_Renderer;
    public Texture[] m_Textures;
    private int m_Index;

    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        m_Index = Random.Range(0, m_Textures.Length);
        m_Renderer.material.SetTexture("_BaseMap", m_Textures[m_Index]);
    }
}
