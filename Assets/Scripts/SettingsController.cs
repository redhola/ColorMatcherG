using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public Toggle soundToggle;
    public Toggle vibrationToggle;

    void Start()
    {
        soundToggle.isOn = AudioListener.volume > 0;
        vibrationToggle.isOn = true; 
        soundToggle.onValueChanged.AddListener(isOn => ToggleSound(isOn));

        vibrationToggle.onValueChanged.AddListener(isOn => ToggleVibration(isOn));
    }

    public void ToggleSound(bool enabled)
    {
        AudioListener.volume = enabled ? 1.0f : 0f;
        Debug.Log("Sound is " + (enabled ? "ON" : "OFF"));
    }

    public void ToggleVibration(bool enabled)
    {
        if (enabled)
        {
            Handheld.Vibrate();
        }
        Debug.Log("Vibration is " + (enabled ? "ON" : "OFF"));
    }
}