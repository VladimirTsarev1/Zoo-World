namespace Animals.Collision
{
    public interface IAnimalCollisionService
    {
        public void HandleCollision(
            Animal originalAnimal,
            Animal collisionAnimal,
            UnityEngine.Collision collisionData);
    }
}