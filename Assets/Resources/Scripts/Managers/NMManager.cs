using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/********************
 * NMManager.cs
 * Type: Instance (Template from Base Instance)
 * Usage: Management of all non-monobehaviour scripts
 ********************/
public class NMManager : BaseInstance<NMManager> {
    // Classes of Base Non-monobehaviour classes
    private List<BaseNM> List_BaseNMs = new List<BaseNM>();
    // Adds a new class base and rearranges according to priority
    public void AddToList(BaseNM newBase, bool arrange) {
        // Adds to our current list
        List_BaseNMs.Add(newBase);
        if (arrange) {
            // Assigns a new list for arrangement
            List<BaseNM> newList = new List<BaseNM>();
            // Uses a fall-loop to go through the count of list
            foreach (BaseNM nm in List_BaseNMs) {
                // Goes through the run queue priority
                for (int i = 0; i < Enum.GetValues(typeof(RunQueue_Priority)).Length; ++i) {
                    if ((int)nm.Priority() == i) {
                        // Adds to new list according to priority
                        newList.Add(nm);
                    }
                }
            }
            // Clears the list of base
            List_BaseNMs.Clear();
            // Resets the new base list
            List_BaseNMs = new List<BaseNM>(newList);
        }
    }
    // Change in Request for priority, and rearranges the priority
    public void RequestChangeInPriority(BaseNM requestor, RunQueue_Priority priority) {
        // Requests
        requestor.ChangePriority(priority);
        // Assigns a new list for arrangement
        List<BaseNM> newList = new List<BaseNM>();
        // Uses a fall-loop to go through the count of list
        foreach (BaseNM nm in List_BaseNMs) {
            // Goes through the run queue priority
            for (int i = 0; i < Enum.GetValues(typeof(RunQueue_Priority)).Length; ++i) {
                if ((int)nm.Priority() == i) {
                    // Adds to new list according to priority
                    newList.Add(nm);
                }
            }
        }
        // Clears the list of base
        List_BaseNMs.Clear();
        // Resets the new base list
        List_BaseNMs = new List<BaseNM>(newList);
    }
    // Clears the list
    public void ClearNMList() { List_BaseNMs.Clear(); }
    // Initializes NMManager
    public void ManagerAwake() { List_BaseNMs = new List<BaseNM>(); }
    // Setting up in Awake
    public void Awake() {
        foreach (BaseNM nm in List_BaseNMs)
            nm.Awake();
    }
    // Runs Init based on Priority
    public void Init() {
        foreach (BaseNM nm in List_BaseNMs) {
            nm.Init();
        }
    }
    // Runs Update based on Priority
    public void RunUpdate() {
        foreach (BaseNM nm in List_BaseNMs) {
            nm.RunUpdate();
        }
    }
    // Destroys on application
    public void DestroyOnEnd() {
        foreach (BaseNM nm in List_BaseNMs) {
            nm.DestroyOnEnd();
        }
    }
}
