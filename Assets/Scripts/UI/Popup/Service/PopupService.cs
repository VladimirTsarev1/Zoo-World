using UnityEngine;
using ZooWorld.Pool.Configs;
using ZooWorld.Pool.Service;

namespace ZooWorld.UI.Popup.Service
{
    public sealed class PopupService : IPopupService
    {
        private readonly IPoolService _poolService;
        private readonly PoolKeyConfig _labelPoolKey;

        public PopupService(IPoolService poolService, PoolKeyConfig labelPoolKey)
        {
            _poolService = poolService;
            _labelPoolKey = labelPoolKey;
        }

        public void SpawnPopupLabel(Vector3 pos)
        {
            var labelObject = _poolService.Get<PopupLabel>(_labelPoolKey);
            labelObject.Setup(pos);
        }
    }
}