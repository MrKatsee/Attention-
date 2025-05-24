namespace Attention.Data
{
    public class PlayerDataContainer
    {
        private float _money;

        public PlayerDataContainer()
        {
            _money = 1000f;
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