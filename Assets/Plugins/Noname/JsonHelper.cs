using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Item;
    }
 
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Item = array;
        return JsonUtility.ToJson(wrapper);
    }
    
    public static string ToJsonString<T>(T[] array)
    {
        WrapperStr wrapper = new WrapperStr();
        List<string> items = new();
        foreach (var arrEl in array)
            items.Add(arrEl.ToString());
        wrapper.Items = items.ToArray();
        return JsonUtility.ToJson(wrapper);
    }
 
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Item = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
 
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Item;
    }
    
    [Serializable]
    private class WrapperStr
    {
        public string[] Items;
    }
}