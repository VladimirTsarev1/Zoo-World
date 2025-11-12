namespace UI.EatenAnimalsCounters
{
    public sealed class EatenAnimalsCountersPresenter
    {
        private readonly EatenAnimalsCountersModel _model;
        private readonly EatenAnimalsCountersView _view;

        public EatenAnimalsCountersPresenter(EatenAnimalsCountersModel model, EatenAnimalsCountersView view)
        {
            _model = model;
            _view = view;

            model.EatenAmountsChanged += HandleEatenAmountsChanged;
            
            model.Reset();
        }

        private void HandleEatenAmountsChanged(int eatenPreysAmount, int eatenPredatorsAmount)
        {
            _view.SetCounters(eatenPreysAmount, eatenPredatorsAmount);
        }
    }
}