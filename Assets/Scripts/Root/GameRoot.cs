using Services.CameraBounds;
using Services.Pools;
using UnityEngine;

namespace Root
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private void Awake()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
            Services.ServiceLocator.PoolService = new PoolService();
            Services.ServiceLocator.CameraBounds = new CameraBoundsService(mainCamera);
        }
    }
}