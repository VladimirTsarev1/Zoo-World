using UnityEngine;

namespace ZooWorld.Animals.Move
{
    public interface IMoveStrategy
    {
        public void Move(Rigidbody rb);
    }
}