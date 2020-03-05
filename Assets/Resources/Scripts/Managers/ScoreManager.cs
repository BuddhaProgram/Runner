using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ScoreManager : BaseInstance<ScoreManager> {
    // List of player names and score
    private List<Dictionary<string, int>> List_Leaderboard = new List<Dictionary<string, int>>();
    // PlayerPrefs key names
    private string s_playerNames = "";
    // String of names
    private string[] s_storedNames;
    // PlayerPrefs key score
    private string s_playerScores = "";
    // Initialization
    public void Init(string name, string score, int max) {
        // INitialize keys
        s_playerNames = name;
        s_playerScores = score;
        List_Leaderboard = new List<Dictionary<string, int>>();
        // Assignment of variables
        s_storedNames = new string[max];
        // Checks if there is a key in Player Prefs for name
        if (PlayerPrefs.HasKey(name)) {
            // Checks if there is a key in Player Prefs for score
            if (PlayerPrefs.HasKey(score)) {
                // Enters it into the dictionary
                for (int i = 0; i < PlayerPrefsX.GetIntArray(score).Length; ++i) {
                    // Creates a new dictionary
                    Dictionary<string, int> highscore = new Dictionary<string, int>();
                    // Get values in player prefs
                    highscore.Add(PlayerPrefsX.GetStringArray(name)[i], PlayerPrefsX.GetIntArray(score)[i]);
                    // Add into list
                    List_Leaderboard.Add(highscore);
                    // Adds to stored names
                    s_storedNames[i] = PlayerPrefsX.GetStringArray(name)[i];
                }
            }
        }
        else if (!PlayerPrefs.HasKey(name) || !PlayerPrefs.HasKey(score)) {
            // Clears both keys
            PlayerPrefs.DeleteAll();
            // Creates own highscore list
            for (int i = 0; i < max; ++i) {
                // Creates a new dictionary
                Dictionary<string, int> highscore = new Dictionary<string, int>();
                // Creates own highscore
                highscore.Add("Player " + i.ToString(), 0);
                // Add to list
                List_Leaderboard.Add(highscore);
                // Adds to string array
                s_storedNames[i] = "Player " + i.ToString();
            }
        }
    }

    public void Test() {
        for (int i = 0; i < s_storedNames.Length; ++i) {
            Debug.Log(s_storedNames[i] + " " + List_Leaderboard[i][s_storedNames[i]]);
        }
    }

    public void Save() {
        // Delete all keys
        PlayerPrefs.DeleteAll();
        // Array of score
        int[] scores = new int[List_Leaderboard.Count];
        // Loops through the stored names to get the scores
        for (int i = 0; i < s_storedNames.Length; ++i) {
            scores[i] = List_Leaderboard[i][s_storedNames[i]];
        }
        // Save score
        PlayerPrefsX.SetIntArray(s_playerScores, scores);
        // Save names
        PlayerPrefsX.SetStringArray(s_playerNames, s_storedNames);
    }

    public void DisplayLeaderboard(TextMeshProUGUI[] names, TextMeshProUGUI[] scores) {
        for (int i = 0; i < List_Leaderboard.Count; ++i) {
            names[i].text = s_storedNames[i];
        }
        for (int j = 0; j < List_Leaderboard.Count; ++j) {
            scores[j].text = List_Leaderboard[j][s_storedNames[j]].ToString();
        }
    }

    public void InsertHighScore(string name, int score) {
        for (int i = 0; i < s_storedNames.Length; ++i) {
            if (List_Leaderboard[i][s_storedNames[i]] <= score) {
                for (int j = List_Leaderboard.Count - 1; j > i; --j) {
                    s_storedNames[j] = s_storedNames[j - 1];
                    Debug.Log(j + " - Before: " + s_storedNames[j - 1] + " After: " + s_storedNames[j]);
                }
                Dictionary<string, int> dict = new Dictionary<string, int>();
                dict.Add(name, score);
                List_Leaderboard.Insert(i, dict);
                List_Leaderboard.RemoveAt(s_storedNames.Length);
                s_storedNames[i] = name;
                break;
            }
        }

        for (int i = 0; i < s_storedNames.Length; ++i)
            Debug.Log(s_storedNames[i]);
    }

    public void DeleteAll() { PlayerPrefs.DeleteAll(); }
}
