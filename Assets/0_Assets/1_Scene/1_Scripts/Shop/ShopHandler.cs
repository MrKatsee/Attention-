using Attention.Data;
using Attention.Process;
using Util;

namespace Attention
{
    [DISubscriber]
    public class ShopHandler : ILogicEventHandler
    {
        [Inject] private ShopDataContainer _shopDataContainer;
        [Inject] private PlayerDataContainer _playerDataContainer;

        public ShopHandler()
        {
            DI.Register(this);
        }

        public void InitShop(GameStartEvent data)
        {
            _shopDataContainer.Init();
        }

        public void BuyItem(BuyItemEvent data)
        {
            int id = data.ID;

            ItemData itemData = _shopDataContainer.GetItemData(id);
            _playerDataContainer.SubtractMoney(itemData.Price);
        }
    }
}