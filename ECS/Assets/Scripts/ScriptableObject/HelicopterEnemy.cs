using UnityEngine;
using Unity.Mathematics;

[CreateAssetMenu(fileName = "New HelicopterEnemy", menuName = "Enemy")]
public class HelicopterEnemy : ScriptableObject
{
  public float m_MinSpeed;
  public float m_MaxSpeed;
  public Mesh m_Mesh;
  public Material m_Material;
  public int m_MaximumAmount;
  [HideInInspector] public float3 m_Direction;

  public void RandomDirection()
  {
    m_Direction = new float3(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 0.0f);
  }
}
