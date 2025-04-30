using Attention.Main.EventModule;
using Attention.View;
using System;
using System.Collections.Generic;

namespace Attention.View
{
    public interface IViewPresenterContainer
    {
        IViewPresenter GetPresenter(Type type);
    }
}

namespace Attention.Main.EventModule
{
    public class ViewPresenterContainer : EventHandlerContainer<IViewEvent>, IViewPresenterContainer
    {
        private Dictionary<Type, IViewPresenter> _presenters;

        protected override void OnInitializeHandlers(IEnumerable<IEventHandler<IViewEvent>> eventHandlers)
        {
            _presenters = new Dictionary<Type, IViewPresenter>();

            foreach (var handler in eventHandlers)
            {
                var presenterType = handler.GetType().BaseType.GenericTypeArguments[0];
                _presenters.Add(presenterType, (IViewPresenter)handler);
            }
        }

        public IViewPresenter GetPresenter(Type type)
        {
            return _presenters.TryGetValue(type, out IViewPresenter presenter) ? presenter : null;
        }
    }
}