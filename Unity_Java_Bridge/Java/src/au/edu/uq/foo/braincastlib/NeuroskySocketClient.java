package au.edu.uq.foo.braincastlib;

import java.io.*;
import java.net.*;

public class NeuroskySocketClient implements Runnable {
	Socket socket = null;
	InputStream in = null;
	DataInputStream data = null;
	PrintWriter out = null;
	
	public void run() {
		// Create socket connection
		try {
			socket = new Socket("127.0.0.1", 13854); 
			in = socket.getInputStream();
			data = new DataInputStream(in);
			
		} catch (UnknownHostException e) {
			System.out.println("Unknown host: kq6py.eng");
			System.exit(1);
		} catch (IOException e) {
			System.out.println("No I/O");
			System.exit(1);
		}
		
		System.out.println("Connected to server.");
		BiometricData.deviceType = "Neurosky"; 
		
		
		
		try {
			while (true) {
				boolean found = false;
				while (!found) {
					if(data.readUnsignedByte() == 0xAA) {
						if(data.readUnsignedByte() == 0xAA) {
							found = true;
						}
					}
				}
				
				while(data.available() != 0) {
					int code = data.readUnsignedByte();
					switch (code) {
					case 0x02:
						BiometricData.poorSignal = data.readUnsignedByte();
						break;
					case 0x04:
						BiometricData.attention = data.readUnsignedByte();
						break;
					case 0x05:
						BiometricData.meditation = data.readUnsignedByte();
						break;
					case 0x81:
						BiometricData.eegPowerLength = data.readUnsignedByte();
						BiometricData.delta = data.readFloat();
						BiometricData.theta = data.readFloat();
						BiometricData.alpha1 = data.readFloat();
						BiometricData.alpha2 = data.readFloat();
						BiometricData.beta1 = data.readFloat();
						BiometricData.beta2 = data.readFloat();
						BiometricData.gamma1 = data.readFloat();
						BiometricData.gamma2 = data.readFloat();
						break;
					//0x16 (1 byte)
					default:
						System.out.println("bad bad bad");
						break;
					}
					//System.out.println("Attention: " + attention + "\n" + "Meditation: " + meditation + "\n" + "Poor signal: " + poorSignal);
				}
				
				System.out.println("Getting new data...");
			}
		} catch (EOFException eof) {
			System.out.println("Finished reading from EEG Headset.");
		} catch (IOException io) {
			System.err.println("IO Exception while attempting to read EEG: " + io);
		} finally {
			if (in != null) {
				try {
					in.close();
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			}
		}
	}
	
	public void listenSocket() throws IOException {
		
		// Create socket connection
		try {
			socket = new Socket("127.0.0.1", 13854); 
			in = socket.getInputStream();
			data = new DataInputStream(in);
			
		} catch (UnknownHostException e) {
			System.out.println("Unknown host: kq6py.eng");
			System.exit(1);
		} catch (IOException e) {
			System.out.println("No I/O");
			System.exit(1);
		}
		
		System.out.println("Connected to server.");
		
		
		
		try {
			while (true) {
				boolean found = false;
				while (!found) {
					if(data.readUnsignedByte() == 0xAA) {
						if(data.readUnsignedByte() == 0xAA) {
							found = true;
						}
					}
				}
				
				while(data.available() != 0) {
					int code = data.readUnsignedByte();
					switch (code) {
					case 0x02:
						BiometricData.poorSignal = data.readUnsignedByte();
						System.out.println("poor"+data.readUnsignedByte());
						break;
					case 0x04:
						BiometricData.attention = data.readUnsignedByte();
						System.out.println("attention"+data.readUnsignedByte());
						break;
					case 0x05:
						BiometricData.meditation = data.readUnsignedByte();
						break;
					case 0x81:
						BiometricData.eegPowerLength = data.readUnsignedByte();
						BiometricData.delta = data.readFloat();
						BiometricData.theta = data.readFloat();
						BiometricData.alpha1 = data.readFloat();
						BiometricData.alpha2 = data.readFloat();
						BiometricData.beta1 = data.readFloat();
						BiometricData.beta2 = data.readFloat();
						BiometricData.gamma1 = data.readFloat();
						BiometricData.gamma2 = data.readFloat();
						break;
					default:
						System.out.println("bad bad bad");
						break;
					}
					//System.out.println("Attention: " + attention + "\n" + "Meditation: " + meditation + "\n" + "Poor signal: " + poorSignal);
				}
				
				System.out.println("Getting new data...");
			}
		} catch (EOFException eof) {
			System.out.println("Finished reading from EEG Headset.");
		} catch (IOException io) {
			System.err.println("IO Exception while attempting to read EEG: " + io);
		} finally {
			if (in != null) {
				in.close();
			}
		}
		
	}

	
}
