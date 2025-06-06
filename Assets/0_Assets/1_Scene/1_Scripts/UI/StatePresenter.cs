using Attention.Data;
using Util;

namespace Attention.View
{
    [DISubscriber]
    public class StatePresenter : ViewPresenter<UI_State> 
    {
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;
        [Inject] private CatDataContainer _catDataContainer;

        public StatePresenter() 
        {
            DI.Register(this);
        }

        protected override void OnInitializeView()
        {
            View.Init(OnExitClick);
        }

        private void OnExitClick()
        {
            _viewContainer.DeactivateView(ViewType.State);
        }

        public void OnStateUpdate(CurrentCatStateViewEvent _event)
        {
            State data = _catDataContainer.GetCurrentState();
            View.SetSlider(data.Happiness, data.Bond, data.Fullness, data.Cleanliness);
        }
    }
}