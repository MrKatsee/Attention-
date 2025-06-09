using UnityEngine;
using UnityEngine.UI;

namespace Attention
{
    public class StatusLog : MonoBehaviour
    {
        [SerializeField] private Slider happinessSlider;
        [SerializeField] private Slider bondSlider;
        [SerializeField] private Slider fullnessSlider;
        [SerializeField] private Slider cleanlinessSlider;

        public void SetSlider(float happiness, float bond, float fullness, float cleanliness)
        {
            happinessSlider.value = happiness / 100;
            bondSlider.value = bond / 100;
            fullnessSlider.value = fullness / 100;
            cleanlinessSlider.value = cleanliness / 100;
        }
    }
}