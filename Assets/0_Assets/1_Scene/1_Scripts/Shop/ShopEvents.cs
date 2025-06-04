namespace Attention
{
    public class BuyItemEvent : ILogicEvent
    {
        public int ID;

        public BuyItemEvent(int id)
        {
            ID = id;
        }
    }
}