package au.edu.uq.foo.braincastlib.testing;
import java.net.*;
import java.io.*;

import au.edu.uq.foo.braincastlib.NeuroskySocketClient;

public class BraincastServer {

	public BraincastServer() throws IOException {
		ServerSocket serverSocket = null;
        boolean listening = true;

        try {
            serverSocket = new ServerSocket(4444);
        } catch (IOException e) {
            System.err.println("Could not listen on port: 4444.");
            System.exit(-1);
        }
        System.out.println("Listening");
        
        NeuroskySocketClient neurosky = new NeuroskySocketClient();
        Thread nskyThread = new Thread(neurosky);
        //nskyThread.start();
        
        while (listening)
        	new BraincastSocketServerConnection(serverSocket.accept()).start();
        serverSocket.close();
	}
	
	public static void main(String[] args) {
		try {
			BraincastServer bc = new BraincastServer();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
}
