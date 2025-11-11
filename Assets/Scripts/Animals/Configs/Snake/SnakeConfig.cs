using UnityEngine;

namespace Animals.Configs.Snake
{
    [CreateAssetMenu(fileName = "SnakeConfig", menuName = "ScriptableObject/AnimalConfig/SnakeConfig")]
    public class SnakeConfig : AnimalConfig
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;
    }
}