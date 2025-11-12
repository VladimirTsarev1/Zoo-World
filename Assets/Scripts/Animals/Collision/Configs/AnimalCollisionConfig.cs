using UnityEngine;

namespace Animals.Collision.Configs
{
    public abstract class AnimalCollisionConfig : ScriptableObject
    {
        public abstract AnimalType AnimalTypeA { get; protected set; }
        public abstract AnimalType AnimalTypeB { get; protected set; }

        public abstract AnimalCollisionResults HandleCollision(
            Animal animalA,
            Animal animalB,
            UnityEngine.Collision collision);
    }
}