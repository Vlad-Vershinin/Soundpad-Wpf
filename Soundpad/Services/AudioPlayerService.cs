using NAudio.Wave;
using Soundpad.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Soundpad.Services;

public class AudioPlayerService : IDisposable
{
    private WaveOutEvent _waveOut;
    private AudioFileReader _audioFile;
    private WasapiLoopbackCapture _microphoneCapture;
    private BufferedWaveProvider _outputBuffer;
    private WasapiOut _virtualMicrophoneOutput;
    private bool _isMicrophoneEnabled = true;

    public AudioPlayerService()
    {
        InitializeMicrophoneProxy();
    }

    private void InitializeMicrophoneProxy()
    {
        try
        {
            _microphoneCapture = new WasapiLoopbackCapture();
            _microphoneCapture.DataAvailable += OnMicrophoneDataAvailable;

            _outputBuffer = new BufferedWaveProvider(_microphoneCapture.WaveFormat);
            _virtualMicrophoneOutput = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 300);
            _virtualMicrophoneOutput.Init(_outputBuffer);

            _microphoneCapture.StartRecording();
            _virtualMicrophoneOutput.Play();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }

    private void OnMicrophoneDataAvailable(object sender, WaveInEventArgs e)
    {
        if (!_isMicrophoneEnabled) return;

        try
        {
            _outputBuffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }

    public void Play(Sound sound)
    {
        StopCurrentSound();

        try
        {
            _audioFile = new AudioFileReader(sound.Path);
            _waveOut = new WaveOutEvent();
            _waveOut.Init(_audioFile);
            _waveOut.Play();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }

    public void PlayInMicrophone(Sound sound)
    {
        try
        {
            using (var audioFile = new AudioFileReader(sound.Path))
            using (var waveOut = new WaveOutEvent())
            {
                waveOut.Init(audioFile);
                waveOut.Play();

                // Ждем завершения воспроизведения
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex.Message}");
        }
    }

    public void Stop()
    {
        StopCurrentSound();
    }

    public void SetMicrophoneEnabled(bool enabled)
    {
        _isMicrophoneEnabled = enabled;
    }

    private void StopCurrentSound()
    {
        _waveOut?.Stop();
        _waveOut?.Dispose();
        _waveOut = null;

        _audioFile?.Dispose();
        _audioFile = null;
    }

    public void Dispose()
    {
        Stop();
        _microphoneCapture?.StopRecording();
        _microphoneCapture?.Dispose();
        _virtualMicrophoneOutput?.Stop();
        _virtualMicrophoneOutput?.Dispose();
    }
}