//using Attention.Main.EventModule;
//using Attention.View;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Util;

//namespace Attention
//{
//    public abstract class ButtonPresenter<T> : ViewPresenter<T> where T : UI_Button
//    {
//        protected override void OnInitializeView()
//        {
//            View.SetButtonListener(
//                () => {
//                    OnClick();
//                });
//        }

//        public abstract void OnClick();
//    }
//}
