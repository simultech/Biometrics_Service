package au.edu.uq.foo.braincastlib;

import java.io.IOException;

public class BraincastLib {
	public static void main(String[] args) {
		NeuroskySocketClient neurosky = new NeuroskySocketClient();
		try {
			neurosky.listenSocket();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}