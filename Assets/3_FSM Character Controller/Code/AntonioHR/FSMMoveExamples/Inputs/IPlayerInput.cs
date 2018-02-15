using UnityEngine;

namespace AntonioHR.FSMMoveExamples.Inputs
{
    public interface IPlayerInput
    {
        Vector2 Axes { get; }

        void Update();
    }
}