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

    public class OnEnterItemEvnet : IViewEvent
    {
        public ItemData ItemData { get; private set; }

        public OnEnterItemEvnet(ItemData itemData)
        {
            ItemData = itemData;
        }
    }

    public class OnExitItemEvnet : IViewEvent
    {

    }
}