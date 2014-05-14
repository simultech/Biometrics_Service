using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;
using System.Xml.Linq;

public class Controller_Wild_Divine : Controller_Biometric {

	//Public attributes
	public float sr;
	public float hr;
	public float sf;
	public float hf;
	public float hb;
	public float br;
	public int tu;

	//Socket is connected and ready
	internal Boolean socketReady = false;
	internal Boolean beenRead = false;

	//Sockets variables
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;

	//Host Variables
	String Host = "localhost";
	Int32 Port = 8888;
	
	// Use this for initialization
	void Start () {
		this.controllerName = "Wild Divine";
		this.isConnected = false;
		Debug.Log ("===Starting biometric connection===");
		setupSocket();
	}
	
	// Update is called once per frame
	void Update () {
		this.readSocket();
	}

	public void setupSocket() {
		try {
			Debug.Log ("Creating socket");
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			theStream.ReadTimeout = 30;
			socketReady = true;
			//Invoke("getstatus", 1);
			Invoke("startBiometrics", 2);
			this.isConnected = true;
		} catch (Exception e) {
			Debug.Log("Socket error: " + e);
		}
	}

	public void getstatus() {
		this.writeSocket("<command value=\"status\"/>");
	}

	public void startBiometrics() {
		this.writeSocket ("<command value=\"start\"><variables><variable code=\"ts\" /><variable code=\"tu\" /><variable code=\"sf\" /><variable code=\"hb\" noise_threshold=\".45\" skipped_beat_rate=\".35\" min_valid=\"15\" max_valid=\"165\" peak_search_threshold_factor=\"1.15\" bpm_smooth_factor=\".8\" /><variable code=\"br\" /><variable code=\"bc\" /><variable code=\"hc\" ideal_cycle=\"10\" /></variables></command> ");
	}

	public void writeSocket(string theLine) {
		if (!socketReady)
			return;
		String foo = theLine + "\0";
		theWriter.Write(foo);
		theWriter.Flush();
	}

	public void readSocket() {
		if (socketReady) {
			if (theStream.DataAvailable) {
				String line = readNullLine();
				Debug.Log ("Found data "+line);
				XElement xmlData = XElement.Parse("<xml>"+line+"</xml>");
				foreach (XElement bioData in xmlData.Elements("m")) {   
					foreach (var attribute in bioData.Element("p").Attributes()) {
						string name = "";
						string value = ""+attribute.Value;
						switch(""+attribute.Name) {
						case "sr":
							name = "SCL Raw";
							this.sr = float.Parse(value);
							break;
						case "hr":
							name = "HRV Raw";
							this.hr = float.Parse(value);
							break;
						case "sf":
							name = "SCL Float";
							this.sf = float.Parse(value);
							break;
						case "hf":
							name = "HRV Float";
							this.hf = float.Parse(value);
							break;
						case "hb":
							name = "Heart (bpm)";
							this.hb = float.Parse(value);
							break;
						case "tu":
							name = "Time (ms)";
							this.tu = int.Parse(value);
							break;
						case "br":
							name = "Respiration";
							this.br = float.Parse(value);
							break;
						}
						if(name != "") {
							this.SetValue (name, ""+value);
						}
					}
				}
			}
		}
	}

	public String readNullLine() {
		String line = "";
		char[] c = null;
		while (theReader.Peek() >= 0) 
		{
			c = new char[1];
			theReader.ReadBlock(c, 0, c.Length);
			if(c[0] == '\0') {
				break;
			}
			line += c[0];
		}
		return line;
	}

	~Controller_Wild_Divine() {
		this.closeSocket ();
	}

	public void closeSocket() {
		if (!socketReady)
			return;
		theWriter.Close();
		theReader.Close();
		mySocket.Close();
		socketReady = false;
	}
}
