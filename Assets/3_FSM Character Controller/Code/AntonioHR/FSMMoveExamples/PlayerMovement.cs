
using AntonioHR.FSMMoveExamples.Movements;

namespace AntonioHR.FSMMoveExamples
{
    public class PlayerMovement
    {

        private IAxisMovement xAxisMode;
        private IAxisMovement yAxisMode;
        private Player owner;


        public PlayerMovement(Player owner)
        {
            this.owner = owner;
        }


        public IAxisMovement XAxisMode
        {
            get { return xAxisMode; }
            set
            {
                xAxisMode = value;
                xAxisMode.Setup(Axis2.X, owner.Body);
            }
        }
        public IAxisMovement YAxisMode
        {
            get { return yAxisMode; }
            set
            {
                yAxisMode = value;
                yAxisMode.Setup(Axis2.Y, owner.Body);
            }
        }



        public void Update()
        {
            xAxisMode.Update();
            yAxisMode.Update();
        }

        public void Simulate()
        {
            xAxisMode.Simulate();
            yAxisMode.Simulate();
        }
    }
}