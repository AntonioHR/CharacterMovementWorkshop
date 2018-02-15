using UnityEngine;

namespace AntonioHR.FSMMoveExamples.Movements
{
    class BlankAxisMovement : IAxisMovement
    {
        Rigidbody2D body;
        Axis2 axis;

        public void Setup(Axis2 axis, Rigidbody2D body)
        {
            this.body = body;
            this.axis = axis;
        }

        public void Simulate()
        {
            body.velocity = body.velocity.WithAxisValue(0, axis);
        }

        public void Update()
        {
        }
    }
}