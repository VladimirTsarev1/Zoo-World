namespace Animals.Collision
{
    public struct AnimalCollisionResults
    {
        public AnimalCollisionResultType ResultForOriginal;
        public AnimalCollisionResultType ResultForAnother;

        public AnimalCollisionResults(
            AnimalCollisionResultType resultForOriginal,
            AnimalCollisionResultType resultForAnother)
        {
            ResultForOriginal = resultForOriginal;
            ResultForAnother = resultForAnother;
        }
    }
}