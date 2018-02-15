using UnityEngine;

namespace AntonioHR.FSMMoveExamples.Inputs
{
    public class SimplePlayerInput : IPlayerInput
    {
        public Vector2 Axes { get; private set; }
        

        public void Update()
        {
            Axes = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
    }
}