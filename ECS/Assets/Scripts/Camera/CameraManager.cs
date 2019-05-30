using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CameraManager
{
  public Camera m_Camera;
  public Vector3 m_CameraBounds;

  public void Initialize()
  {
    m_Camera = Camera.main;
    m_CameraBounds = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
  }
}
