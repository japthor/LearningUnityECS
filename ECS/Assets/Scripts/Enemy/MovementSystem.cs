using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class MovementSystem : ComponentSystem
{
  protected override void OnUpdate()
  {
    // Moves throught the entities which has a translation and a movementcomponent system
    Entities.ForEach((ref Translation translation, ref MovementComponent movement) => {

      Movement(ref translation, ref movement);

    });
  }

  // Movement of the entity
  private void Movement(ref Translation translation, ref MovementComponent movement)
  {
    translation.Value += (movement.m_Speed * movement.m_Direction) * Time.deltaTime;
    CameraBoundariesCollision(ref translation, ref movement);
  }
  // Checks if the entity has collides with the camera boundaries.
  private void CameraBoundariesCollision(ref Translation translation, ref MovementComponent movement)
  {
    if (translation.Value.y >= movement.m_YBoundaries[1] || translation.Value.y <= movement.m_YBoundaries[0])
      movement.m_Direction.y = ChangeDirection(movement.m_Direction.y);

    if (translation.Value.x >= movement.m_XBoundaries[1] || translation.Value.x <= movement.m_XBoundaries[0])
      movement.m_Direction.x = ChangeDirection(movement.m_Direction.x);
  }
  // Moves to the opposite direction when it collides with the boundaries of the camera
  private float ChangeDirection(float value)
  {
    if(value >= 0)
      return -Mathf.Abs(value);

    return Mathf.Abs(value);
  }
}
