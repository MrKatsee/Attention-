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
            RegisterItemData(new ItemData(0, "사료", 200f, 0f, 0f, 30f, 0f, "맛있는 사료입니다"));
            RegisterItemData(new ItemData(10, "간식", 200f, 15f, 10f, 15f, 0f, "맛있는 간식은 행복감을 느끼게 하죠."));
            RegisterItemData(new ItemData(5, "청결 도구", 300f, -5f, 0f, 0f, 30f, "고양이를 깨끗하게 만들어보세요!"));
            RegisterItemData(new ItemData(4, "낚시대", 300f, 30f, 30f, 0f, 0f, "고양이의 본능을 깨워보세요!"));
            RegisterItemData(new ItemData(20, "푸른 색 침대", 1000f, ItemData.ItemType.furniture, 0f, 0f, 0f, 0f, 1, "CatBedBlue", "푹신한 침대입니다."));
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