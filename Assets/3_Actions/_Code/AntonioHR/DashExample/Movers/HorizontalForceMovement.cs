using AntonioHR.Utils;
using System;
using UnityEngine;


namespace AntonioHR.DashExample.Movers
{
    public class HorizontalForceMovement
    {
        [Serializable]
        public class Settings
        {
            public static float StoppingThreshold = 1f;

            public float moveForce = 5.0f;
            public float groundBrakeForce = 4.0f;
            public float speedCap = 5f;
            public float speedCapFrictionPower = 5f;
        }

        internal void Move(object horizontalAxisInput)
        {
            throw new NotImplementedException();
        }

        private Settings settings;

        private Rigidbody2D rb;
        private float input;
        private Vector2 moveForceVec;


        private Vector2 Velocity { get { return rb.velocity; } }
        private float BrakeForce { get { return settings.groundBrakeForce; } }
        



        public HorizontalForceMovement(Settings settings, Rigidbody2D body)
        {
            this.settings = settings;
            this.rb = body;
        }
        

        public void Move(float horizontalMove)
        {
            this.input = horizontalMove;
            //input = InputHelper.DefaultJoystickInputRawCapped;

            AddHorizontalMoveForce();
            ApplyFriction();

            rb.AddForce(moveForceVec);
        }




        private void AddHorizontalMoveForce()
        {
            moveForceVec = Vector2.right * input * settings.moveForce;
        }
        private void ApplyFriction()
        {
            var xMag = Mathf.Abs(rb.velocity.x);

            if (xMag < Settings.StoppingThreshold && input == 0)
            {
                FullStopHorizontal();
            }
            else
            {
                ApplyHorizontalBrake();
            }

            if (xMag > settings.speedCap)
            {
                ApplyHorizontalSpeedCap();
            }
        }
        private void ApplyHorizontalSpeedCap()
        {
            float force = -Mathf.Sign(rb.velocity.x) * settings.speedCapFrictionPower;
            moveForceVec += Vector2.right * force;
        }
        private void FullStopHorizontal()
        {
            rb.velocity = rb.velocity.WithX(0);
        }
        private void ApplyHorizontalBrake()
        {
            if (input == 0)
            {
                float force = -Mathf.Sign(rb.velocity.x) * BrakeForce;
                moveForceVec += Vector2.right * force;
            }

        }
    }
}