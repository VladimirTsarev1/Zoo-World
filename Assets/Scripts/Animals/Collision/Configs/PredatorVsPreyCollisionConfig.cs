using UnityEngine;

namespace Animals.Collision.Configs
{
    [CreateAssetMenu(fileName = "PredatorVsPreyCollisionConfig",
        menuName = "ScriptableObject/CollisionConfig/PredatorVsPreyCollisionConfig")]
    public class PredatorVsPreyCollisionConfig : AnimalCollisionConfig
    {
        public override AnimalType AnimalTypeA { get; protected set; } = AnimalType.Predator;
        public override AnimalType AnimalTypeB { get; protected set; } = AnimalType.Prey;

        public override AnimalCollisionResults HandleCollision(Animal animalA, Animal animalB,
            UnityEngine.Collision collision)
        {
            if (animalA.Config.AnimalType == AnimalType.Predator)
            {
                return new AnimalCollisionResults(AnimalCollisionResultType.Ate, AnimalCollisionResultType.Eaten);
            }

            return new AnimalCollisionResults(AnimalCollisionResultType.Eaten, AnimalCollisionResultType.Ate);
        }
    }
}