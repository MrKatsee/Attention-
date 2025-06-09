using Util;

namespace Attention.Data
{
    public interface IPlayerDataContainer
    {
        float GetMoney();
    }

    [DIPublisher]
    public class PlayerDataContainer : IPlayerDataContainer
    {
        private float _money;

        public PlayerDataContainer()
        {
            _money = 1000f;

            DI.Register(this);
        }

        public void SetMoney(float money)
        {
            _money = money;
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