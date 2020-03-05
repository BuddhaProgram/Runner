using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Fader : BaseAnim {
    #region Variables
    [SerializeField]
    private Fade_Type m_Start_Fade = Fade_Type.None;
    [SerializeField]
    private Fade_Type m_End_Fade = Fade_Type.None;
    #endregion
    #region Functions
    // Setting up to Manager in awake
    public override void Awake() {
        // Set up variables
        m_Animation_State = Animation_State.Standby;
        // Sets up fade speed
        f_animSpeed = 1 / f_originAnimTime;
    }
    // Init
    public override void Init() {

    }

    // Run Update
    public override void RunUpdate() {
        // Runs update based on animation state
        switch (m_Animation_State) {
            case Animation_State.Running_Start: {
                    switch (m_Start_Fade) {
                        case Fade_Type.Gradual_Fade_In: {
                                m_thisCanvasGroup.alpha += f_animSpeed * SystemControls.Instance.UTime * Time.deltaTime;
                                if (m_thisCanvasGroup.alpha >= SystemControls.Instance.One) {
                                    m_thisCanvasGroup.alpha = SystemControls.Instance.One;
                                    m_Animation_State = Animation_State.End;
                                }
                            }
                            break;
                        case Fade_Type.Instant_Fade_In: {
                                m_thisCanvasGroup.alpha = SystemControls.Instance.One;
                                m_Animation_State = Animation_State.End;
                            }
                            break;
                        case Fade_Type.Gradual_Fade_Out: {
                                m_thisCanvasGroup.alpha -= f_animSpeed * SystemControls.Instance.UTime * Time.deltaTime;
                                if (m_thisCanvasGroup.alpha <= SystemControls.Instance.Zero) {
                                    m_thisCanvasGroup.alpha = SystemControls.Instance.Zero;
                                    m_Animation_State = Animation_State.End;
                                }
                            }
                            break;
                        case Fade_Type.Instant_Fade_Out:
                            {
                                m_thisCanvasGroup.alpha = SystemControls.Instance.Zero;
                                m_Animation_State = Animation_State.End;
                            }
                            break;
                    }
                    break;
                }

            case Animation_State.Running_End: {
                    switch (m_End_Fade) {
                        case Fade_Type.Gradual_Fade_In: {
                                m_thisCanvasGroup.alpha += f_animSpeed * SystemControls.Instance.UTime * Time.deltaTime;
                                if (m_thisCanvasGroup.alpha >= SystemControls.Instance.One) {
                                    m_thisCanvasGroup.alpha = SystemControls.Instance.One;
                                    m_Animation_State = Animation_State.End;
                                }
                            }
                            break;
                        case Fade_Type.Instant_Fade_In: {
                                m_thisCanvasGroup.alpha = SystemControls.Instance.One;
                                m_Animation_State = Animation_State.End;
                            }
                            break;
                        case Fade_Type.Gradual_Fade_Out: {
                                m_thisCanvasGroup.alpha -= f_animSpeed * SystemControls.Instance.UTime * Time.deltaTime;
                                if (m_thisCanvasGroup.alpha <= SystemControls.Instance.Zero) {
                                    m_thisCanvasGroup.alpha = SystemControls.Instance.Zero;
                                    m_Animation_State = Animation_State.End;
                                }
                            }
                            break;
                        case Fade_Type.Instant_Fade_Out: {
                                m_thisCanvasGroup.alpha = SystemControls.Instance.Zero;
                                m_Animation_State = Animation_State.End;
                            }
                            break;
                    }
                    break;
                }

            case Animation_State.End: {
                    if (b_runOnce)
                        m_Animation_State = Animation_State.Disabled;
                } break;
        }
    }
    // Destroys on end Application
    public override void DestroyOnEnd() {
        m_thisCanvasGroup = null;
    }
    // Function to trigger start animation
    public override void TriggerStartimation() {
        // Checks if animation is on standby
        if (m_Animation_State == Animation_State.Standby) {
            // Trigger animation to run
            m_Animation_State = Animation_State.Running_Start;
        }
    }
    // Function to trigger end animation
    public override void TriggerEndAnimation() {
        // Checks if animation is on standby
        if (m_Animation_State == Animation_State.Standby) {
            // Trigger animation to run
            m_Animation_State = Animation_State.Running_End;
        }
    }
    // Function used to disable all animation
    public override void DisableAnimation() {
        m_Animation_State = Animation_State.Disabled;
    }
    // Function used for resetting animation
    public override void ResetAnimation() {
        if (m_Animation_State == Animation_State.End)
            m_Animation_State = Animation_State.Standby;
    }
    #endregion
}
