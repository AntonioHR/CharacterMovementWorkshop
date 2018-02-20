namespace AntonioHR.Utils
{
    public interface ICooldown
    {
        bool IsOver { get; }
        float Progress { get; }
    }
    public class DummyCooldown : ICooldown
    {
        public static ICooldown OutOfCooldown { get { return new DummyCooldown(true); } }
        public static ICooldown OnCooldown { get { return new DummyCooldown(false); } }

        private DummyCooldown(bool isOver)
        {
            this.IsOver = isOver;
        }

        public bool IsOver { get; private set; }

        public float Progress
        {
            get
            {
                return IsOver ? 1 : 0;
            }
        }
    }

}
