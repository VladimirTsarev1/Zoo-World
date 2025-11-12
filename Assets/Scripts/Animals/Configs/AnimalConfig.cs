using Animals.Move;
using Pool.Configs;
using UnityEngine;

namespace Animals.Configs
{
    public abstract class AnimalConfig : ScriptableObject
    {
        [field: SerializeField] public PoolKeyConfig PoolKeyConfig { get; private set; }
        [field: SerializeField] public MoveConfig MoveConfig { get; private set; }
        [field: SerializeField] public AnimalType AnimalType { get; private set; }
    }
}