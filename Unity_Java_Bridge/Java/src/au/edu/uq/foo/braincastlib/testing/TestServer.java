package au.edu.uq.foo.braincastlib.testing;

import java.io.IOException;

public class TestServer {
	
	public static void main(String[] args) {
		try {
			BraincastServer testServer = new BraincastServer();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}