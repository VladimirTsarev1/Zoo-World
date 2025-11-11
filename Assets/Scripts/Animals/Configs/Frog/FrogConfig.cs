using UnityEngine;

namespace Animals.Configs.Frog
{
    [CreateAssetMenu(fileName = "FrogConfig", menuName = "ScriptableObject/AnimalConfig/FrogConfig")]
    public class FrogConfig : AnimalConfig
    {
        [field: SerializeField] public float JumpDistance { get; private set; } = 10f;
    }
}