namespace AntonioHR.Utils
{
    public interface ICooldown
    {
        bool IsOver { get; }
        float Progress { get; }
    }
    public class DummyCooldown : ICooldown
    {
        public bool IsOver
        {
            get
            {
                return true;
            }
        }

        public float Progress
        {
            get
            {
                return 1;
            }
        }
    }

}
