using Attention.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Attention
{
    public class UI_Store : UI_Base
    {
        public override ViewType Type => ViewType.Store;

        public void exitButton()
        {
            this.gameObject.SetActive(false);
        }
    }
}