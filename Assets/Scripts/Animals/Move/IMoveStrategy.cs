using UnityEngine;

namespace Animals.Move
{
    public interface IMoveStrategy
    {
        public void Move(Rigidbody rb);
    }
}