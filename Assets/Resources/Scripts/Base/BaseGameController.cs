using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameController : MonoBehaviour {
    #region Variables
    // Scene's state
    protected Scene_State m_Scene_State = Scene_State.Standby;
    // Animation to start off the scene
    [SerializeField]
    protected Fader m_startSceneAnimation = null;
    // Animation to end off the scene
    [SerializeField]
    protected Fader m_endSceneAnimation = null;
    #endregion
    #region Functions
    // Changes the scene on Level Button interactivity
    public abstract void LoadNextScene();
    // Awake
    public virtual void Awake() { m_Scene_State = Scene_State.Idle; }
    // Start is called before the first frame update
    public abstract void Start();
    // Update is called once per frame
    public abstract void Update();
    // OnDestroy
    public abstract void OnDestroy();
    #endregion
}
