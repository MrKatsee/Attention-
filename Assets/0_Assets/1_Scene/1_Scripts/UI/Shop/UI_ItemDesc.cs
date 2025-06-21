
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View {
    public class UI_ItemDesc : UI_Base
    {
        public override ViewType Type => ViewType.ItemDesc;

        [SerializeField] private Text _nameText; 
        [SerializeField] private Text _descText; 

        public void Init(ItemData itemData)
        {
            _nameText.text = itemData.Name;
            _descText.text =
            $" {itemData.Description}\n" +
            $"<color=#FF69B4> 행복함: {(int)itemData.Happiness}</color>\n" +
            $"<color=#1E90FF> 유대감: {(int)itemData.Bond}</color>\n" +
            $"<color=#32CD32> 포만감: {(int)itemData.Fullness}</color>\n" +
            $"<color=#DAA520> 청결도: {(int)itemData.Cleanliness}</color>";
        }
    }


}


