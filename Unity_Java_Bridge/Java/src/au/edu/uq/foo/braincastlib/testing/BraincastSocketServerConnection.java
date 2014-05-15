package au.edu.uq.foo.braincastlib.testing;

import java.net.*;
import java.io.*;

import au.edu.uq.foo.braincastlib.BiometricData;

public class BraincastSocketServerConnection extends Thread {
	private Socket socket = null;
	public String data = "";

	public BraincastSocketServerConnection(Socket socket) {
		super("KKMultiServerThread");
		this.socket = socket;
	}

	public void run() {

		try {
			PrintWriter out = new PrintWriter(socket.getOutputStream(), true);

			while(socket.isConnected()) {
				out.println(BiometricData.getData());
			}

			out.close();
			socket.close();

		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}