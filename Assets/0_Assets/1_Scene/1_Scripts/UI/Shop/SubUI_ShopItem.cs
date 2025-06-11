using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace Attention.View
{
    public class SubUI_ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _imgItem;
        [SerializeField] private Text _txtName;
        [SerializeField] private Text _txtPrice;
        [SerializeField] private Button _btn;

        private Action _onEnter;
        private Action _onExit;

        public void Init(Action onClick, Action onEnter, Action onExit)
        {
            _btn.onClick.AddListener(() => onClick());
            _onEnter = onEnter;
            _onExit = onExit;   
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onExit.Invoke();
        }

        public void SetData(ItemData itemData)
        {
            _imgItem.sprite = Resources.Load<Sprite>("Sprites/Shop/" + itemData.Index);
            _txtName.text = itemData.Name;
            _txtPrice.text = itemData.Price.ToString() + "원";
        }
    }
}