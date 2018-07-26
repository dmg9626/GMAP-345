using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacManMoveComponent : MoveComponent {

    private Rigidbody2D rbody;

    private GameController gameController;

    public Intersection intersection;

    public int score;

    public Text scoreText;

    public AudioSource audioSource;

    public Image scott;

    public float fadeDelta;

    // Use this for initialization
    void Start() {
        base.Start();

        animator.speed = animateSpeed;

        rbody = GetComponent<Rigidbody2D>();

        gameController = GameController.FindObjectOfType<GameController>();

        SetAlpha(scott, 0);

        if(!PelletsLeft()) {
            InvokeRepeating("FadeIn", Time.deltaTime, Time.deltaTime);
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    /// <summary>
    /// Moves/animates actor based on button presses
    /// </summary>
    override
    public void ManageMovement()
    {
        Vector2 input = GetInput();

        BaseConstants.Direction direction = DirectionHelper.VectorToDirection(input);

        if (direction != BaseConstants.Direction.None) {
            Move(direction);

            // Animate walking if receiving input
            if (!currentDirection.Equals(BaseConstants.Direction.None)) {
                AnimateMovement();
            }
        }
    }

    /// <summary>
    /// Returns player input from arrow keys/WASD
    /// </summary>
    protected Vector2 GetInput()
    {
        // Get player input
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            return Vector2.up;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            return Vector2.left;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            return Vector2.down;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            return Vector2.right;
        }
        return input;
    }

    /// <summary>
    /// Moves player in specified direction.
    /// </summary>
    /// <param name="direction">The direction.</param>
    protected void Move(BaseConstants.Direction direction)
    {
        // Move actor in direction of input
        Vector2 movement = DirectionHelper.DirectionToVector(direction) * moveSpeed;
        rbody.velocity = movement;

        // Save direction to use next frame
        currentDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if pellet
        if (collider.gameObject.GetComponent<Pellet>()) {
            HandlePellet(collider.gameObject.GetComponent<Pellet>());
        }

        // Check if warp pipe
        else if (collider.gameObject.GetComponent<Warp>()) {
            Warp warp = collider.gameObject.GetComponent<Warp>();

            Debug.Log("Warping to " + warp.destination.name);
            transform.position = warp.destination.position;
        }
    }

    /// <summary>
    /// Called when player collides with pellet
    /// </summary>
    /// <param name="pellet">The pellet.</param>
    protected void HandlePellet(Pellet pellet)
    {
        // Boost speed/points gained if powerup
        if (pellet.powerUp) {
            score += gameController.powerUpScore;

            //moveSpeed += gameController.powerUpSpeedIncrease;
        }
        else {
            score += gameController.pelletScore;
        }

        // Set pellet inactive
        pellet.gameObject.SetActive(false);

        if (!PelletsLeft()) {
            // Display win message
            Debug.Log("you found all the pellets!");
            //scoreText.text = "You Win!";

            audioSource.PlayOneShot(audioSource.clip);

            InvokeRepeating("FadeIn", Time.deltaTime, Time.deltaTime);
        }
        else {
            // Update score text
            scoreText.text = score.ToString();
        }
    }

    /// <summary>
    /// Returns true if player can move in given direction at this intersection, false otherwise
    /// </summary>
    /// <param name="direction">The direction.</param>
    /// <param name="intersection">The intersection.</param>
    protected bool CanMove(BaseConstants.Direction direction)
    {
        if (intersection != null) {
            return intersection.validDirections.Contains(direction);
        }
        return false;
    }


    /// <summary>
    /// Animates movement based on horizontal/vertical input
    /// </summary>
    /// <param name="input">Player input</param>
    protected void AnimateMovement()
    {
        // Calculcate angle of rotation based on current direction
        Vector3 rotation = new Vector3(0, 0, DirectionHelper.DirectionToRotation(currentDirection) ?? 0);

        // Assign rotation to quaternion.eulerAngles
        Quaternion rot = transform.rotation;
        rot.eulerAngles = rotation;
        transform.rotation = rot;
    }

    /// <summary>
    /// Slowly fades in image of scott pilgrim
    /// </summary>
    protected void FadeIn()
    {
        SetAlpha(scott, scott.color.a + ((fadeDelta / audioSource.clip.length) * Time.deltaTime));

        // Quit game when audio clip done playing
        if(!audioSource.isPlaying) {
            Application.Quit();
        }
    }

    /// <summary>
    /// Sets alpha value of image to given value
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="alpha">The alpha.</param>
    protected void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        //color.r += alpha;

        scott.color = color;
    }

    /// <summary>
    /// Returns true if pellets are left on the board, false otherwise
    /// </summary>
    /// <returns></returns>
    bool PelletsLeft()
    {
        return GameObject.FindObjectOfType<Pellet>() != null;
    }
}
