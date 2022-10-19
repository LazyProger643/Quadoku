using UnityEngine;

public static class VolumeUtils
{

	public static float ConvertLinearToDB(float linear)
	{
		return linear <= 0 ? -80.0f : 20f * Mathf.Log10(linear);
	}

	public static float ConvertDBToLinear(float dB)
	{
		return Mathf.Clamp01(Mathf.Pow(10.0f, dB / 20.0f));
	}

}