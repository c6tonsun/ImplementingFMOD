using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class VolumeSliderManager : MonoBehaviour
{
    // These should be saved when changed
    // and loaded on application start
    // TODO: add saving and loading of data
    [Range(0, 1)]       // 0 = 0%, 1 = 100%
    public float master, music, sfx;

    // Array of all sliders
    VolumeSlider[] _sliders;

    // These are used for volume control
    VCA _master, _music, _sfx;

    private void Start()
    {
        // Fills array with all VolumeSliders in scene
        _sliders = FindObjectsOfType<VolumeSlider>();

        // Making VCA references
        _master = RuntimeManager.GetVCA("vca:/Master");
        _music = RuntimeManager.GetVCA("vca:/Music");
        _sfx = RuntimeManager.GetVCA("vca:/SFX");

        SetupSliders();
    }

    // Goes though sliders and setups them correctly
    // according to what VCA they belong
    void SetupSliders()
    {
        foreach(VolumeSlider vs in _sliders)
        {
            switch (vs.vca)
            {
                case VolumeSlider.VCA.Master:
                    vs.SetupSlider(master);
                    break;
                case VolumeSlider.VCA.Music:
                    vs.SetupSlider(music);
                    break;
                case VolumeSlider.VCA.SFX:
                    vs.SetupSlider(sfx);
                    break;
                default:
                    // do nothing
                    break;
            }
        }
    }

    // Sets volume of the VCA that is set in VolumeSlider
    public void SetVolume(VolumeSlider.VCA vca, float value)
    {
        switch (vca)
        {
            case VolumeSlider.VCA.Master:
                master = value;
                _master.setVolume(master);
                break;
            case VolumeSlider.VCA.Music:
                music = value;
                _music.setVolume(music);
                break;
            case VolumeSlider.VCA.SFX:
                sfx = value;
                _sfx.setVolume(sfx);
                break;
            default:
                // do nothing
                break;
        }
    }
}
