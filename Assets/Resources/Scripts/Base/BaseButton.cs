using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/********************
 * BaseButton.cs
 * Type: Base Abstract
 * Usage: Every button's script
 ********************/
public abstract class BaseButton : MonoBehaviour {
    #region Variables
    // The button script from Game Object
    [SerializeField]
    protected Button m_thisButton = null;
    #endregion
    #region Functions
    // OnInteract Function
    public abstract void OnClick();
    // Disables button interactable
    public virtual void DisableInteractivity() { m_thisButton.interactable = false; }
    protected abstract void Awake();
    protected abstract void Start();
    protected abstract void Update();
    protected abstract void OnDestroy();
    #endregion
}
