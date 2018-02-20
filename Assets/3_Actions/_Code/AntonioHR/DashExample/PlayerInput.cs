using AntonioHR.Utils;
using System;
using UnityEngine;

namespace AntonioHR.DashExample
{
    public class PlayerInput 
    {
        [Serializable]
        public class Settings
        {
            public KeyCode JumpKey;
            public KeyCode DashKey;
        }
        private Settings settings;

        public PlayerInput (Settings settings)
        {

            this.settings = settings;
        }

        public bool JumpPressed { get { return Input.GetKeyDown(settings.JumpKey); } }
        public bool DashPressed { get { return Input.GetKeyDown(settings.DashKey); } }

        public float HorizontalMove { get { return InputHelper.DefaultJoystickInputRawCapped.x; } }
    }
}
