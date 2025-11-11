using UnityEngine;

namespace Animals.Move
{
    public class JumpMoveStrategy : IMoveStrategy
    {
        private readonly float _distance;

        public JumpMoveStrategy(float distance)
        {
            _distance = distance;
        }

        public void Move(Rigidbody rb)
        {
        }
    }
}