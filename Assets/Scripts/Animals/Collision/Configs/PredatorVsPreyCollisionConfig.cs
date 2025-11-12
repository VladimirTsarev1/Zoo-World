using UnityEngine;

namespace Animals.Collision.Configs
{
    [CreateAssetMenu(fileName = "PredatorVsPreyCollisionConfig",
        menuName = "ScriptableObject/CollisionConfig/PredatorVsPreyCollisionConfig")]
    public class PredatorVsPreyCollisionConfig : AnimalCollisionConfig
    {
        public override AnimalType AnimalTypeA { get; protected set; } = AnimalType.Predator;
        public override AnimalType AnimalTypeB { get; protected set; } = AnimalType.Prey;

        public override void HandleCollision(Animal animalA, Animal animalB,
            UnityEngine.Collision collision)
        {
            if (animalA.Config.AnimalType == AnimalType.Predator)
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