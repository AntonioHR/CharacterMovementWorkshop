using AntonioHR.MovementExamples;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RigibodyMoveChar : MonoBehaviour {


	#region Inspector Variables
	public float moveForce = 5.0f;
	public bool useDynamicFriction = false;
    public bool useSpeedCapFriction = false;
    public bool useBraking = false;
    public FrictionSettings frictionSettings;

	[Serializable]
	public class FrictionSettings
	{
		public static float StoppingThreshold = 1f;
		public float brakeForce = 4.0f;
        public float dynamicFriction = 0.4f;
        public float speedCap = 5f;
        public float speedCapFrictionPower = 5f;
    }
    
    #endregion



    Rigidbody2D rb;
	Vector2 input;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		input = InputHelper.DefaultJoystickInputRawCapped;
		Move();
        if(useDynamicFriction || useBraking)
		{ 
			ApplyFriction();
		}
	}

	private void Move()
	{
		rb.AddForce(input * moveForce, ForceMode2D.Force);
	}

	private void ApplyFriction()
	{
		if (rb.velocity.magnitude < FrictionSettings.StoppingThreshold && input == Vector2.zero)
        {
            FullStop();
        }
        else if(useBraking)
        {
            ApplyBrake();
        }

        if (useDynamicFriction)
        {
            ApplyDynamicFriction();
        } else if(useSpeedCapFriction && rb.velocity.magnitude > frictionSettings.speedCap)
        {
            rb.AddForce(-rb.velocity.normalized * frictionSettings.speedCapFrictionPower);
        }
    }

    private void FullStop()
    {
        rb.velocity = Vector2.zero;
    }

    private void ApplyDynamicFriction()
    {
        Vector2 friction = -rb.velocity * frictionSettings.dynamicFriction;
        rb.AddForce(friction);
    }

    private void ApplyBrake()
    {
        Vector2 brakeForce = -rb.velocity.normalized * frictionSettings.brakeForce;
        if (input.x != 0)
            brakeForce.x = 0;
        if (input.y != 0)
            brakeForce.y = 0;
        rb.AddForce(brakeForce);
    }

}
