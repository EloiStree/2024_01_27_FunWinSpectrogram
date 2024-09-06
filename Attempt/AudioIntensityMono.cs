using UnityEngine;

public class MicrophoneIntensity : MonoBehaviour
{
    public string m_microphoneName = null;
    public float sensitivity = 100f;
    public float visualIntensity;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
        {
            if (m_microphoneName == null)
            {
                m_microphoneName = Microphone.devices[0];
            }

            audioSource.clip = Microphone.Start(m_microphoneName, true, 10, AudioSettings.outputSampleRate);
            audioSource.loop = true;

            while (!(Microphone.GetPosition(m_microphoneName) > 0)) { }

            audioSource.Play();
        }
        else
        {
            Debug.LogError("No microphone found!");
        }
    }

    void Update()
    {
        float[] samples = new float[audioSource.clip.samples];
        audioSource.clip.GetData(samples, 0);

        // Calculate the current intensity
        float currentIntensity = 0f;
        foreach (float sample in samples)
        {
            currentIntensity += Mathf.Abs(sample);
        }
        currentIntensity /= samples.Length;

        // Map the intensity to a visual scale
        visualIntensity = currentIntensity * sensitivity;

        // Use visualIntensity to control visual elements in your scene
        // For example, you can change the color or scale of an object based on intensity
        m_asString=("Microphone Intensity: " + visualIntensity);
    }
    public string m_asString;
}
