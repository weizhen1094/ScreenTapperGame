using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static GameObject GetParentObj(this GameObject go)
    {
        return go.transform.parent.gameObject;
    }

    public static List<GameObject> GetChildren(this GameObject go)
    {
        List<GameObject> result = new List<GameObject>();
        foreach(Transform t in go.GetComponentInChildren<Transform>())
        {
            result.Add(t.gameObject);
        }
        return result;
    }
}
