namespace Animals.Collision
{
    public interface IAnimalCollisionService
    {
        public AnimalCollisionResults HandleCollision(
            Animal originalAnimal,
            Animal collisionAnimal,
            UnityEngine.Collision collisionData);
    }
}