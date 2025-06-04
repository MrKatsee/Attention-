using Util;

namespace Attention.Data
{
    [DIPublisher]
    public class PlayerDataContainer
    {
        private float _money;

        public PlayerDataContainer()
        {
            _money = 1000f;

            DI.Register(this);
        }

        public void AddMoney(float money)
        {
            _money += money;
        }
        public void SubtractMoney(float money)
        {
            _money -= money;
        }

        public float GetMoney()
        {
            return _money;
        }
    }
}