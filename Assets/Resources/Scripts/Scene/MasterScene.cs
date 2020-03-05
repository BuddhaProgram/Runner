using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************
 * MasterScene.cs
 * Type: Game Controller (Inherit from BaseGameController)
 * Usage: MasterScene controller
 ********************/
public class MasterScene : BaseGameController {
    [SerializeField]
    private string s_players = "";
    [SerializeField]
    private string s_scores = "";
    [SerializeField]
    private int i_maxScores = 0;
    // Changes the scene on Level Button interactivity
    public override void LoadNextScene() { }
    // Awake
    public override void Awake() {
        // Get all managers to initialize their values
        ButtonManager.Instance.ManagerAwake();
        PlayerManager.Instance.ManagerAwake();
        NMManager.Instance.ManagerAwake();
        base.Awake();
    }
    // Start
    public override void Start() {
        // Starts loading scene
        StartCoroutine(LevelManager.Instance.LoadAsynchronously("Main Menu"));
        // Init Score Manager
        ScoreManager.Instance.Init(s_players, s_scores, i_maxScores);
        // Change scene state
        m_Scene_State = Scene_State.Load;
    }
    // Update
    public override void Update() {
        switch (m_Scene_State) {
            case Scene_State.Load: {
                    if (LevelManager.Instance.OperationReady) {
                        LevelManager.Instance.ActivateOperation();
                        m_Scene_State = Scene_State.End;
                    }
                } break;

            default: break;

        }
    }
    // OnDestroy
    public override void OnDestroy() {

    }
}
