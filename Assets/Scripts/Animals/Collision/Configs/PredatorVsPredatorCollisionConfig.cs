using UnityEngine;

namespace Animals.Collision.Configs
{
    [CreateAssetMenu(fileName = "PredatorVsPredatorCollisionConfig",
        menuName = "ScriptableObject/CollisionConfig/PredatorVsPredatorCollisionConfig")]
    public class PredatorVsPredatorCollisionConfig : AnimalCollisionConfig
    {
        public override AnimalType AnimalTypeA { get; protected set; } = AnimalType.Predator;
        public override AnimalType AnimalTypeB { get; protected set; } = AnimalType.Predator;

        public override AnimalCollisionResults HandleCollision(Animal animalA, Animal animalB,
            UnityEngine.Collision collision)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                return new AnimalCollisionResults(AnimalCollisionResultType.Ate, AnimalCollisionResultType.Eaten);
            }

            return new AnimalCollisionResults(AnimalCollisionResultType.Eaten, AnimalCollisionResultType.Ate);
        }
    }
}