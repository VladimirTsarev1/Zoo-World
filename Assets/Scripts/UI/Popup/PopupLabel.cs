using Pool;
using UnityEngine;

namespace UI.Popup
{
    public sealed class PopupLabel : BasePooledObject
    {
        public void Setup(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}