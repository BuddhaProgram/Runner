using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour {
    // Initial force applicable for running
    [SerializeField]
    private Vector3 m_initialRunSpeed = new Vector3(0, 0, 0);
    // Speed increment per tick
    [SerializeField]
    private Vector3 m_speedIncrement = new Vector3(0, 0, 0);
    // Time to increase speed
    [SerializeField]
    private float f_speedIncrementTime = 0f;
    // Timer to check for speed increment
    private float f_speedIncrementTimer = 0f;
    // Player's rigidbody
    [SerializeField]
    private Rigidbody2D m_playerBody = null;
    // Player's Sprite Animation
    [SerializeField]
    private Animator m_playerAniamtion = null;
    // Player's jumping force when jumping
    [SerializeField]
    private Vector2 m_jumpingForce = new Vector2(0, 0);
    [SerializeField]
    private Transform m_cameraTransform = null;
    // Checks whether the player is jumping
    private bool b_isJumping = false;
    // Checks whether the player releases jump button while on-air
    private bool b_stopJump = false;
    // Checks whether if it is paused
    private bool b_isPaused = false;
    // Player's velocity when pausing
    private Vector3 m_playerVelocity = new Vector3(0, 0, 0);
    // Player's state
    private PLAYER_STATE m_Player_State = PLAYER_STATE.None;
    // Function to check if player is on standby
    public bool IsPlayerOnStandby() { return m_Player_State == PLAYER_STATE.Standby; }
    // Returns player's running speed
    public float PlayerSpeed() { return m_initialRunSpeed.x; }
    // Function to call the player to start running
    public void StartRunning() {
        // Sets player animation to start running
        m_playerAniamtion.SetTrigger("Run");
        // Changes the player state to running
        m_Player_State = PLAYER_STATE.Running;
    }
    // Pause running
    public void PauseRunning() {
        // Stop animation
        m_playerAniamtion.enabled = false;
        // Stop time
        Time.timeScale = SystemControls.Instance.Zero;
        // Sets pause to true
        b_isPaused = true;
    }
    // Resume running
    public void ResumeRunning() {
        // Start animation
        m_playerAniamtion.enabled = true;
        // Resume Time
        Time.timeScale = SystemControls.Instance.One;
        // Sets pause to false
        b_isPaused = false;
    }
    // Checks if player has lost
    public bool HasPlayerLost() { return m_Player_State == PLAYER_STATE.Lose; }
    // Setting up in awake
    private void Awake() {
        // Speed increment timer
        f_speedIncrementTimer = 0f;
        // Player's State
        m_Player_State = PLAYER_STATE.Standby;
    }
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (!b_isPaused) {
            f_speedIncrementTimer += SystemControls.Instance.UTime * Time.deltaTime;
            if (f_speedIncrementTimer > f_speedIncrementTime)
            {
                f_speedIncrementTimer = SystemControls.Instance.Zero;
                m_initialRunSpeed += m_speedIncrement;
            }
        }
    }

    private void FixedUpdate() {
        if (m_Player_State != PLAYER_STATE.None && m_Player_State != PLAYER_STATE.Standby)
            m_playerBody.AddForce(m_initialRunSpeed, ForceMode2D.Force);

        // Update player based on switch case
        switch (m_Player_State) {
            case PLAYER_STATE.Running: {
                    if (!b_isJumping && !b_stopJump) {
                        if (Input.GetButton("Jump")) {
                            // Jumping is now true
                            b_isJumping = true;
                            // Sets animation trigger
                            m_playerAniamtion.SetTrigger("Jump");
                            // Changes player state
                            m_Player_State = PLAYER_STATE.Jumping;
                            // Input Force
                            m_playerBody.AddForce(m_jumpingForce, ForceMode2D.Impulse);
                        }
                    }
                    else if (b_stopJump) {
                        if (!Input.GetButton("Jump")) {
                            b_stopJump = false;
                        }
                    }

                } break;
            case PLAYER_STATE.Jumping: {
                    if (m_playerBody.velocity.y > 0f) {
                        if (Input.GetButtonUp("Jump")) {
                            // Putting a stop to jumping
                            b_stopJump = true;
                            // Change player state
                            m_Player_State = PLAYER_STATE.Cancel_Jump;
                        }
                    }
                }
                break;
            case PLAYER_STATE.Cancel_Jump: {
                    if (b_stopJump) {
                        b_stopJump = false;
                        // Sets the force-y to 0
                        m_playerBody.velocity = new Vector2(m_playerBody.velocity.x, 0);
                    }
                } break;
        }
    }
    private void LateUpdate() {
        // Camera update
        m_cameraTransform.position = new Vector3(transform.position.x, m_cameraTransform.position.y, m_cameraTransform.position.z);
    }
    // On Collision changes the state of the player
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        // Checks for tag
        if (collision.gameObject.tag == "Wall") {
            // Stop animation
            m_playerAniamtion.enabled = false;
            // Sleep rigidbody
            m_playerBody.Sleep();
            // Set to lose
            m_Player_State = PLAYER_STATE.Lose;
        }
        else {
            if (m_Player_State == PLAYER_STATE.Jumping || m_Player_State == PLAYER_STATE.Cancel_Jump) {
                // Cancels jumping
                b_isJumping = false;
                if (m_Player_State == PLAYER_STATE.Jumping) {
                    b_stopJump = true;
                }
                // Sends animation to running
                m_playerAniamtion.SetTrigger("Run");
                // Set player state back to running
                m_Player_State = PLAYER_STATE.Running;
            }
        }
    }
}
