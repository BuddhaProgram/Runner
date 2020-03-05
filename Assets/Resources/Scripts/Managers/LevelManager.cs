using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/********************
 * LevelManager.cs
 * Type: Instance (Template from Base Instance)
 * Usage: Management of scene change
 ********************/
public class LevelManager : BaseInstance<LevelManager> {
    // The scene name to change
    private string s_sceneName;
    // Sets the scene name
    public void SetNextSceneName(string name) { s_sceneName = name; }
    // Async Operation
    private AsyncOperation m_asyncOps = null;
    // Checks if Async Operation progress is done
    public bool OperationReady { get; private set; } = false;
    // Activates the operation
    public void ActivateOperation() {
        // Set scene activation
        m_asyncOps.allowSceneActivation = true;
        // Set the async operation to null
        m_asyncOps = null;
        // Set the scene name to empty
        s_sceneName = "";
    }
    // Invoke this function to get the name of the current active scene
    public string GetActiveSceneName { get { return SceneManager.GetActiveScene().name; } }
    // Load scene asynchronously
    public IEnumerator LoadAsynchronously(string sceneName) {
        // Load scene
        m_asyncOps = SceneManager.LoadSceneAsync(sceneName);
        // Set loading to false
        m_asyncOps.allowSceneActivation = false;
        // Goes through a loop to check if progress is ready
        while (!OperationReady) {
            if (m_asyncOps.progress >= 0.9f) {
                OperationReady = true;
                yield return null;
            }
        }
    }
    public IEnumerator LoadAsynchronously() {
        // Load scene
        m_asyncOps = SceneManager.LoadSceneAsync(s_sceneName);
        // Set loading to false
        m_asyncOps.allowSceneActivation = false;
        // Goes through a loop to check if progress is ready
        while (!OperationReady) {
            if (m_asyncOps.progress >= 0.9f) {
                OperationReady = true;
                yield return null;
            }
        }
    }
}
