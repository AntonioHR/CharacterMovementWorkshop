using UnityEngine;

namespace AntonioHR.Utils
{
    public static class InputHelper
    {
        public static Vector2 DefaultJoystickInput { get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); } }

        public static Vector2 DefaultJoystickInputRawNotCapped
        {
            get
            {
                var i = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                return i.sqrMagnitude > 1 ? i.normalized : i;
            }
        }
        public static Vector2 DefaultJoystickInputRawCapped {
            get
            {
                var i = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                return i.sqrMagnitude > 1 ? i.normalized : i;
            }
        }
    }
}
