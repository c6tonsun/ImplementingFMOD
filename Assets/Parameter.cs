using UnityEngine;
using UnityEditor;

// Basicly a data bank filled with parameter data
public class Parameter : MonoBehaviour
{
    // Name of parameter in FMOD
    public string parameterName;

    // Same list as in FMOD
    // but in code we use enum structure
    public enum ParameterType
    {
        Continuous,
        Discrete,
        Labeled
    }

    // This shows the type list in editor
    public ParameterType parameterType;

    // Continuous variables
    public float min, max, value;

    // Discrete variables
    public int Min, Max, Value;

    // Labeled variables
    // String array so you can add values in editor
    public string[] labels;
    // ID or order on the array
    // Starting from zero [0, 1, 2, 3, ... N]
    public int labelID;

    // Next stuff is advanced so ...
    // You can stop reading this code now :)

    public float GetParameterValue()
    {
        switch (parameterType)
        {
            case ParameterType.Continuous:
                return value;
            case ParameterType.Discrete:
                return Value;
            default:
                return labelID;
        }
    }

    public void SetParameter(float parameterValue)
    {
        switch (parameterType)
        {
            case ParameterType.Continuous:
                value = Mathf.Clamp(parameterValue, min, max);
                break;
            case ParameterType.Discrete:
                Value = Mathf.Clamp(Mathf.RoundToInt(parameterValue),Min, Max);
                break;
            default:
                labelID = Mathf.Clamp(Mathf.RoundToInt(parameterValue), 0, labels.Length -1);
                break;
        }
    }
}

// Basicly makes custom editer for Parameter components
[CustomEditor(typeof(Parameter))]
public class ParameterEditor : Editor
{
    override public void OnInspectorGUI()
    {
        Parameter parameter = target as Parameter;

        parameter.parameterName = EditorGUILayout.TextField("Parameter Name", parameter.parameterName);
        parameter.parameterType = (Parameter.ParameterType) EditorGUILayout.EnumPopup("Parameter Type", parameter.parameterType);
        
        switch (parameter.parameterType)
        {
            case Parameter.ParameterType.Continuous:
                parameter.min = EditorGUILayout.FloatField("Min", parameter.min);
                parameter.max = EditorGUILayout.FloatField("Max", parameter.max);
                parameter.value = EditorGUILayout.Slider("Value:", parameter.value, parameter.min, parameter.max);
                break;
            case Parameter.ParameterType.Discrete:
                parameter.Min = EditorGUILayout.IntField("Min", parameter.Min);
                parameter.Max = EditorGUILayout.IntField("Max", parameter.Max);
                parameter.Value = EditorGUILayout.IntSlider("Value:", parameter.Value, parameter.Min, parameter.Max);
                break;
            case Parameter.ParameterType.Labeled:
                SerializedObject so = new SerializedObject(parameter);
                EditorGUILayout.PropertyField(so.FindProperty("labels"), true);
                so.ApplyModifiedProperties();
                break;

            default:
                break;
        }
    }
}
