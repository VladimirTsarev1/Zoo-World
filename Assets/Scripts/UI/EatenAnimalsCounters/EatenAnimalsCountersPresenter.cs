using System;
using ZooWorld.Animals;
using ZooWorld.UI.EatenAnimalsCounters.View;

namespace ZooWorld.UI.EatenAnimalsCounters
{
    public sealed class EatenAnimalsCountersPresenter
    {
        private readonly EatenAnimalsCountersService _service;
        private readonly IEatenAnimalsCountersView _view;

        public EatenAnimalsCountersPresenter(EatenAnimalsCountersService service, IEatenAnimalsCountersView view)
        {
            _service = service;
            _view = view;

            service.AmountChanged += HandleAmountChanged;

            foreach (AnimalType type in Enum.GetValues(typeof(AnimalType)))
            {
                _view.SetAmount(type, _service.GetAmount(type));
            }
        }

        private void HandleAmountChanged(AnimalType type, int amount)
        {
            _view.SetAmount(type, amount);
        }
    }
}