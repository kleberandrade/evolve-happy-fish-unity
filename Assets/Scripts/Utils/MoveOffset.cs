using UnityEngine;

public class MoveOffset : MonoBehaviour 
{
    public float m_ScrollSpeed = 0.5f;
    public Vector2 m_Direction = -Vector2.right;
    private Renderer m_Renderer;
    
	private void Awake () 
    {
        m_Renderer = GetComponent<Renderer>();
	}

    private void LateUpdate () 
    {
        m_Renderer.material.SetTextureOffset("_BaseMap", m_Direction * m_ScrollSpeed * Time.time);
	}
}