using UnityEngine;

namespace Odometr
{
    public class Window : MonoBehaviour
    {
        public virtual void ActivateWindow()
        {
            gameObject.SetActive(true);
        }

        public virtual void DeactivateWindow()
        {
            gameObject.SetActive(false);
        }
    }
}