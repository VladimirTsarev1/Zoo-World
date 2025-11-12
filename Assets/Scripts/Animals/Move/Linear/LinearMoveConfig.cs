using UnityEngine;

namespace Animals.Move.Linear
{
    [CreateAssetMenu(fileName = "LinearMoveConfig", menuName = "ScriptableObject/MoveConfig/LinearMoveConfig")]
    public sealed class LinearMoveConfig : MoveConfig
    {
        [field: SerializeField] public float Speed { get; private set; }

        public override IMoveStrategy CreateStrategy()
        {
            return new LinearMoveStrategy(Speed);
        }
    }
}