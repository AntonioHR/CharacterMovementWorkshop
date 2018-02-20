using AntonioHR.Utils;
using System;
using UnityEngine;

namespace AntonioHR.DashExample.PlayerStates
{
    public class PlayerStateDash : PlayerState
    {
        [Serializable]
        public class Settings
        {
            public float dashCooldown = .5f;
            public float dashTime = .5f;
            public float dashDistance = 1;

            public float DashVelocity { get { return dashDistance / dashTime; } }
        }

        private Settings configs;
        private Vector2 direction;
        private float startTime;

        private SimpleTimer timer;


        public PlayerStateDash(DashingPlayer player, Settings configs, Vector2 dashDirection) : base(player)
        {
            this.direction = dashDirection.normalized;
            this.configs = configs;
        }

        public override void FixedUpdate()
        {
            player.Body.velocity = direction * configs.DashVelocity;
        }

        public override void OnEnter()
        {
            timer = new SimpleTimer(configs.dashTime);
        }

        public override void OnLeave()
        {
            player.StartDashCooldown();
        }

        public override void Update()
        {
            if(timer.IsOver)
            {
                player.Body.velocity = Vector2.zero;
                player.State = new PlayerStateFloat(player);
            }
        }
    }
}
