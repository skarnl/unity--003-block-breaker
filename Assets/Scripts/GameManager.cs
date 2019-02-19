using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<Block> blocks;
    
    // [SerializeField]
    // private Text scoreText;

    private GameState gameStateReference;

    private GameManager() {
        blocks = new List<Block>();
    }

    public void Start() {
        gameStateReference = FindObjectOfType<GameState>();
    }

    public void RegisterBlock(Block block) {
        blocks.Add(block);
    }

    public void UnregisterBlock(Block block) {
        blocks = blocks.Where( x => x != block).ToList();

        gameStateReference.OnBlockDestroyed();

        if (this.blocks.Count() == 0) {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings - 2) {
                SceneManager.LoadScene("Scenes/X2. Win");
            } else {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }
}
