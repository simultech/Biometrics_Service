using UnityEngine;
using System.Collections;

public class BurnFireHellDemonStuff : MonoBehaviour {

	public GameObject biometricController;
	private BiometricInputClient input;
	
	private ParticleEmitter smoke;
	private ParticleEmitter inner;
	private ParticleEmitter outer;
	private Light flameLight;
	
	private int firstLevel = 30;
	private int secondLevel = 50;
	private int thirdLevel = 80;

	// Use this for initialization
	void Start () {
		input = biometricController.GetComponent("BiometricInputClient") as BiometricInputClient;
		smoke = GameObject.Find("Smoke").GetComponent("ParticleEmitter") as ParticleEmitter;
		inner = GameObject.Find("InnerCore").GetComponent("ParticleEmitter") as ParticleEmitter;
		outer = GameObject.Find("OuterCore").GetComponent("ParticleEmitter") as ParticleEmitter;
		flameLight = GameObject.Find("Lightsource").GetComponent("Light") as Light;
	}
	
	// Update is called once per frame
	void Update () {
		int attentionLevel = input.attention;
		if(attentionLevel < firstLevel) {
			//nothing
			inner.minEmission = 0*attentionLevel;
			inner.maxEmission = 0*attentionLevel;
			outer.minEmission = 0*attentionLevel;
			outer.maxEmission = 0*attentionLevel;
			smoke.minEmission = 0*attentionLevel;
			smoke.maxEmission = 0*attentionLevel;
		} else if(attentionLevel < secondLevel) {
			//only smoke
			inner.minEmission = 0*(attentionLevel-firstLevel)/(secondLevel-firstLevel);
			inner.maxEmission = 0*(attentionLevel-firstLevel)/(secondLevel-firstLevel);
			outer.minEmission = 0*(attentionLevel-firstLevel)/(secondLevel-firstLevel);
			outer.maxEmission = 0*(attentionLevel-firstLevel)/(secondLevel-firstLevel);
			smoke.minEmission = 40*(attentionLevel-firstLevel)/(secondLevel-firstLevel);
			smoke.maxEmission = 40*(attentionLevel-firstLevel)/(secondLevel-firstLevel);
		} else if(attentionLevel < thirdLevel) {
			//only innercore			
			inner.minEmission = 20*(attentionLevel-secondLevel)/(thirdLevel-secondLevel);
			inner.maxEmission = 20*(attentionLevel-secondLevel)/(thirdLevel-secondLevel);
			outer.minEmission = 10*(attentionLevel-secondLevel)/(thirdLevel-secondLevel);
			outer.maxEmission = 10*(attentionLevel-secondLevel)/(thirdLevel-secondLevel);
			smoke.minEmission = 30*(attentionLevel-secondLevel)/(thirdLevel-secondLevel);
			smoke.maxEmission = 30*(attentionLevel-secondLevel)/(thirdLevel-secondLevel);
		} else {
			//everything
			inner.minEmission = 40*attentionLevel/100;
			inner.maxEmission = 40*attentionLevel/100;
			outer.minEmission = 50*attentionLevel/100;
			outer.maxEmission = 50*attentionLevel/100;
			smoke.minEmission = 10*attentionLevel/100;
			smoke.maxEmission = 10*attentionLevel/100;
		}
	}
}
