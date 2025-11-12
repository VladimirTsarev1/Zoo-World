using UnityEngine;

namespace Animals.Collision.Configs
{
    [CreateAssetMenu(fileName = "PreyVsPreyCollisionConfig",
        menuName = "ScriptableObject/CollisionConfig/PreyVsPreyCollisionConfig")]
    public class PreyVsPreyCollisionConfig : AnimalCollisionConfig
    {
        [field: SerializeField] public float PushForce { get; private set; } = 3f;

        public override AnimalType AnimalTypeA { get; protected set; } = AnimalType.Prey;
        public override AnimalType AnimalTypeB { get; protected set; } = AnimalType.Prey;

        public override AnimalCollisionResults HandleCollision(Animal animalA, Animal animalB,
            UnityEngine.Collision collision)
        {
            Vector3 direction = (animalA.transform.position - animalB.transform.position).normalized;

            animalA.Push(direction * PushForce, ForceMode.Impulse);
            animalB.Push(-direction * PushForce, ForceMode.Impulse);

            return new AnimalCollisionResults(AnimalCollisionResultType.Pushed, AnimalCollisionResultType.Pushed);
        }
    }
}