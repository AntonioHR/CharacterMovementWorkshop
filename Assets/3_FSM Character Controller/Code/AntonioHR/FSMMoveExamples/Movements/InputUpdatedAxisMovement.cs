using AntonioHR.FSMMoveExamples.Inputs;
using UnityEngine;

namespace AntonioHR.FSMMoveExamples.Movements
{
    public class InputUpdatedAxisMovement : IAxisMovement
    {
        private IIntentionalAxisMovement child;
        private IPlayerInput input;

        private Axis2 axis;

        public InputUpdatedAxisMovement(IIntentionalAxisMovement child, IPlayerInput input)
        {
            this.child = child;
            this.input = input;
        }


        public void Setup(Axis2 axis, Rigidbody2D body)
        {
            this.axis = axis;
            child.Setup(axis, body);
        }

        public void Simulate()
        {
            child.Simulate();
        }

        public void Update()
        {
            child.MovementIntention = input.Axes.AxisValue(axis);
            child.Update();
        }
    }
}
