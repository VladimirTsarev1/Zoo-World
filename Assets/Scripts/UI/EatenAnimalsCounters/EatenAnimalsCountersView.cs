using TMPro;
using UnityEngine;

namespace UI.EatenAnimalsCounters
{
    [DisallowMultipleComponent]
    public sealed class EatenAnimalsCountersView : MonoBehaviour
    {
        [SerializeField] private TMP_Text eatenPreysText;
        [SerializeField] private TMP_Text eatenPredatorsText;

        public void SetCounters(int preyAmount, int predatorsAmount)
        {
            eatenPreysText.text = preyAmount.ToString();
            eatenPredatorsText.text = predatorsAmount.ToString();
        }
    }
}