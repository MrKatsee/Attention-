using Attention.Process;
using Attention.View;
using Util;

namespace Attention
{
    [DISubscriber]
    public class ShopControlHandler : ILogicEventHandler
    {
        [Inject(typeof(ViewLoader))] private IViewLoader _viewContainer;

        public ShopControlHandler()
        {
            DI.Register(this);
        }

        public void OpenStore(OpenStoreEvent data)
        {
            _viewContainer.ActivateView(ViewType.Shop);
        }
    }
}