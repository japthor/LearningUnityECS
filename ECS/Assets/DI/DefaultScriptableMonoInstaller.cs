using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DefaultScriptableMonoInstaller", menuName = "Installers/DefaultScriptableMonoInstaller")]
public class DefaultScriptableMonoInstaller : ScriptableObjectInstaller<DefaultScriptableMonoInstaller>
{
  public HelicopterEnemy m_EnemySettings;

  public override void InstallBindings()
  {
    Container.BindInstance(m_EnemySettings);
  }
}