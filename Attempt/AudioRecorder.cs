using UnityEngine;
using System.Collections.Generic;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    private AudioSource audioSource;

    public string selectedMicrophone;
    public string[] microphoneArray;

    void Start()
    {
        // Create an AudioSource component
        audioSource = gameObject.AddComponent<AudioSource>();

        // Get the initial list of available microphones
        GetMicrophoneList();
    }


    [ContextMenu("List of microphone")]
    // Get a list of available microphones
    public void GetMicrophoneList()
    {
        microphoneArray = Microphone.devices;

    }

    // Start recording audio using a specific microphone
    public void StartRecording(string microphoneDevice)
    {
        // Check if there is no recording in progress
        if (Microphone.IsRecording(microphoneDevice))
        {
            Debug.LogWarning("Recording is already in progress.");
            return;
        }

        // Start recording with a 10-second clip
        recordedClip = Microphone.Start(microphoneDevice, false, 10, 44100);

        // Update the selected microphone
        selectedMicrophone = microphoneDevice;

        Debug.Log("Recording started with microphone: " + selectedMicrophone);
    }

    // Stop recording and play the recorded audio
    public void StopRecordingAndPlay()
    {
        // Check if there is no recording in progress
        if (!Microphone.IsRecording(null))
        {
            Debug.LogWarning("No recording in progress.");
            return;
        }

        // Stop recording
        Microphone.End(null);

        Debug.Log("Recording stopped.");

        // Play the recorded audio
        audioSource.clip = recordedClip;
        audioSource.Play();
    }

    // Context menu entry to start recording
    [ContextMenu("Start Recording")]
    private void ContextStartRecording()
    {
        if (microphoneArray.Length > 0)
        {
            StartRecording(microphoneArray[0]); // Start recording using the first available microphone (change this if needed)
        }
        else
        {
            Debug.LogWarning("No available microphones to start recording.");
        }
    }

    // Context menu entry to stop recording
    [ContextMenu("Stop Recording")]
    private void ContextStopRecording()
    {
        StopRecordingAndPlay();
    }
}
