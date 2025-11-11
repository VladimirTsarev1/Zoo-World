using UnityEngine;

namespace Animals.Move
{
    [CreateAssetMenu(fileName = "JumpMoveConfig", menuName = "ScriptableObject/MoveConfig/JumpMoveConfig")]
    public class JumpMoveConfig : MoveConfig
    {
        [field: SerializeField] public float Distance { get; private set; }

        public override IMoveStrategy CreateStrategy()
        {
            return new JumpMoveStrategy(Distance);
        }
    }
}