using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention
{
    public class CreatePresenter : ViewPresenter<UI_Create>
    {
        public void close(CloseCreateCatUIEvent _event)
        {
            Debug.Log("CloseCreateCatUIEvent!!");
            View.gameObject.SetActive(false);
        }
    }
}