using UnityEngine;

namespace Factory
{
    public interface IFactory
    {
        GameObject CreateObject(GameObject prefab, Transform parent);
    }
}