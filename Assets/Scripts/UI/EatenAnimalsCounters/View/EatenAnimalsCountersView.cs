using TMPro;
using UnityEngine;
using ZooWorld.Animals;

namespace ZooWorld.UI.EatenAnimalsCounters.View
{
    [DisallowMultipleComponent]
    public sealed class EatenAnimalsCountersView : MonoBehaviour, IEatenAnimalsCountersView
    {
        [SerializeField] private TMP_Text eatenPreysText;
        [SerializeField] private TMP_Text eatenPredatorsText;

        public void SetAmount(AnimalType type, int amount)
        {
            TMP_Text counterText = null;

            switch (type)
            {
                case AnimalType.Prey:
                    counterText = eatenPreysText;
                    break;
                case AnimalType.Predator:
                    counterText = eatenPredatorsText;
                    break;
            }

            if (counterText == null)
            {
                Debug.LogWarning($"There is no counter for {type}");
                return;
            }

            counterText.text = amount.ToString();
        }
    }
}