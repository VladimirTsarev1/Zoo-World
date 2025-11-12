using UnityEngine;

namespace Animals.Collision.Configs
{
    [CreateAssetMenu(fileName = "PredatorVsPredatorCollisionConfig",
        menuName = "ScriptableObject/CollisionConfig/PredatorVsPredatorCollisionConfig")]
    public class PredatorVsPredatorCollisionConfig : AnimalCollisionConfig
    {
        public override AnimalType AnimalTypeA { get; protected set; } = AnimalType.Predator;
        public override AnimalType AnimalTypeB { get; protected set; } = AnimalType.Predator;

        public override void HandleCollision(Animal animalA, Animal animalB,
            UnityEngine.Collision collision)
        {
            if (Random.Range(0f, 1f) >= 0.5f)
            {
                animalA.Ate(animalB);
                animalB.WasEaten(animalA);

                return;
            }

            animalB.Ate(animalA);
            animalA.WasEaten(animalB);
        }
    }
}