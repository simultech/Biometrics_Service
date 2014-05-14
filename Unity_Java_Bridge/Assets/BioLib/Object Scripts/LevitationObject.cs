using UnityEngine;
using System.Collections;

public class LevitationObject : MonoBehaviour {
	
	public GameObject biometricController;
	private Vector3 levitationForce;
	private BiometricInputClient input;
	private Rigidbody rigid;
	private float maximumLevitationPower;
	public float meditationMultiplier;
	private float initialHeight;
	private Vector3 lastForce;
	
	public float maximumLevitationHeight;
	public int requiredMeditation = 50;

	// Use this for initialization
	void Start () {
		meditationMultiplier = 4;
		maximumLevitationPower = (Mathf.Abs(Physics.gravity.y))*100/requiredMeditation;
		maximumLevitationHeight = 15;
		input = biometricController.GetComponent("BiometricInputClient") as BiometricInputClient;
		rigid = this.GetComponent("Rigidbody") as Rigidbody;
		initialHeight = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if((this.transform.position.y < initialHeight + maximumLevitationHeight) && (input.meditation*maximumLevitationPower/100 >= Mathf.Abs(Physics.gravity.y))) {
			//Goes upwards
			levitationForce = new Vector3(0,(input.meditation*maximumLevitationPower)/100*meditationMultiplier,0);
			rigid.velocity = Vector3.zero;
			rigid.useGravity = true;
		} else if((input.meditation*maximumLevitationPower/100 < Mathf.Abs(Physics.gravity.y))) {
			//Goes downwards
			//levitationForce = new Vector3(0,(input.meditation*maximumLevitationPower)/100,0)+Physics.gravity;
			levitationForce = Vector3.zero;
			if(rigid.velocity.y > 0) {
				Vector3 tmpVelocity = rigid.velocity;
				tmpVelocity.y = 0;
				rigid.velocity = tmpVelocity;
			} else {
				Vector3 tmpVelocity = rigid.velocity;
				tmpVelocity.y = rigid.velocity.y * (requiredMeditation-(input.meditation-20))/requiredMeditation;
				rigid.velocity = tmpVelocity;
			}
			rigid.useGravity = true;
		} else {
			if((input.meditation*maximumLevitationPower)/100 > Mathf.Abs(Physics.gravity.y)) {
				levitationForce = Vector3.zero;
				rigid.velocity = new Vector3(0,-0.5f,0);
				rigid.useGravity = false;
			}
		}
		rigid.AddForce(levitationForce);
	}
}
