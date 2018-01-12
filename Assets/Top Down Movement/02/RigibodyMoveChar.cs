using AntonioHR.MovementExamples;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigibodyMoveChar : MonoBehaviour {
    Rigidbody2D rb;

    public float moveForce = 5.0f;
    public bool doBrake = false;

    public BrakeSettings brakeSettings;

    public class BrakeSettings
    {
        public float stoppingThreshold = 0.001f;
        public float breakForceMag = 4.0f;
    }


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(InputHelper.DefaultJoystickInputRaw * moveForce, ForceMode2D.Force);

        if (doBrake)
        {
            //rb.AddForce()
        }
    }
}
