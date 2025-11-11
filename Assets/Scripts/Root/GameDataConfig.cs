using UnityEngine;

namespace Root
{
    [CreateAssetMenu(fileName = "GameDataConfig", menuName = "ScriptableObject/GameDataConfig")]
    public class GameDataConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 TimeToSpawnAnimals { get; private set; }
    }
}