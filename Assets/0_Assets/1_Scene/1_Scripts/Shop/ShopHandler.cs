using Attention.Data;
using Attention.Main.EventModule;
using Attention.Process;
using Util;

namespace Attention
{
    [DISubscriber]
    public class ShopHandler : ILogicEventHandler
    {
        [Inject(typeof(EventBus))] private IEventQueue _eventQueue;
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
            if (_playerDataContainer.GetMoney() < itemData.Price || itemData.RemainStock == 0) { return; }
            itemData.purchase();

            float price = itemData.Price;

            _eventQueue.EnqueueLogicEvent(new CurrentCatItemUseEvent(itemData));
            if(itemData.Type != ItemData.ItemType.expendable)
            {
                float shareMoney = _playerDataContainer.GetSharedMoney();
                if (shareMoney > price)
                {
                    _playerDataContainer.SubtractSharedMoney(price);
                    price = 0;
                }
                else
                {
                    price -= shareMoney;
                    _playerDataContainer.SetMoney(0);
                }
                _eventQueue.EnqueueLogicEvent(new CreateEntityEvent(itemData.EntitySpone));
            }
            _playerDataContainer.SubtractMoney(price);

            _eventQueue.EnqueueViewEvent(new UpdateMoneyEvent());
        }
    }
}