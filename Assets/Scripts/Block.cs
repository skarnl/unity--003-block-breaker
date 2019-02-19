using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //serialized fields
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int startingHealth;
    [SerializeField] Sprite[] hitSprites;
    
    //references
    private GameManager gameManagerReference;
    private SpriteRenderer spriteRenderer;

    //config
    private string BREAKABLE = "Breakable";
    private int currentHealth;

    private void Awake() {
        if (tag == BREAKABLE) {
            gameManagerReference = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            gameManagerReference.RegisterBlock(this);

            spriteRenderer = GetComponent<SpriteRenderer>();

            currentHealth = startingHealth;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleHit();
    }

    private void HandleHit() {
        if (tag == BREAKABLE) {
            currentHealth -= 1;
            
            if (currentHealth < 1) {
                DestroyBlock();
            }
            else {
                var spriteIndex = Mathf.Clamp(startingHealth - currentHealth - 1, 0, hitSprites.Length);
                spriteRenderer.sprite = hitSprites[spriteIndex];
            }
        }
    }

    private void DestroyBlock() {
        AudioSource.PlayClipAtPoint(destroySound, gameObject.transform.position);
        gameManagerReference.UnregisterBlock(this);
        TriggerSparkles();
        TriggerSparkles();
        TriggerSparkles();
        TriggerSparkles();
        TriggerSparkles();

        Destroy(gameObject);
    }

    private void TriggerSparkles() {
        Vector3 adjustment = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f), transform.position.z);

        GameObject particle = Instantiate(blockSparklesVFX, transform.position + adjustment, transform.rotation);
        Destroy(particle, 1f);
    }
}
