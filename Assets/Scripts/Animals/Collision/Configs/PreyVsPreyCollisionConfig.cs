using UnityEngine;

namespace Animals.Collision.Configs
{
    [CreateAssetMenu(fileName = "PreyVsPreyCollisionConfig",
        menuName = "ScriptableObject/CollisionConfig/PreyVsPreyCollisionConfig")]
    public sealed class PreyVsPreyCollisionConfig : AnimalCollisionConfig
    {
        [field: SerializeField] public float PushForce { get; private set; } = 3f;

        public override AnimalType AnimalTypeA { get; protected set; } = AnimalType.Prey;
        public override AnimalType AnimalTypeB { get; protected set; } = AnimalType.Prey;

        public override void HandleCollision(Animal animalA, Animal animalB,
            UnityEngine.Collision collision)
        {
            Vector3 direction = (animalA.transform.position - animalB.transform.position).normalized;

            animalA.Push(direction * PushForce, ForceMode.Impulse);
            animalB.Push(-direction * PushForce, ForceMode.Impulse);
        }
    }
}