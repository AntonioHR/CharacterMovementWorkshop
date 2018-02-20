namespace AntonioHR.DashExample.PlayerStates
{
    class PlayerStateGrounded : PlayerState
    {
        public PlayerStateGrounded(DashingPlayer player) : base(player)
        {
        }

        public override void FixedUpdate()
        {
            player.DoDefaultMoveFixedUpdate();
        }

        public override void OnEnter()
        {
        }

        public override void OnLeave()
        {
        }

        public override void Update()
        {
            if (player.PlayerInput.JumpPressed)
                player.DoDefaultJumpAndTransitionToFloat();
            else if (player.CanDash && player.PlayerInput.DashPressed)
                player.DoDash();
            else if (!player.AreFeetOnGround)
                player.State = new PlayerStateFloat(player);
        }
    }
}
