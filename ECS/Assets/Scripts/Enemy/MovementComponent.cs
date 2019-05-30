using Unity.Entities;
using Unity.Mathematics;

public struct MovementComponent : IComponentData
{
  public float m_Speed;
  public float3 m_Direction;
  public float2 m_YBoundaries;
  public float2 m_XBoundaries;
}
