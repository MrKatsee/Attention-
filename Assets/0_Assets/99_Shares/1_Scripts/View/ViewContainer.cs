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

        public ViewContainer(IViewPresenterContainer viewPresenters)
        {
            _viewPresenters = viewPresenters;

            _view = new Dictionary<ViewType, IView>();
            _activeViews = new List<ViewType>();
            _inactiveViews = new List<ViewType>();

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

            if (_view.TryGetValue(type, out IView view))
            {
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

        public void DeactivateView(ViewType type)
        {
            if (!_activeViews.Contains(type)) { return; }

            if (_view.TryGetValue(type, out IView view))
            {
                _activeViews.Remove(type);
                _inactiveViews.Add(type);

                IViewPresenter presenter = _viewPresenters.GetPresenter(view.GetType());
                presenter.OnDeactivateView();
            }
        }
    }
}