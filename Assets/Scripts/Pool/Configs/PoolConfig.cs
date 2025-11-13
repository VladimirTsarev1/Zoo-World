using Pool.Core;
using UnityEngine;

namespace Pool.Configs
{
    [CreateAssetMenu(fileName = "NewPoolConfig", menuName = "ScriptableObject/PoolConfig")]
    public sealed class PoolConfig : ScriptableObject
    {
        [field: SerializeField] public PoolKeyConfig KeyConfig { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField, Min(0)] public int PrewarmAmount { get; private set; } = 10;

        [field: SerializeField] public PoolReleaseConditions ReleaseCondition { get; private set; } =
            PoolReleaseConditions.OnDisable;

        [field: SerializeField] public float TimeToRelease { get; private set; }
    }
}