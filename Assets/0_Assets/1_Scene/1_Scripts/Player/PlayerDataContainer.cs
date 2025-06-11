using Util;

namespace Attention.Data
{
    public interface IPlayerDataContainer
    {
        float GetMoney();
        float GetSharedMoney();
    }

    [DIPublisher]
    public class PlayerDataContainer : IPlayerDataContainer
    {
        private float _money;
        private float _sharedMoney;

        public PlayerDataContainer()
        {
            _money = 1000f;
            _sharedMoney = 0f;

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

        public void SetSharedMoney(float sharedMoney)
        {
            _sharedMoney = sharedMoney;
        }

        public void AddSharedMoney(float sharedMoney)
        {
            _sharedMoney += sharedMoney;
        }

        public void SubtractSharedMoney(float sharedMoney)
        {
            _sharedMoney -= sharedMoney;
        }

        public float GetSharedMoney()
        {
            return _sharedMoney;
        }
    }
}