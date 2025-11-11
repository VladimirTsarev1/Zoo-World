using UnityEngine;

namespace Animals.Move
{
    public class LinearMoveStrategy : IMoveStrategy
    {
        private readonly float _speed;

        public LinearMoveStrategy(float speed)
        {
            _speed = speed;
        }

        public void Move(Rigidbody rb)
        {
            rb.linearVelocity = rb.transform.forward * (_speed * Time.fixedDeltaTime);
        }
    }
}