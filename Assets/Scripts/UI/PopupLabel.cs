using Pool;
using UnityEngine;

namespace UI
{
    public class PopupLabel : BasePooledObject
    {
        public void Setup(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}