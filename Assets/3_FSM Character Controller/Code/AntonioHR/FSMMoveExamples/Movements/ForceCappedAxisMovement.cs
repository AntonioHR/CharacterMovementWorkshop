using System;
using UnityEngine;

namespace AntonioHR.FSMMoveExamples.Movements
{
    public class ForceCappedAxisMovement : IIntentionalAxisMovement
    {
        [Serializable]
        public class Configs
        {
            public float speedCap;
            public float acceleration;
            public float speedCapAccel;
            public float brakeAccel;

            public const float ForceStopThreshold = 0.02f;
        }


        private Configs configs;
        private Axis2 axis;
        private Rigidbody2D body;

        private float accelResult;



        public float MovementIntention { get; set; }
        public bool MoveIntentionOpposesMovement
        {
            get
            {
                var movement = body.velocity.AxisValue(axis);
                return movement != 0 && MovementIntention * movement <= 0;
            }
        }
        public bool MoveIntentionMatchesMovement
        {
            get
            {
                var movement = body.velocity.AxisValue(axis);
                return MovementIntention * movement > 0;
            }
        }
        public bool ShouldForceStop { get { return MoveIntentionOpposesMovement && body.velocity.AxisValue(axis) <= Configs.ForceStopThreshold; } }

        public bool IsOverSpeedCap {
            get
            {
                return Mathf.Abs(body.velocity.AxisValue(axis)) > (configs.speedCap * Mathf.Abs(MovementIntention));
            }
        }

        public ForceCappedAxisMovement(Configs configs)
        {
            this.configs = configs;
        }



        public void Setup(Axis2 axis, Rigidbody2D body)
        {
            this.axis = axis;
            this.body = body;
        }

        public void Simulate()
        {
            if (ShouldForceStop)
            {
                FullStop();
            }
            else
            {
                AddMoveAccel();
                if (MoveIntentionOpposesMovement)
                    AddBrakeAccel();
                if (MoveIntentionMatchesMovement && IsOverSpeedCap)
                    AddSpeedCapAccel();

                Debug.Log(accelResult);
                body.AddForce(accelResult * axis.AsVector2() * body.mass);
            }
        }


        private void FullStop()
        {
            body.velocity = body.velocity.WithAxisValue(0, axis);
        }

        private void AddMoveAccel()
        {
            accelResult = MovementIntention * configs.acceleration;
        }

        private void AddBrakeAccel()
        {
            AddOpposingAccel(configs.brakeAccel);
        }

        private void AddSpeedCapAccel()
        {
            AddOpposingAccel(configs.speedCapAccel);
        }

        private void AddOpposingAccel(float scale)
        {
            var direction = Mathf.Sign(body.velocity.AxisValue(axis)) * -1;
            accelResult += direction * scale;
        }


        public void Update()
        {
        }

    }
}