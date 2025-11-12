using TMPro;
using UnityEngine;

namespace UI.EatenAnimalsCounters
{
    public class EatenAnimalsCountersView : MonoBehaviour
    {
        [SerializeField] private TMP_Text eatenPreysText;
        [SerializeField] private TMP_Text eatenPredatorsText;

        public void SetCounters(int predatorsAmount, int preyAmount)
        {
            eatenPreysText.text = predatorsAmount.ToString();
            eatenPredatorsText.text = preyAmount.ToString();
        }
    }
}