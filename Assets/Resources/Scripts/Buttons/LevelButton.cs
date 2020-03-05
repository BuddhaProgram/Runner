using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : BaseButton {
    #region Variables
    // The level to change to
    [SerializeField]
    private string s_sceneName = "";
    #endregion
    #region Functions
    // OnInteract Function
    public override void OnClick() {
        // Sets the scene name
        LevelManager.Instance.SetNextSceneName(s_sceneName);
        // Request Button Manager to disable all button and go to next scene
        ButtonManager.Instance.RequestSceneChange();
    }
    // Awake
    protected override void Awake() {

    }
    // Start
    protected override void Start() {
        // Adds button to Button Manager
        ButtonManager.Instance.AddButtonToList(this);
    }
    // Update
    protected override void Update() {

    }
    // Destroy
    protected override void OnDestroy() {
        // Sets the button to null to prevent memory leak
        m_thisButton = null;
    }
    #endregion
}
