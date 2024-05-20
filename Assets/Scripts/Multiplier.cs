using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Multiplier : MonoBehaviour
{
    private static TMP_Text tMP_Text;
    private static int multiply = 1, highScore = 1;
    private static Animator animator;
    public static float    timeElapsed, startTime;
    // Start is called before the first frame update
    void Start() {
        tMP_Text = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();
        tMP_Text.text = multiply.ToString();
        startTime = Time.time;
        highScore = multiply;
        if (highScore > LoadHighScore())
            SaveHighScore(highScore);
    }
    public static int getHighScore() {
        return highScore;
    }
    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public static void addMultiplier(int value) {
        if (animator)
            animator.SetTrigger("score");
        multiply += value;
        tMP_Text.text = multiply.ToString();
        if (multiply > highScore)
            highScore = multiply;
        if (highScore > LoadHighScore())
            SaveHighScore(highScore);
    }

    public static void setMultiplier(int value) {
        multiply = value;
        tMP_Text.text = multiply.ToString();
        if (highScore > LoadHighScore())
            SaveHighScore(highScore);
    }

    public static int getMultiplier() {
        return multiply;
    }

    public static void TimeStamp() {
        timeElapsed = Time.time - startTime;
        if (!PlayerPrefs.HasKey(QuickTime) || timeElapsed < LoadQuickTime()) {
            SaveQuickTime(Time.time - startTime);
        }
    }

    public static float getTimeElapsed() {
        return timeElapsed;
    }

    private const string HighScoreKey = "HighScore";

    // Call this method when the player achieves a new high score
    public static void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(HighScoreKey, score);
        PlayerPrefs.Save();
    }

    // Call this method to get the high score
    public static int LoadHighScore()
    {
        return PlayerPrefs.HasKey(HighScoreKey) ? PlayerPrefs.GetInt(HighScoreKey) : 0;
    }

    private const string QuickTime = "QuickTime";

    // Call this method when the player achieves a new high score
    public static void SaveQuickTime(float time)
    {
        PlayerPrefs.SetFloat(QuickTime, time);
        PlayerPrefs.Save();
    }

    // Call this method to get the high score
    public static float LoadQuickTime()
    {
        Debug.Log("Loading: " + PlayerPrefs.GetFloat(QuickTime));
        return PlayerPrefs.HasKey(QuickTime) ? PlayerPrefs.GetFloat(QuickTime) : timeElapsed;
    }
}
