namespace Attention.View
{
    public class TestPresenter : ViewPresenter<UI_Test>
    {
        public void Test(TestUIEvent data)
        {
            View.SetText(data.ScreenPosition.ToString());
        }
    }
}