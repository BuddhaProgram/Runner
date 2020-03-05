using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************
 * ButtonManager.cs
 * Type: Instance (Template from Base Instance)
 * Usage: Management of all buttons in the scene
 ********************/
public class ButtonManager : BaseInstance<ButtonManager> {
    #region Variables
    // List of Buttons
    private List<BaseButton> List_Buttons = new List<BaseButton>();
    // The current scene
    private BaseGameController m_currentScene = null;
    #endregion
    #region Functions
    // Add button into the list
    public void AddButtonToList(BaseButton button) { List_Buttons.Add(button); }
    // Clears Button List
    public void ClearButtonList() { List_Buttons.Clear(); }
    // Attach current scene for event
    public void AssignScene(BaseGameController controller) { m_currentScene = controller; }
    // Clears current scene
    public void ClearAssignedScene() { m_currentScene = null; }
    // On click in any of the buttons disables all buttons
    public void RequestSceneChange() {
        // Disables interactables through a loop
        foreach (BaseButton button in List_Buttons) {
            button.DisableInteractivity();
        }
        // Changes the scene
        m_currentScene.LoadNextScene();
    }

    // Setting up in Awake
    public void ManagerAwake() {
        // Initialize the list of buttons
        List_Buttons = new List<BaseButton>();
    }
    #endregion
}
