using UnityEngine;

public class MicrophoneIntensityGauge : MonoBehaviour
{
    public float updateInterval = 0.1f; // Update interval in seconds
    public float intensity = 0.0f; // Intensity value between 0 and 100%

    private string microphoneName;
    private AudioClip microphoneClip;
    private float[] samples;

    private void Start()
    {
        // Get the default microphone and its name
        microphoneName = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

        if (microphoneName == null)
        {
            Debug.LogError("Microphone not found!");
            return;
        }

        samples = new float[256]; // Adjust the size based on your needs

        // Start recording from the microphone
        microphoneClip = Microphone.Start(microphoneName, true, 1, AudioSettings.outputSampleRate);
        while (Microphone.GetPosition(microphoneName) <= 0) { } // Wait for microphone to start

        InvokeRepeating("UpdateIntensity", 0.0f, updateInterval);
    }

    private void UpdateIntensity()
    {
        // Get audio samples from the microphone
        microphoneClip.GetData(samples, 0);

        // Calculate intensity based on the amplitude of the samples
        float sum = 0.0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        // Normalize intensity between 0 and 100%
        intensity = Mathf.Clamp01(sum / samples.Length) * 100.0f;

        // Use the intensity value as needed (e.g., update UI, control game mechanics, etc.)
        m_asString=("Intensity: " + intensity.ToString("F2") + "%");
    }
    public string m_asString;

    private void OnDestroy()
    {
        // Stop recording when the script is destroyed
        Microphone.End(microphoneName);
    }
}