using UnityEngine;
using Zenject;

public class DefaultMonoInstaller : MonoInstaller
{
  public override void InstallBindings()
  {
   Container.Bind<CameraManager>().AsSingle();
  }
}