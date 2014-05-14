using UnityEngine;
using System.Collections;
using System.Collections.Specialized;
using System;
using System.IO;
using System.Net.Sockets;
using System.Xml.Linq;

public class Controller_Biometric : MonoBehaviour {
	
	internal Boolean isConnected = false;
	internal String controllerName = "Unknown";
	internal OrderedDictionary values = new OrderedDictionary();

	public String ControllerName() {
		return this.controllerName;
	}

	public Boolean Connected() {
		return this.isConnected;
	}

	OrderedDictionary GetValues() {
		return this.values;
	}

	public void SetValue(String key,String value) {
		if (this.values.Contains (key)) {
			this.values[key] = value;
		} else {
			this.values.Add(key,value);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
