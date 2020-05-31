using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EventEmitter : MonoBehaviour
{
    // Reference to event emitter with header :)
    [Header("Drag and drop event below")]
    public StudioEventEmitter eventEmitter;

    // Actual instance that emits sound
    EventInstance _instance;

    // Array of parameters
    [Header("Add used parameters below")]
    [SerializeField] Parameter[] _parameters;

    // Start playing event
    public void Play()
    {
        if (_instance.isValid() == false)
            _instance = RuntimeManager.CreateInstance(eventEmitter.Event);

        _instance.start();
    }

    // Stop playing event
    public void Stop()
    {
        _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Unity calls this when scene is changed or application ended
    // Releasing EventInstances is very important as you might leave dead things in memory
    private void OnDestroy()
    {
        _instance.release();
    }

    // Updates all parameter values of this event for FMOD
    public void UpdateParameters()
    {
        foreach (Parameter p in _parameters)
            _instance.setParameterByName(p.parameterName, p.GetParameterValue());
    }
}
