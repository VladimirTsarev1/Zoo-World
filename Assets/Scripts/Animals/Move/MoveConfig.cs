using UnityEngine;

namespace ZooWorld.Animals.Move
{
    public abstract class MoveConfig : ScriptableObject
    {
        public abstract IMoveStrategy CreateStrategy();
    }
}