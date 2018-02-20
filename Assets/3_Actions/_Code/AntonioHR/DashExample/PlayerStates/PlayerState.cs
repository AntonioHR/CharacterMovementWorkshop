namespace AntonioHR.DashExample.PlayerStates
{

    public abstract class PlayerState
    {

        protected DashingPlayer player;

        public PlayerState(DashingPlayer player)
        {
            this.player = player;
        }

        public abstract void Update();
        public abstract void FixedUpdate();

        public abstract void OnEnter();
        public abstract void OnLeave();
    }
}
