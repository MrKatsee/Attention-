using Attention.Main.EventModule;
using Attention.View;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Util;

namespace Attention
{
    public class UI_Create : UI_Base
    {

        [SerializeField] private RectTransform _UI;

        public override ViewType Type => ViewType.Create_Cat_Panel;
    }
}