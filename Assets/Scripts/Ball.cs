using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;
    [SerializeField] AudioClip[] clips;
    [SerializeField] float startingSpeed = 15f;
    [SerializeField] float angle = 2f;
    [SerializeField] float randomFactor = .2f;
    [SerializeField] [Range (0f, 30f)] float ballSnelheidOnderwater = 10f;

    private bool launched = false;
    private Rigidbody2D rigidBody2D;
    private AudioSource audioSource;

    private Vector3 offset;

    void Start() {
        rigidBody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        offset = paddle.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched) {
            FollowPaddle();

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
                launched = true;

                rigidBody2D.velocity = new Vector2(angle, startingSpeed);
            }

        }
        
        Debug.Log("velo:" + rigidBody2D.velocity);
    }

    private void FollowPaddle () {
        transform.position = paddle.transform.position - offset;
    }
    
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (launched) {
            float x = rigidBody2D.velocity.x;
            float y = rigidBody2D.velocity.y;

            x = CorrectVelocityAfterBounce(x);
            y = CorrectVelocityAfterBounce(y);

            AudioClip clip = GetRandomAudioClip();
            audioSource.PlayOneShot(clip);

            rigidBody2D.velocity = new Vector2(x, y);
        }
    }

    private float CorrectVelocityAfterBounce(float number) {
        var oldNumber = number;
        if (number > -10f && number < 0) {
            number = Mathf.Round(number - 1f);
            Debug.Log($"Adjust from {oldNumber} to {number}");
        } else if (number > 0 && number < 10f) {
            number = Mathf.Round(number + 1f);
            Debug.Log($"Adjust from {oldNumber} to {number}");
        }

        return Mathf.Clamp(number, startingSpeed * -1, startingSpeed);
    }

    private AudioClip GetRandomAudioClip() {
        return clips[ Mathf.RoundToInt(UnityEngine.Random.Range(0, clips.Length )) ];
    }
}
