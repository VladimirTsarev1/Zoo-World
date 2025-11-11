using UnityEngine;

namespace Animals.Move
{
    public interface IMoveBehavior
    {
        public void Move(Rigidbody rb);
    }
}