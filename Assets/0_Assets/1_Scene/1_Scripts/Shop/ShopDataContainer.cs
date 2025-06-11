using System;
using System.Collections.Generic;
using Util;

namespace Attention.Data
{
    public interface IShopDataContainer
    {
        IEnumerable<ItemData> GetAllItemDatas();
    }

    [DIPublisher]
    public class ShopDataContainer : IShopDataContainer
    {
        private Dictionary<int, ItemData> _shopItems;   // Key: id

        public ShopDataContainer()
        {
            DI.Register(this);
        }

        public void Init()
        {
            _shopItems = new Dictionary<int, ItemData>();

            // 귀찮으니까 걍 하드코딩
            RegisterItemData(new ItemData(0, "사료", 20f, 0f, -1f, 2f, 0f));
            RegisterItemData(new ItemData(1, "고급 사료", 35f, 0f, 0f, 3f, 0f));
            RegisterItemData(new ItemData(2, "최고급 사료", 50f, 1f, 0f, 4f, 0f));
            RegisterItemData(new ItemData(10, "간식", 30f, 2f, 1f, 1f, 0f));
            RegisterItemData(new ItemData(11, "고급 간식", 45f, 3f, 2f, 1f, 0f));
            RegisterItemData(new ItemData(12, "최고급 간식", 60f, 4f, 3f, 1f, 0f));
            RegisterItemData(new ItemData(20, "푸른 색 침대", 100f, ItemData.ItemType.furniture, 4f, 3f, 1f, 0f, 1, "CatBedBlue"));
        }

        public ItemData GetItemData(int id)
        {
            return _shopItems[id];
        }

        private void RegisterItemData(ItemData itemData)
        {
            _shopItems.Add(itemData.Index, itemData);
        }

        public IEnumerable<ItemData> GetAllItemDatas()
        {
            return _shopItems.Values;
        }
    }
}