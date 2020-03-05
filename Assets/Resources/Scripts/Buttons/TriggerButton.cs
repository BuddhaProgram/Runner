using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerButton : BaseButton {
    #region Variables
    // Which Game Object to trigger
    [SerializeField]
    private GameObject m_next_GameObject = null;
    // Turn off by which level by state
    [SerializeField]
    private Button_Trigger_Type m_Next_Button_Trigger_Type = Button_Trigger_Type.None;
    // Sets it to true or false
    [SerializeField]
    private bool b_buttonSetTrigger = true;
    [SerializeField]
    private GameObject m_Current_GameObject = null;
    [SerializeField]
    private Button_Trigger_Type m_curr_Button_Trigger_Type = Button_Trigger_Type.None;
    #endregion
    #region Functions
    // OnInteract Function
    public override void OnClick() {
        switch (m_Next_Button_Trigger_Type) {
            case Button_Trigger_Type.GameObject: {
                    // Set next Game Object based on button Trigger
                    m_next_GameObject.SetActive(b_buttonSetTrigger);
                    // Checks current affected button state
                    switch (m_curr_Button_Trigger_Type) {
                        case Button_Trigger_Type.GameObject: {
                                // Set own game object active
                                m_Current_GameObject.SetActive(!b_buttonSetTrigger);
                            }
                            break;

                        case Button_Trigger_Type.CanvasGroup: {
                                // Sets own Canvas group's alpha and interactivity to false
                                CanvasGroup cCG = m_Current_GameObject.GetComponent<CanvasGroup>();
                                cCG.alpha = Convert.ToInt16(!b_buttonSetTrigger);
                                cCG.interactable = !b_buttonSetTrigger;
                                cCG.blocksRaycasts = !b_buttonSetTrigger;
                            }
                            break;
                    }
                }
                break;

            case Button_Trigger_Type.CanvasGroup: {
                    // Sets next Game object's Canvas group's alpha and interactivity to false
                    CanvasGroup nCG = m_next_GameObject.GetComponent<CanvasGroup>();
                    nCG.alpha = Convert.ToInt16(b_buttonSetTrigger);
                    nCG.interactable = b_buttonSetTrigger;
                    nCG.blocksRaycasts = b_buttonSetTrigger;
                    // Checks current affected button state
                    switch (m_curr_Button_Trigger_Type) {
                        case Button_Trigger_Type.GameObject: {
                                // Set own game object active
                                m_Current_GameObject.SetActive(!b_buttonSetTrigger);
                            }
                            break;

                        case Button_Trigger_Type.CanvasGroup: {
                                // Sets own Canvas group's alpha and interactivity to false
                                CanvasGroup cCG = m_Current_GameObject.GetComponent<CanvasGroup>();
                                cCG.alpha = Convert.ToInt16(!b_buttonSetTrigger);
                                cCG.interactable = !b_buttonSetTrigger;
                                cCG.blocksRaycasts = !b_buttonSetTrigger;
                            }
                            break;
                    }
                }
                break;
        }
    }
    // Disables Button Interactable
    public override void DisableInteractivity() {
        base.DisableInteractivity();
    }
    // Awake Function
    protected override void Awake() {

    }
    // Start Function
    protected override void Start() {
        // Adds button to ButtonManager
        ButtonManager.Instance.AddButtonToList(this);
    }
    // Update Function
    protected override void Update() {

    }
    // OnDestroy Function
    protected override void OnDestroy() {

    }
    #endregion
}
