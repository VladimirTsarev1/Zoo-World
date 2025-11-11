using UnityEngine;

namespace Factory
{
    public class UniversalFactory : IFactory
    {
        public GameObject CreateObject(GameObject prefab, Transform parent)
        {
            return Object.Instantiate(prefab, parent);
        }
    }
}