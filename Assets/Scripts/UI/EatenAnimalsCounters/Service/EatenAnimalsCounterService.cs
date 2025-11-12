using Animals;

namespace UI.EatenAnimalsCounters.Service
{
    public class EatenAnimalsCounterService : IEatenAnimalsCounterService
    {
        private readonly EatenAnimalsCountersView _view;
        private readonly EatenAnimalsCountersModel _model;
        private readonly EatenAnimalsCountersPresenter _presenter;

        public EatenAnimalsCounterService(EatenAnimalsCountersView view)
        {
            _model = new EatenAnimalsCountersModel();
            _presenter = new EatenAnimalsCountersPresenter(_model, view);
        }

        public void AnimalEaten(Animal eatenAnimal)
        {
            _model.AddEatenAnimal(eatenAnimal);
        }
    }
}