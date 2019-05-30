using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;
using Zenject;

public class EntitiesManager : MonoBehaviour
{
  private HelicopterEnemy m_HelicopterEnemy;
  private CameraManager m_CameraManager;
  private EntityManager m_EntityManager;

  [Zenject.Inject]
  private void Constructor(CameraManager cm, HelicopterEnemy he)
  {
    m_CameraManager = cm;
    m_HelicopterEnemy = he;

    m_CameraManager.Initialize();
    m_EntityManager = World.Active.EntityManager;
  }

  private void Start()
  {
    Archetype();
  }

  // Creates the entities.
  private void Archetype()
  {
    // Create the entity Type.
    EntityArchetype entityArchetype = m_EntityManager.CreateArchetype
    (
        typeof(Translation),
        typeof(RenderMesh),
        typeof(LocalToWorld),
        typeof(MovementComponent)
    );

    // Array of entities.
    NativeArray<Entity> entityArray = new NativeArray<Entity>(m_HelicopterEnemy.m_MaximumAmount, Allocator.Temp);
    // Adds the entities and creates them.
    m_EntityManager.CreateEntity(entityArchetype, entityArray);
    // Setup the values of the entities.
    SetUpEntities(entityArray);
    entityArray.Dispose();
  }

  // Add to each entity component their values.
  private void SetUpEntities(NativeArray<Entity> entities)
  {
    for (int i = 0; i < entities.Length; i++)
    {
      // Adds a initial random direction.
      m_HelicopterEnemy.RandomDirection();

      // Adds the values to the MovementComponent script.
      m_EntityManager.SetComponentData(entities[i], new MovementComponent
      {
            m_Speed = UnityEngine.Random.Range(m_HelicopterEnemy.m_MinSpeed, m_HelicopterEnemy.m_MaxSpeed),

            m_YBoundaries = new float2(-m_CameraManager.m_CameraBounds.y + m_HelicopterEnemy.m_Mesh.bounds.size.y,
                                        m_CameraManager.m_CameraBounds.y - m_HelicopterEnemy.m_Mesh.bounds.size.y),

            m_XBoundaries = new float2(-m_CameraManager.m_CameraBounds.x + m_HelicopterEnemy.m_Mesh.bounds.size.x,
                                        m_CameraManager.m_CameraBounds.x - m_HelicopterEnemy.m_Mesh.bounds.size.x),

            m_Direction = new float3(m_HelicopterEnemy.m_Direction)

      });
      // Sets the initial translation.
      m_EntityManager.SetComponentData(entities[i], new Translation
      {
            Value = new float3(UnityEngine.Random.Range(
                               -m_CameraManager.m_CameraBounds.x + m_HelicopterEnemy.m_Mesh.bounds.size.x,
                               m_CameraManager.m_CameraBounds.x - m_HelicopterEnemy.m_Mesh.bounds.size.x),
                               UnityEngine.Random.Range(
                               -m_CameraManager.m_CameraBounds.y + m_HelicopterEnemy.m_Mesh.bounds.size.y,
                               m_CameraManager.m_CameraBounds.y - m_HelicopterEnemy.m_Mesh.bounds.size.y),
                               0.0f)

      });
      // Sets the mesh.
      m_EntityManager.SetSharedComponentData(entities[i], new RenderMesh
      {
        mesh = m_HelicopterEnemy.m_Mesh,
        material = m_HelicopterEnemy.m_Material,
      });
    }
  }
}
