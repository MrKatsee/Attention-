using Attention.View;

namespace Attention.Main
{
    public class ObjectLoader
    {
        private ViewLoader _viewLoader;

        public ObjectLoader(ViewContainer viewContainer)
        {
            _viewLoader = new ViewLoader(viewContainer);
        }

        public void Update()
        {
            _viewLoader.Update();
        }
    }
}