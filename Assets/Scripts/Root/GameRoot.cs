using Animals;
using Pool.Service;
using Services;
using Services.CameraBounds;
using UnityEngine;

namespace Root
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private void Awake()
        {
            InitializeServices();

            InitializeAnimalSpawner();
        }

        private void InitializeServices()
        {
            ServiceLocator.PoolService = new PoolService();
            ServiceLocator.CameraBounds = new CameraBoundsService(mainCamera);
        }

        private void InitializeAnimalSpawner()
        {
            var animalSpawner = new AnimalSpawner(ServiceLocator.PoolService);
        }
    }
}