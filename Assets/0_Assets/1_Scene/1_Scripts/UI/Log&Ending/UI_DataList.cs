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

        public void Init(Action ExitAction)
        {
            exitButton.onClick.AddListener(() => { ExitAction(); });
        }

        public void Set(List<(Guid, CatData)> datas, Action<Guid> CallDataAction)
        {
            List<DataBlock> block = parent.GetComponentsInChildren<DataBlock>().ToList<DataBlock>();
            for (int i = 0; i < block.Count; i++)
            {
                if (i < datas.Count)
                {
                    block[i].Init(datas[i].Item1, datas[i].Item2.name, datas[i].Item2.startTime, CallDataAction);
                }
                else
                {
                    block[i].Init();
                }
            }
        }
    }
}