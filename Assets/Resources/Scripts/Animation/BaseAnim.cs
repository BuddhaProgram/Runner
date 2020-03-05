using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class BaseAnim : BaseNM {
    #region Variables
    // Canvas Group used for animation
    [SerializeField]
    protected CanvasGroup m_thisCanvasGroup = null;
    [SerializeField]
    protected float f_originAnimTime = 1f;
    // The animation speed
    protected float f_animSpeed = 0f;
    // Checks if this animation runs only once
    [SerializeField]
    protected bool b_runOnce = true;
    // Animation State
    protected Animation_State m_Animation_State = Animation_State.Standby;
    #endregion
    #region Functions
    // Change the anim speed
    public void ChangeAnimSpeed(float time) { f_animSpeed = 1 / time; }
    // Reset anim speed
    public void ResetAnimSpeed() { f_animSpeed = 1 / f_originAnimTime; }
    // Abstract function to trigger start animation
    public abstract void TriggerStartimation();
    // Abstract function to trigger end animation
    public abstract void TriggerEndAnimation();
    // Abstract function for disabling animation
    public abstract void DisableAnimation();
    // Abstract function for resetting animations
    public abstract void ResetAnimation();
    // Checks if animation is finish
    public virtual bool IsAnimationFinished() { return m_Animation_State == Animation_State.End; }
    // Checks if animation is disabled
    public virtual bool IsAnimationDisabled() { return m_Animation_State == Animation_State.Disabled; }
    #endregion
}
