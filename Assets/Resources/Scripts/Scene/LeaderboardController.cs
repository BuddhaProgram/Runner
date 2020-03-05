using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardController : BaseGameController {
    #region Variables
    // The start of the scene of Game Object's interaction
    [SerializeField]
    private GameObject m_start_of_GameObject = null;
    [SerializeField]
    private TextMeshProUGUI[] m_playerNames = null;
    [SerializeField]
    private TextMeshProUGUI[] m_playerScores = null;
    #endregion
    #region Functions
    // Changes the scene on Level Button interactivity
    public override void LoadNextScene() {
        // Changes scene to fade out (Fade in on CanvasGroup due to alpha)
        m_endSceneAnimation.TriggerEndAnimation();
        // Changes the scene state to load
        m_Scene_State = Scene_State.Load;
        // Loads the next scene from level manager
        StartCoroutine(LevelManager.Instance.LoadAsynchronously());
    }
    // Awake
    public override void Awake() {
        // Assigns the scene to Button Manager for any scene Interactivity
        ButtonManager.Instance.AssignScene(this);
        // Adds all Non-monobehaviour classes to the manager
        NMManager.Instance.AddToList(m_startSceneAnimation, false);
        NMManager.Instance.AddToList(m_endSceneAnimation, false);
        // Runs Awake of all non-monobehaviour class
        NMManager.Instance.Awake();
        // Calls base function
        base.Awake();
    }
    // Start is called before the first frame update
    public override void Start() {
        // Init all NMs in the current scene
        NMManager.Instance.Init();
    }

    // Update is called once per frame
    public override void Update() {
        // Runs NMManager Update as long as the scene is not disabled
        if (m_Scene_State != Scene_State.Disabled)
            NMManager.Instance.RunUpdate();
        // Update based on state of scene
        switch (m_Scene_State) {
            case Scene_State.Idle: {
                    // Starts the animation (Fade Out on CanvasGroup due to alpha)
                    m_startSceneAnimation.TriggerStartimation();
                    // Display score
                    ScoreManager.Instance.DisplayLeaderboard(m_playerNames, m_playerScores);
                    // Change to standby scene state
                    m_Scene_State = Scene_State.Standby;
                }
                break;
            case Scene_State.Standby: {
                    // Checks if animation is done
                    if (m_startSceneAnimation.IsAnimationFinished()) {
                        // Starts initial Game Object Interaction through Canvas Group
                        m_start_of_GameObject.GetComponent<CanvasGroup>().interactable = true;
                        // Change the scene state to ready
                        m_Scene_State = Scene_State.Ready;
                    }
                }
                break;

            case Scene_State.Ready:
                break;

            case Scene_State.Load: {
                    // Checks if level is ready to change and if animation has ended
                    if (LevelManager.Instance.OperationReady && m_endSceneAnimation.IsAnimationFinished()) {
                        // Destroys and unload all buttons
                        ButtonManager.Instance.ClearButtonList();
                        NMManager.Instance.Destroy();
                        NMManager.Instance.ClearNMList();
                        // Changes scene state to end
                        m_Scene_State = Scene_State.End;
                    }
                }
                break;

            case Scene_State.End: {
                    // Loads scene
                    LevelManager.Instance.ActivateOperation();
                    // Change to disabled
                    m_Scene_State = Scene_State.Disabled;
                }
                break;

            case Scene_State.Disabled:
                break;
        }
    }
    // OnDestroy
    public override void OnDestroy() {

    }
    #endregion
}
