using AntonioHR.FSMMoveExamples.Inputs;
using AntonioHR.FSMMoveExamples.Movements;
using UnityEngine;
namespace AntonioHR.FSMMoveExamples
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public ForceCappedAxisMovement.Configs Configs;



        private PlayerMovement movement;
        private IPlayerInput input;



        public Rigidbody2D Body { get { return GetComponent<Rigidbody2D>(); } }

        public void SetXAxisMovement(IAxisMovement move)
        {
            movement.XAxisMode = move;
        }
        public void SetYAxisMovement(IAxisMovement move)
        {
            movement.YAxisMode = move;
        }
        


        private void Awake()
        {
            input = new SimplePlayerInput();

            movement = new PlayerMovement(this);
            movement.XAxisMode = new InputUpdatedAxisMovement(new ForceCappedAxisMovement(Configs), input);
            movement.YAxisMode = new InputUpdatedAxisMovement(new ForceCappedAxisMovement(Configs), input);
        }
        private void Update()
        {
            input.Update();
            movement.Update();
        }
        private void FixedUpdate()
        {
            movement.Simulate();
        }
    }
}