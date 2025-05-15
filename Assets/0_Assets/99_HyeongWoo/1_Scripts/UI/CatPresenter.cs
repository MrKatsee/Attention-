using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention.View
{
    public class CatPresenter : ViewPresenter<UI_Cat>
    {
        public void MatchCatImage(MatchCatImageEvent _event)
        {
            View.SetData(_event._catData);
        }
    }
}