using UnityEngine;
using UnityEngine.UI;

namespace Odometr
{
    public class NumberSlot : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollView;

        public void SetNumber(float value)
        {
            scrollView.verticalScrollbar.value = 1 - value / 10f;
        }
    }
}