using System;
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
    public class ViewContainer : IViewLoader
    {
        private IViewPresenterContainer _viewPresenters;

        private ViewFactory _viewFactory;

        private Dictionary<ViewType, IView> _view;
        private List<ViewType> _activeViews;
        private List<ViewType> _inactiveViews;

        private Queue<ViewType> _activateQueue;
        private Queue<ViewType> _deactivateQueue;

        public ViewContainer(IViewPresenterContainer viewPresenters)
        {
            _viewPresenters = viewPresenters;

            _view = new Dictionary<ViewType, IView>();
            _activeViews = new List<ViewType>();
            _inactiveViews = new List<ViewType>();

            _activateQueue = new Queue<ViewType>();
            _deactivateQueue = new Queue<ViewType>();

            DI.Register(this);
        }

        public void InitFactory()
        {
            _viewFactory = new ViewFactory();
            _viewFactory.Init();
        }

        public void ActivateView(ViewType type)
        {
            if (_activeViews.Contains(type)) { return; }
            if (_activateQueue.Contains(type)) { return; }

            _activateQueue.Enqueue(type);
        }

        public void DeactivateView(ViewType type)
        {
            if (!_activeViews.Contains(type)) { return; }
            if (_deactivateQueue.Contains(type)) { return; }

            _deactivateQueue.Enqueue(type);
        }

        public void Update()
        {
            while (_activateQueue.Count > 0)
            {
                ViewType type = _activateQueue.Dequeue();
                if (_view.TryGetValue(type, out IView view))
                {
                    if (view is UI_Base ui)
                    {
                        ui.gameObject.SetActive(true);
                    }

                    _activeViews.Add(type);
                    _inactiveViews.Remove(type);

                    IViewPresenter presenter = _viewPresenters.GetPresenter(view.GetType());
                    presenter.OnActivateView();
                }
                else
                {
                    view = _viewFactory.CreateView(type);

                    _view.Add(type, view);
                    _activeViews.Add(type);

                    IViewPresenter presenter = _viewPresenters.GetPresenter(view.GetType());
                    presenter.InitializeView(view);
                    presenter.OnActivateView();
                }
            }

            while (_deactivateQueue.Count > 0)
            {
                ViewType type = _deactivateQueue.Dequeue();
                if (_view.TryGetValue(type, out IView view))
                {
                    if (view is UI_Base ui)
                    {
                        ui.gameObject.SetActive(false);
                    }

                    _activeViews.Remove(type);
                    _inactiveViews.Add(type);

                    IViewPresenter presenter = _viewPresenters.GetPresenter(view.GetType());
                    presenter.OnDeactivateView();
                }
            }
        }
    }
}