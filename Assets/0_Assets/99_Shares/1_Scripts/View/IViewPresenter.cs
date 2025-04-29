namespace Attention.View
{
    public interface IViewPresenter
    {
        void InitializeView(IView view);

        void OnActivateView();
        void OnDeactivateView();
    }

    public abstract class ViewPresenter<T> : IViewPresenter, IViewEventHandler where T : IView
    {
        protected T View { get; private set; }

        public void InitializeView(IView view)
        {
            View = (T)view;

            OnInitializeView();
        }

        /// <summary>
        /// View가 첫 생성될 때만 호출되는 초기화 함수.
        /// </summary>
        protected virtual void OnInitializeView() { }

        /// <summary>
        /// View가 Activate될 때마다 호출되는 초기화 함수.
        /// </summary>
        public virtual void OnActivateView() { }

        /// <summary>
        /// View가 Deactivate될 때마다 호출되는 초기화 함수.
        /// </summary>
        public virtual void OnDeactivateView() { }
    }
}