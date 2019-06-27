using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager
{
    private Dictionary<string, GameObject> popups = new Dictionary<string,GameObject>();

    public GameObject AddPopup(string name, Transform parent)
    {
        GameObject result = null;

        if (!string.IsNullOrEmpty(name))
        {
            var resource = Resources.Load("Prefabs/Popups/" + name);
            if (resource != null)
            {
                result = (GameObject) Object.Instantiate(resource, parent);
                popups.Add(name, result);
            }
            else
            {
                throw new NullReferenceException("UIManager: " + name + " is not exists in resources");
            }
        }
        else
        {
            throw new NullReferenceException("UIManager: resource name is empty");
        }

        return result;
    }

    public void RemovePopup([NotNull] string name)
    {
        if (popups.ContainsKey(name))
        {
            if (popups.ContainsValue(popups[name]))
            {
                Object.Destroy(popups[name]);
            }
            else
            {
                throw new Exception("UIManager: " + name + " not found");
            }
        }
        else
        {
            throw new Exception("UIManager: " + name + " doesn't exists");
        }
    }
}