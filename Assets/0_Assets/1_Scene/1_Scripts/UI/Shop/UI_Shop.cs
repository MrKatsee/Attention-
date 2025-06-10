using Attention.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{
    public class UI_Shop : UI_Base
    {
        public override ViewType Type => ViewType.Shop;

        [SerializeField] private Button _exitBtn;
        [SerializeField] private List<SubUI_ShopItem> _shopItemUIs;

        private Action<int> _onClickBuy;
        private Action<ItemData> _onEnterItem;
        private Action _onExitItem;

        public void Init(Action onClickExit, Action<int> onClickBuy, Action<ItemData> onEnterItem, Action onExitItem)
        {
            _onClickBuy = onClickBuy;
            _onEnterItem = onEnterItem;
            _onExitItem = onExitItem;

            _exitBtn.onClick.AddListener(() => onClickExit());
        }

        public void SetItemDatas(IEnumerable<ItemData> itemDatas)
        {
            int i = 0;
            foreach (ItemData itemData in itemDatas)
            {
                _shopItemUIs[i].gameObject.SetActive(true);

                _shopItemUIs[i].Init(
                    () => _onClickBuy.Invoke(itemData.Index),
                    () => _onEnterItem.Invoke(itemData),
                    () => _onExitItem.Invoke()
                    );
                _shopItemUIs[i].SetData(itemData);

                i++;
            }

            for (; i < _shopItemUIs.Count; i++)
            {
                _shopItemUIs[i].gameObject.SetActive(false);
            }
        }

        

    }
}