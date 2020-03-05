using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/********************
 * PlayerManager.cs
 * Type: Instance (Template from Base Instance)
 * Usage: Management of Player from Start to End of Application
 ********************/
public class PlayerManager : BaseInstance<PlayerManager> {
    /*** Variables ***/
    // Player's name at any one point in time in the game
    public string s_playerName { get; private set; }
    // Player's current score at any one point in time in the game
    public int i_playerScore { get; private set; }

    /*** Functions ***/
    // Set player's name
    public void SetPlayerName(string name) { s_playerName = name; }
    // Reset player's name
    public void ResetPlayerName() { s_playerName = "Player"; }
    // Addition of score
    public void AddScore(int score) { i_playerScore = i_playerScore + score; }
    // Reset Score
    public void ResetScore() { i_playerScore = 0; }
    // Setting up in Awake
    public void ManagerAwake() {
        s_playerName = "";
        i_playerScore = 0;
    }
}
