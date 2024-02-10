using UnityEngine;
using System.Collections.Generic;


public abstract class AbstractStateMachineDataProvider : ScriptableObject
{
    public AbstractStateMachineDataProvider() { }

    private Dictionary<string, object> _ObjectData = null;

    public object GetObject(string key)
    {
        return GetFromDict(key, ref _ObjectData);
    }

    public object GetOrInitObject(string key)
    {
        return GetOrInitFromDict(key, ref _ObjectData);
    }

    public void SetObject(string key, object value)
    {
        SetToDict(key, value, ref _ObjectData);
    }

    private Dictionary<string, float> _FloatData = null;

    public float GetFloat(string key)
    {
        return GetFromDict(key, ref _FloatData);
    }

    public float GetOrInitFloat(string key)
    {
        return GetOrInitFromDict(key, ref _FloatData);
    }

    public void SetFloat(string key, float value)
    {
        SetToDict(key, value, ref _FloatData);
    }

    private Dictionary<string, Vector2> _Vector2Data = null;

    public Vector2 GetVector2(string key)
    {
        return GetFromDict(key, ref _Vector2Data);
    }

    public Vector2 GetOrInitVector2(string key)
    {
        return GetOrInitFromDict(key, ref _Vector2Data);
    }

    public void SetVector2(string key, Vector2 value)
    {
        SetToDict(key, value, ref _Vector2Data);
    }

    private T GetFromDict<T>(string key, ref Dictionary<string, T> dict)
    {
        if (null == dict)
            initDict<T>(ref dict);

        return dict[key];
    }

    private T GetOrInitFromDict<T>(string key, ref Dictionary<string, T> dict)
    {
        if (null == dict)
            initDict<T>(ref dict);

        if (!dict.ContainsKey(key))
            dict[key] = default(T);

        return dict[key];
    }

    private void SetToDict<T>(string key, T value, ref Dictionary<string, T> dict)
    {
        if (null == dict)
            initDict<T>(ref dict);

        dict[key] = value;
    }

    private void initDict<T>(ref Dictionary<string, T> dict)
    {
        dict = new Dictionary<string, T>();
    }
}
