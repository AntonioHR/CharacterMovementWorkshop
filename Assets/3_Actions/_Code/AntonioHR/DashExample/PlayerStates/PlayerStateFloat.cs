namespace AntonioHR.DashExample.PlayerStates
{
    public class PlayerStateFloat : PlayerState
    {
        public PlayerStateFloat(DashingPlayer player) : base(player)
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
            if(player.AreFeetOnGround && !player.IsAscending)
                player.State = new PlayerStateGrounded(player);
            else if (player.CanDash && player.PlayerInput.DashPressed)
                player.DoDash();
        }
    }
}
