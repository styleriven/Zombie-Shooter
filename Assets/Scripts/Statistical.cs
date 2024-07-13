using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Statistical : MonoBehaviour {
    private static Statistical instance;

    private int score;


    private int monsters;



    TMP_Text scoreText;

    TMP_Text monstersText;

    public static Statistical Instance { get => instance; set => instance = value; }
    public int Score { get => score; set => score = value; }
    public int Monsters { get => monsters; set => monsters = value; }

    public void IncreaseScore(int amountToIncrease) {
        Score += amountToIncrease;
        if(scoreText != null) {
            scoreText.text = Score.ToString();
        }
    }
    public void IncreaseMonters() {
        Monsters++;
        if(monstersText != null) {
            monstersText.text = Monsters.ToString();
        }
    }
    void Awake() {
        if(Statistical.Instance != null) {
            Debug.LogError("Only 1 Scores instance is allowed");
        }
        Statistical.Instance = this;
        score = 0;
        monsters = 0;
        //DontDestroyOnLoad(gameObject);
    }


    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnLoading(); 
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        OnLoading();
    }
    private void OnLoading() {
        Transform game = GameObject.Find("Canvas").transform.Find("Game");
        scoreText = game.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        monstersText = game.GetChild(1).GetChild(1).GetComponent<TMP_Text>();
    }
    void Start() {
        if(scoreText != null) {
            scoreText.text = Score.ToString();
        }
        if(monstersText != null) {
            monstersText.text = Monsters.ToString();
        }
    }
}
