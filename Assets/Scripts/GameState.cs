using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField, Range(0.5f,2.5f)]
    private float speed = 1.1f;

    [SerializeField]
    private int pointsPerBlockDestroyed = 10;

    [SerializeField] Text scoreText;

    [SerializeField] int currentScore = 0;

    void Awake() {
        if (FindObjectsOfType <GameState>().Length > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = speed;
    }

    public void Reset() {
        currentScore = 0;

        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreText.text = currentScore.ToString();
    }

    public void OnBlockDestroyed() {
        currentScore += pointsPerBlockDestroyed;

        UpdateScoreText();
    }
}
