using UnityEngine;

namespace AntonioHR.Utils
{
    public class SimpleTimer : ICooldown
    {
        private float start;
        private float duration;

        public SimpleTimer(float duration)
        {
            start = Time.time;
            this.duration = duration;
        }
        public bool IsOver { get { return ElapsedTime >= duration; } }
        public float Progress { get { return ElapsedTime / duration; } }
        public float ElapsedTime { get { return Time.time - start; } }
    }
}
