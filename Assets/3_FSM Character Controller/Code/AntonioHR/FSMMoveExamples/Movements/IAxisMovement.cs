using UnityEngine;


namespace AntonioHR.FSMMoveExamples.Movements
{
    public interface IAxisMovement
    {
        void Setup(Axis2 axis, Rigidbody2D body);

        void Update();
        void Simulate();

    }
    public interface IIntentionalAxisMovement : IAxisMovement
    {
        float MovementIntention { get; set; }
    }
}