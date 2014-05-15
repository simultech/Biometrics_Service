package au.edu.uq.foo.braincastlib;

public class BiometricData {
	
	public static String deviceType = "Local";
	
	public static int attention = 0;
	public static int meditation = 0;
	public static int poorSignal = 50;
	
	// Brain wave signals
	public static int eegPowerLength = 0;
	public static float delta = 0.0f;
	public static float theta = 0.0f;
	public static float alpha1 = 0.0f;
	public static float alpha2 = 0.0f;
	public static float beta1 = 0.0f;
	public static float beta2 = 0.0f;
	public static float gamma1 = 0.0f;
	public static float gamma2 = 0.0f;
	
	public static String getData(String format) {
		
		if (format == "json") {
			return "{" + 
					"'deviceType':" + deviceType + "," +
					"'poorSignal':" + poorSignal + "," +
					"'attention':" + attention + "," +
					"'meditation':" + meditation + "," +
					"'eegPowerLength':" + eegPowerLength + "," +
					"'delta':" + delta + "," +
					"'theta':" + theta + "," +
					"'alpha1':" + alpha1 + "," +
					"'alpha2':" + alpha2 + "," +
					"'beta1':" + beta1 + "," +
					"'beta2':" + beta2 + "," +
					"'gamma1':" + gamma1 + "," +
					"'gamma2':" + gamma2 + 
					"}";
		}
		return "";
	}
	
	public static String getData() {
		return getData("json");
	}
}