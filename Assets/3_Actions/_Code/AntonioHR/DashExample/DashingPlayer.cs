using AntonioHR.DashExample.Movers;
using AntonioHR.DashExample.PlayerStates;
using AntonioHR.Utils;
using System;
using UnityEngine;

namespace AntonioHR.DashExample
{
    public class DashingPlayer : MonoBehaviour
    {
        #region Helper Data Classes
        [Serializable]
        public class ContactChecker
        {
            public static float DefaultCheckRadius = .1f;
            public Vector2 localPosition;
            public LayerMask checkLayers;
        }

        [Serializable]
        public class Settings
        {
            public float jumpForce = 2;

            public HorizontalForceMovement.Settings walkSettings;
            public PlayerStateDash.Settings dashSettings;
            public PlayerInput.Settings inputSettings;
        }
        #endregion

        #region Inspector Variables
        public ContactChecker feet;
        public Settings settings;
        #endregion



        private PlayerState state;
        private ICooldown dashCooldown = new DummyCooldown();




        public PlayerState State
        {
            get
            {
                return state;
            }
            set
            {
                state.OnLeave();
                this.state = value;
                state.OnEnter();
            }
        }

        public PlayerInput PlayerInput { get; private set; }
        public HorizontalForceMovement DefaultHorizontalMove { get; private set; }
        public Rigidbody2D Body { get; private set; }
        public float HorizontalDirection { get; private set; }


        public bool AreFeetOnGround
        {
            get
            {
                var layers = feet.checkLayers.value;
                var radius = ContactChecker.DefaultCheckRadius;
                var objectsOnFoot = Physics2D.OverlapCircleAll(FeetInWorldSpace, radius, layers);
                foreach (var obj in objectsOnFoot)
                {
                    if (obj.gameObject != gameObject)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool IsAscending
        {
            get
            {
                return Body.velocity.y > 0;
            }
        }
        public bool CanDash { get { return dashCooldown.IsOver; } }




        #region Unity Functions
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            DefaultHorizontalMove = new HorizontalForceMovement(settings.walkSettings, Body);
            PlayerInput = new PlayerInput(settings.inputSettings);

            state = new PlayerStateFloat(this);
            HorizontalDirection = 1;
        }
        private void Update()
        {
            UpdateDirection();
            state.Update();
        }

        private void FixedUpdate()
        {
            state.FixedUpdate();
        }
        #endregion


        public void StartDashCooldown()
        {
            dashCooldown = new SimpleTimer(settings.dashSettings.dashCooldown);
        }

        public void DoDash()
        {
            Debug.Assert(CanDash);
            var directionVector = Vector2.right * HorizontalDirection;
            State = new PlayerStateDash(this, settings.dashSettings, directionVector);
        }

        public void DoDefaultMoveFixedUpdate()
        {
            DefaultHorizontalMove.Move(PlayerInput.HorizontalMove);
        }
        
        public void DoDefaultJumpAndTransitionToFloat()
        {
            Body.AddForce(Vector2.up * settings.jumpForce, ForceMode2D.Impulse);
            State = new PlayerStateFloat(this);
        }



        #region Helpers
        private Vector3 FeetInWorldSpace
        {
            get
            {
                return transform.TransformPoint(feet.localPosition);
            }
        }


        private void UpdateDirection()
        {
            float x = PlayerInput.HorizontalMove;
            if (x != 0)
                HorizontalDirection = x > 0 ? 1 : -1;
        }
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(FeetInWorldSpace, ContactChecker.DefaultCheckRadius);
        }
    }
}