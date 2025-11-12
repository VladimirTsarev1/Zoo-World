using UnityEngine;

namespace Animals.Move.Jump
{
    public class JumpMoveStrategy : IMoveStrategy
    {
        private float _distance;
        private float _delay;
        private float _timeSinceLastJump;

        public JumpMoveStrategy(float distance, float delay)
        {
            _distance = distance;
            _delay = delay;
            _timeSinceLastJump = 0f;
        }

        public void Move(Rigidbody rb)
        {
            _timeSinceLastJump += Time.fixedDeltaTime;

            if (_timeSinceLastJump < _delay)
            {
                return;
            }

            _timeSinceLastJump = 0f;

            Vector3 jumpDirection = rb.transform.forward.normalized;
            Vector3 jumpVector = jumpDirection * _distance + Vector3.up * (_distance * 0.5f);

            var velocity = rb.linearVelocity;
            velocity.y = 0;
            rb.linearVelocity = velocity;

            rb.AddForce(jumpVector, ForceMode.VelocityChange);
        }
    }
}