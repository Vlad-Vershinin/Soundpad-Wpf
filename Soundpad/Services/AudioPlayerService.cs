using NAudio.Wave;
using Soundpad.Models;
using System.Diagnostics;

namespace Soundpad.Services;

public class AudioPlayerService
{
    private WaveOutEvent _waveOut;
    private AudioFileReader _audioFile;

    public void Play(Sound sound)
    {
        Stop();

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

    public void Stop()
    {
        _waveOut?.Stop();
        _waveOut?.Dispose();
        _audioFile?.Dispose();
    }

    public void Dispose()
    {
        Stop();
    }
}
