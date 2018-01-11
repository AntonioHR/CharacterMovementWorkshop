using AntonioHR.MovementExamples;
using UnityEngine;

public class DumbestMovement : MonoBehaviour {

	public float speed = 5.0f;
	public bool useRawInput = false;
	

    //This is the 'dumbest' movement script for top-down movement, it uses the joystick value to move the transform manually
    //The biggest problems with this are:
    //          *No Collision Handling
	void Start () {
		
	}
	
	void Update () {
		Vector3 input = useRawInput? InputHelper.DefaultJoystickInputRaw : InputHelper.DefaultJoystickInput;
		transform.position += input * speed * Time.deltaTime;
	}
}
