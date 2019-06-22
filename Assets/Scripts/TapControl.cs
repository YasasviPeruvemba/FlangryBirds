using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class TapControl : MonoBehaviour
{
    public float tapforce = 10;
    public float tiltsmooth = 2;
    public Vector3 startPos;
    Rigidbody2D rb;
    Quaternion downRotation;
    Quaternion forwardRotation;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public AudioSource tapAudio;
    public AudioSource scoreAudio;
    public AudioSource dieAudio;

    GameManager game;

    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
        GameManager.OnGameStart += OnGameStart;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
        GameManager.OnGameStart -= OnGameStart;
    }

    void OnGameStart()
    {
        rb.velocity = Vector3.zero;
        rb.simulated = true;
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0,0,-90);
        forwardRotation = Quaternion.Euler(0,0,40);
        game = GameManager.Instance;
        rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GameOver) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            //Time.timeScale += 1;
            tapAudio.Play();
            transform.rotation = forwardRotation;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * tapforce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltsmooth*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Obstacle")
        {
            dieAudio.Play();
            rb.simulated = false;
            //make player die
            OnPlayerDied(); //Event sent to Gamemanager
            //sound maybe
        }

        if(col.gameObject.tag == "Score")
        {
            //Increase player score
            scoreAudio.Play();
            OnPlayerScored(); //Event sent to Gamemanager
            //add sound
        }
    }
}
