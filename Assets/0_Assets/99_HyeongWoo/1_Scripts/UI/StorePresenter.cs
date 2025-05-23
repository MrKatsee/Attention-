using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Attention
{
    public class StorePresenter : ViewPresenter<UI_Store>
    {
        public void OnStore(OpenStoreViewEvent _event)
        {
            View.gameObject.SetActive(true);
        }

        public void CloseStore(CloseStoreEvent _event)
        {
            View?.gameObject.SetActive(false);
        }
    }
}
