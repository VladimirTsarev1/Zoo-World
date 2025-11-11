using Pool.Configs;
using UnityEngine;

namespace Animals.Configs
{
    public class AnimalConfig : ScriptableObject
    {
        [field: SerializeField] public PoolKeyConfig PoolKeyConfig { get; private set; }
        [field: SerializeField] public AnimalType AnimalType { get; private set; }
    }
}