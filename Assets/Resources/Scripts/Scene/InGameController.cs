using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameController : BaseGameController
{
    #region Variables
    // The start of the scene of Game Object's interaction
    [SerializeField]
    private GameObject m_start_of_GameObject = null;
    // Adding of tiles
    [SerializeField]
    private TilemapAdder m_tileMapAdder = null;
    // Player's controller
    [SerializeField]
    private PlayerController m_playerController = null;
    // Pause Menu
    [SerializeField]
    private GameObject m_pauseGameObject = null;
    // Text for player's name
    [SerializeField]
    private TextMeshProUGUI m_playerText = null;
    // Text for score
    [SerializeField]
    private TextMeshProUGUI m_playerScore = null;
    [SerializeField]
    private TextMeshProUGUI[] m_LeaderboardNames = null;
    [SerializeField]
    private TextMeshProUGUI[] m_LeaderboardScores = null;
    // Player's score
    private float f_score = 0f;
    // Game Over
    [SerializeField]
    private GameObject m_gameOverObject = null;
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
        NMManager.Instance.AddToList(m_tileMapAdder, false);
        // Runs Awake of all non-monobehaviour class
        NMManager.Instance.Awake();
        // Calls base function
        base.Awake();
    }
    // Start is called before the first frame update
    public override void Start() {
        // Init all NMs in the current scene
        NMManager.Instance.Init();
        // Sets player's name
        m_playerText.text = "Name: " + PlayerManager.Instance.s_playerName;
    }

    // Update is called once per frame
    public override void Update() {
        //// Runs NMManager Update as long as the scene is not disabled
        if (m_Scene_State != Scene_State.Disabled)
        NMManager.Instance.RunUpdate();
        // Update based on state of scene
        switch (m_Scene_State) {
            case Scene_State.Idle: {
                    // Starts the animation (Fade Out on CanvasGroup due to alpha)
                    m_startSceneAnimation.TriggerStartimation();
                    // Change to standby scene state
                    m_Scene_State = Scene_State.Standby;
                }
                break;
            case Scene_State.Standby: {
                    // Checks if animation is done
                    if (m_startSceneAnimation.IsAnimationFinished()) {
                        // Sets the Game object to set active
                        m_start_of_GameObject.SetActive(true);
                        // Change the scene state to ready
                        m_Scene_State = Scene_State.Ready;
                    }
                }
                break;

            case Scene_State.Ready: {
                    // Checks if player is on standby
                    if (m_playerController.IsPlayerOnStandby()) {
                        // Starts the game if ready
                        if (Input.GetButtonUp("Jump")) {
                            // Start Running
                            m_playerController.StartRunning();
                            // Set Instruction Game Object to false
                            m_start_of_GameObject.SetActive(false);
                        }
                    }

                    else if (m_playerController.HasPlayerLost()) {
                        // Set game over to true
                        if (!m_gameOverObject.activeSelf) {
                            // Save score
                            ScoreManager.Instance.InsertHighScore(PlayerManager.Instance.s_playerName, Mathf.FloorToInt(f_score));
                            // Display score
                            ScoreManager.Instance.DisplayLeaderboard(m_LeaderboardNames, m_LeaderboardScores);
                            m_gameOverObject.SetActive(true);
                        }
                        if (Input.GetButtonUp("Jump")) {
                            // Save score data
                            ScoreManager.Instance.Save();
                            // Changes scene to fade out (Fade in on CanvasGroup due to alpha)
                            m_endSceneAnimation.TriggerEndAnimation();
                            // Swtich off gameover canvas
                            m_gameOverObject.SetActive(false);
                            // Start loading scene
                            StartCoroutine(LevelManager.Instance.LoadAsynchronously("Main Menu"));
                            // Change scene state
                            m_Scene_State = Scene_State.Load;
                        }
                    }
                    else {
                        // Counts the score
                        f_score += m_playerController.PlayerSpeed() * Time.deltaTime;
                        // Print score
                        m_playerScore.text = Mathf.FloorToInt(f_score).ToString();
                        // Pause the game
                        if (Input.GetKeyUp(KeyCode.P)) {
                            // Freeze player controller
                            m_playerController.PauseRunning();
                            // Pause Menu
                            m_pauseGameObject.SetActive(true);
                            // Change to paused state
                            m_Scene_State = Scene_State.Paused;
                        }
                    }
                }
                break;

            case Scene_State.Paused: {
                    if (Input.GetKeyUp(KeyCode.P)) {
                        m_playerController.ResumeRunning();
                        // Pause Menu
                        m_pauseGameObject.SetActive(false);
                        // Sets to ready
                        m_Scene_State = Scene_State.Ready;
                    }
                } break;

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
