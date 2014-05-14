using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class BiometricInputClient : MonoBehaviour {
    internal Boolean socketReady = false;

    TcpClient mySocket;
    NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    String Host = "localhost";
    Int32 Port = 4444;
    
    // Connection information
    public String deviceType = "";
    public int connected = 0;
    public int poorSignal = 0;
	
	// Aggregate signals
	public int attention = 0;
	public int meditation = 0;
	
	// Brain wave signals
	public int eegPowerLength = 0;
	public float delta = 0.0f;
	public float theta = 0.0f;
	public float alpha1 = 0.0f;
	public float alpha2 = 0.0f;
	public float beta1 = 0.0f;
	public float beta2 = 0.0f;
	public float gamma1 = 0.0f;
	public float gamma2 = 0.0f;
	
    void Start () {
		setupSocket();
    }
	
    void Update () {
		this.readSocket();
    }
	
// **********************************************
    public void setupSocket() {
        try {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e) {
            Debug.Log("Socket error: " + e);
        }
    }
    public void writeSocket(string theLine) {
        if (!socketReady)
            return;
        String foo = theLine + "\r\n";
        theWriter.Write(foo);
        theWriter.Flush();
    }
	
    public void readSocket() {
        if (socketReady) {
	        if (theStream.DataAvailable) {
	        	if(connected == 0) {
	        		connected = 1;
	        	}
	            String jsonLine = theReader.ReadLine();
				jsonLine = jsonLine.Replace(" ", "");
				jsonLine = jsonLine.Replace("{", "");
				jsonLine = jsonLine.Replace("}", ""); 
				
				String[] components = jsonLine.Split(',');
				foreach (String component in components) {
					String[] kvp = component.Split(':');
					if (kvp[0].Equals("'deviceType'")) {
						deviceType = kvp[1];
					} else if (kvp[0].Equals("'poorSignal'")) {
						poorSignal = int.Parse(kvp[1]);
					} else if (kvp[0].Equals("'attention'")) {
						attention = int.Parse(kvp[1]);
					} else if (kvp[0].Equals("'meditation'")) {
						meditation = int.Parse(kvp[1]);
					} else if (kvp[0].Equals("'eegPowerLength'")) {
						eegPowerLength = int.Parse(kvp[1]);
					} else if (kvp[0].Equals("'delta'")) {
						delta = float.Parse(kvp[1]);
					} else if (kvp[0].Equals("'theta'")) {
						theta = float.Parse(kvp[1]);
					} else if (kvp[0].Equals("'alpha1'")) {
						alpha1 = float.Parse(kvp[1]);
					} else if (kvp[0].Equals("'alpha2'")) {
						alpha2 = float.Parse(kvp[1]);
					} else if (kvp[0].Equals("'beta1'")) {
						beta1 = float.Parse(kvp[1]);
					} else if (kvp[0].Equals("'beta2'")) {
						beta2 = float.Parse(kvp[1]);
					} else if (kvp[0].Equals("'gamma1'")) {
						gamma1 = float.Parse(kvp[1]);
					} else if (kvp[0].Equals("'gamma2'")) {
						gamma2 = float.Parse(kvp[1]);
					}
					
				}
	        }
        }
    }
	
    public void closeSocket() {
        if (!socketReady)
            return;
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }
	
} // end class s_TCP