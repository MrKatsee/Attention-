using System.Collections.Generic;
using Util;

namespace Attention.View
{
    public interface IViewLoader
    {
        public void ActivateView(ViewType type);
        public void DeactivateView(ViewType type);
    }

    [DIPublisher]
    public class ViewLoader : IViewLoader
    {
        private ViewContainer _viewContainer;

        private Queue<ViewType> _activateQueue;
        private Queue<ViewType> _deactivateQueue;

        public ViewLoader(ViewContainer viewContainer)
        {
            _viewContainer = viewContainer;

            _activateQueue = new Queue<ViewType>();
            _deactivateQueue = new Queue<ViewType>();

            DI.Register(this);
        }

        public void ActivateView(ViewType type)
        {
            if (_activateQueue.Contains(type)) { return; }

            _activateQueue.Enqueue(type);
        }
        public void DeactivateView(ViewType type)
        {
            if (_deactivateQueue.Contains(type)) { return; }

            _deactivateQueue.Enqueue(type);
        }

        public void Update()
        {
            while (_activateQueue.Count > 0)
            {
                _viewContainer.ActivateView(_activateQueue.Dequeue());
            }
            while (_deactivateQueue.Count > 0)
            {
                _viewContainer.DeactivateView(_deactivateQueue.Dequeue());
            }
        }
    }
}