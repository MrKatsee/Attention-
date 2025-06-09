using Attention.Data;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

namespace Attention.View
{
    public class UI_DataList : UI_Base
    {
        public override ViewType Type => ViewType.DataList;

        [SerializeField] private GameObject parent;
        [SerializeField] private Button exitButton;

        private Func<string, Sprite> spriteCallAction;

        public void Init(Action ExitAction, Func<string, Sprite> SpriteCallAction)
        {
            exitButton.onClick.AddListener(() => { ExitAction(); });
            this.spriteCallAction = SpriteCallAction;
        }

        public void Set(List<(Guid, CatData)> datas, Action<Guid> CallDataAction)
        {
            List<DataBlock> block = parent.GetComponentsInChildren<DataBlock>().ToList<DataBlock>();
            for (int i = 0; i < block.Count; i++)
            {
                if (i < datas.Count && !string.IsNullOrEmpty(datas[i].Item2.Ending))
                {
                    block[i].Init(datas[i].Item1, datas[i].Item2.name, datas[i].Item2.startTime, CallDataAction, spriteCallAction(datas[i].Item2.Ending));
                }
                else
                {
                    block[i].Init(spriteCallAction("BaseImage"));
                }
            }
        }
    }
}