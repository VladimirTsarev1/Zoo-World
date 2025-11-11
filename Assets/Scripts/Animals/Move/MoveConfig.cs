using UnityEngine;

namespace Animals.Move
{
    public abstract class MoveConfig : ScriptableObject
    {
        public abstract IMoveStrategy CreateStrategy();
    }
}