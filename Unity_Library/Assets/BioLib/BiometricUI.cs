using UnityEngine;
using System.Collections;
using System;

public class BiometricUI : MonoBehaviour {

	//public Controller_Wild_Divine inputClient;
	public Controller_Biometric inputClient;
	internal int startY;
	internal int windowWith = 140;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		Rect windowRect = new Rect (10, 10, 200, 320);
		windowRect = GUI.Window (0, windowRect, WindowFunction, "Biometric Data");
	}

	void WindowFunction(int windowID) {
		startY = 25;
		if(windowID == 0) {
			//Foreach
			AddRow("Device: "+inputClient.ControllerName(),true);
			String connectedStatus = "Disconnected";
			if(inputClient.Connected()) {
				connectedStatus = "Connected";
			}
			AddRow("Status: "+connectedStatus);
			//Debug.Log (inputClient.values.Keys.Count);
			foreach(DictionaryEntry entry in inputClient.values) {
				AddRow(entry.Key+": "+entry.Value);
			}



			//GUI.Label (new Rect (25, 25, windowWith, 30), "Status: "+inputClient.Connected());
			/*GUI.Label (new Rect (25, 45, windowWith, 30), "Device: "+inputClient.deviceType);
			int signalquality = (200-inputClient.poorSignal)/2;
			GUI.Label (new Rect (25,65, windowWith, 30), "Signal Quality: "+signalquality+"%");
			int midset = 95;
			GUI.Label (new Rect (25,midset+0, windowWith, 30), "Meditation: "+inputClient.meditation);
			GUI.Label (new Rect (25,midset+20, windowWith, 30), "Attention: "+inputClient.attention);
			int bottomset = 140;
			GUI.Label (new Rect (25,bottomset, windowWith, 30), "Delta: "+inputClient.delta);
			GUI.Label (new Rect (25,bottomset+20, windowWith, 30), "Theta: "+inputClient.theta);
			GUI.Label (new Rect (25,bottomset+40, windowWith, 30), "Alpha 1: "+inputClient.alpha1);
			GUI.Label (new Rect (25,bottomset+60, windowWith, 30), "Alpha 2: "+inputClient.alpha2);
			GUI.Label (new Rect (25,bottomset+80, windowWith, 30), "Beta 1: "+inputClient.beta1);
			GUI.Label (new Rect (25,bottomset+100, windowWith, 30), "Beta 2: "+inputClient.beta2);
			GUI.Label (new Rect (25,bottomset+120, windowWith, 30), "Gamma 1: "+inputClient.gamma1);
			GUI.Label (new Rect (25,bottomset+140, windowWith, 30), "Gamma 2: "+inputClient.gamma2);*/
		}
	}

	void AddRow(String rowData,Boolean isTitle = false) {
		int theX = 25;
		if (isTitle) {
			theX = 10;
		}
		GUI.Label (new Rect (theX, startY, windowWith, 30),rowData);
		startY += 20;
	}
}