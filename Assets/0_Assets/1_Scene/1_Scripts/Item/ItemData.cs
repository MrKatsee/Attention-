namespace Attention
{
    public class ItemData
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public float Price { get; private set; }

        public float Happiness { get; private set; }    // 행복함
        public float Bond { get; private set; }         // 유대감
        public float Fullness { get; private set; }     // 포만감
        public float Cleanliness { get; private set; }  // 청결도

        public ItemData(int index, string name, float price, float happiness, float bond, float fullness, float cleanliness)
        {
            Index = index;
            Name = name;
            Price = price;

            Happiness = happiness;
            Bond = bond;
            Fullness = fullness;
            Cleanliness = cleanliness;
        }
    }
}