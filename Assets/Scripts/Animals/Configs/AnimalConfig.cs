using UnityEngine;
using ZooWorld.Animals.Move;
using ZooWorld.Pool.Configs;

namespace ZooWorld.Animals.Configs
{
    public abstract class AnimalConfig : ScriptableObject
    {
        [field: SerializeField] public PoolKeyConfig PoolKeyConfig { get; private set; }
        [field: SerializeField] public MoveConfig MoveConfig { get; private set; }
        [field: SerializeField] public AnimalType AnimalType { get; private set; }
    }
}