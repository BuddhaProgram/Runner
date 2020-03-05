using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**************************************************
 * Class: Base Instance
 * Type: Instance (Template)
 * Description: Use this to define Manager scripts
 **************************************************/
public class BaseInstance<T> where T : new() {
    // The instance
    private static T instance;
    // Gets the instance of the class
    public static T Instance {
        get {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }
    // Invoke this function to instantiate
    public void Instantiate() {
        if (instance != null)
            return;

        else
            instance = new T();
    }
    // Invoke this function to clear all attributes to prevent memory leak
    public virtual void Destroy() { instance = default(T); }
}
