using UnityEngine;

namespace Animals.Move.Jump
{
    [CreateAssetMenu(fileName = "JumpMoveConfig", menuName = "ScriptableObject/MoveConfig/JumpMoveConfig")]
    public class JumpMoveConfig : MoveConfig
    {
        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }

        public override IMoveStrategy CreateStrategy()
        {
            return new JumpMoveStrategy(Distance, Delay);
        }
    }
}