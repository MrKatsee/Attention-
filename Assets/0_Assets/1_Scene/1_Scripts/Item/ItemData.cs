using Attention.Data;

namespace Attention
{
    public class ItemData
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }

        public string Description { get; private set; }

        public enum ItemType { expendable, furniture, clothes }
        public ItemType Type { get; private set; }

        public float Happiness { get; private set; }    // 행복함
        public float Bond { get; private set; }         // 유대감
        public float Fullness { get; private set; }     // 포만감
        public float Cleanliness { get; private set; }  // 청결도

        public int RemainStock { get; private set; } // 최대 구매 개수 (-1이면 무제한 구매 가능)

        public string EntitySpone { get; private set; }

        public ItemData(int index, string name, float price, float happiness, float bond, float fullness, float cleanliness, string description = "")
        {
            Index = index;
            Name = name;
            Price = price;
            Type = ItemType.expendable;

            Happiness = happiness;
            Bond = bond;
            Fullness = fullness;
            Cleanliness = cleanliness;

            RemainStock = -1;

            EntitySpone = "";

            Description = description;
        }

        public ItemData(int index, string name, float price, ItemType type, float happiness, float bond, float fullness, float cleanliness)
        {
            Index = index;
            Name = name;
            Price = price;
            Type = type;

            Happiness = happiness;
            Bond = bond;
            Fullness = fullness;
            Cleanliness = cleanliness;

            RemainStock = -1;

            EntitySpone = "";
        }

        public ItemData(int index, string name, float price, ItemType type, float happiness, float bond, float fullness, float cleanliness, int remainStock, string entitySpone, string description = "") : this(index, name, price, type, happiness, bond, fullness, cleanliness)
        {
            RemainStock = remainStock;
            EntitySpone = entitySpone;
            Description = description;
        }

        public void purchase()
        {
            RemainStock--;
        }
    }
}