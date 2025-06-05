using System;
using UnityEngine;
using UnityEngine.UI;

namespace Attention.View
{
    public class UI_State : UI_Base
    {
        public override ViewType Type => ViewType.State;

        [SerializeField] private Button _exitBtn;
        [SerializeField] private Slider happinessSlider;
        [SerializeField] private Slider bondSlider;
        [SerializeField] private Slider fullnessSlider;
        [SerializeField] private Slider cleanlinessSlider;

        public void Init(Action onClickExit)
        {
            _exitBtn.onClick.AddListener(() => onClickExit());
        }

        public void SetSlider(float happiness, float bond, float fullness, float cleanliness)
        {
            happinessSlider.value = happiness / 100;
            bondSlider.value = bond / 100;
            fullnessSlider.value = fullness / 100;
            cleanlinessSlider.value = cleanliness / 100;
        }
    }
}