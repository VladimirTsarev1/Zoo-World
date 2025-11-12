using Pool.Configs;
using Pool.Service;
using UnityEngine;

namespace UI.PopupService
{
    public class PopupService : IPopupService
    {
        private readonly IPoolService _poolService;
        private PoolKeyConfig _labelPoolKey;

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