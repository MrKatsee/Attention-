using Attention.View;

namespace Attention.Main
{
    public class ObjectLoader
    {
        private ViewLoader _viewLoader;
        private EntityLoader _entityLoader;

        public ObjectLoader(ViewContainer viewContainer, EntityContainer entityContainer)
        {
            _viewLoader = new ViewLoader(viewContainer);
            _entityLoader = new EntityLoader(entityContainer);
        }

        public void Update()
        {
            _viewLoader.Update();
            _entityLoader.Update();
        }
    }
}