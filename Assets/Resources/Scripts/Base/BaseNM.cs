using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/********************
 * BaseNM.cs
 * Type: Base Abstract
 * Usage: Base abstract class for all non-monobehaviour classes
 ********************/

[Serializable]
public abstract class BaseNM {
    #region Variables
    // The Queue Priority when manager runs
    [SerializeField]
    protected RunQueue_Priority m_Queue_Priority;
    #endregion
    #region Functions
    // Gets Priority
    public RunQueue_Priority Priority() { return m_Queue_Priority; }
    // Changes Priority
    public void ChangePriority(RunQueue_Priority priority) { m_Queue_Priority = priority; }
    // Setting up in Manager on Awake
    public abstract void Awake();
    // Initialization on Start
    public abstract void Init();
    // Runs Update
    public abstract void RunUpdate();
    // Destroys on end Application
    public abstract void DestroyOnEnd();
    #endregion
}