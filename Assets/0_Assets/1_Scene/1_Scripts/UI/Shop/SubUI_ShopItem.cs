using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{
    public class SubUI_ShopItem : MonoBehaviour
    {
        [SerializeField] private Image _imgItem;
        [SerializeField] private Text _txtName;
        [SerializeField] private Text _txtPrice;
        [SerializeField] private Button _btn;

        public void Init(Action onClick)
        {
            _btn.onClick.AddListener(() => onClick());
        }

        public void SetData(ItemData itemData)
        {
            _imgItem.sprite = Resources.Load<Sprite>("Sprites/Shop/" + itemData.Index);
            _txtName.text = itemData.Name;
            _txtPrice.text = itemData.Price.ToString("F4") + "원";
        }
    }
}