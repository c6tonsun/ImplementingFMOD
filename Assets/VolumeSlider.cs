using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    VolumeSliderManager _manager;

    public enum VCA
    {
        Master,
        Music,
        SFX
    }
    public VCA vca;

    // Called from UI slider setup in editor
    // Tells manager to set volume of specified VCA
    public void SetVolume()
    {
        _manager.SetVolume(vca, GetComponent<Slider>().value);
    }

    // Sets UI slider to correct value
    public void SetupSlider(float value)
    {
        GetComponent<Slider>().value = value;
    }
}
