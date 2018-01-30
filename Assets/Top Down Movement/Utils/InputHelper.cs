using UnityEngine;

namespace AntonioHR.MovementExamples
{
    public static class InputHelper
    {
        public static Vector2 DefaultJoystickInput { get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); } }

        public static Vector2 DefaultJoystickInputRawCapped {
            get
            {
                var i = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                return i.sqrMagnitude > 1 ? i.normalized : i;
            }
        }
    }
}
