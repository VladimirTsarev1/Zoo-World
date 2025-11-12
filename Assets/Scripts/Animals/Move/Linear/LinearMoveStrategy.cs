using UnityEngine;

namespace Animals.Move.Linear
{
    public sealed class LinearMoveStrategy : IMoveStrategy
    {
        private readonly float _speed;

        private Vector3 _velocity;

        public LinearMoveStrategy(float speed)
        {
            _speed = speed;
        }

        public void Move(Rigidbody rb)
        {
            _velocity = rb.transform.forward * _speed;
            _velocity.y = rb.linearVelocity.y;

            rb.linearVelocity = _velocity;
        }
    }
}