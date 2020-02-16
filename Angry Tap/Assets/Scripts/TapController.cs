using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startPos;
    public AudioSource startAudio;
    public AudioSource tapAudio;
    public AudioSource scoreAudio;
    public AudioSource dieAudio;

    public Animator anim;


    Rigidbody2D rb;
    Quaternion downRotation;
    Quaternion forwardRotation;

    GameManager game;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
        rb.simulated = false;

    }
    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }
    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        startAudio.Play();
        rb.velocity = Vector3.zero;
        rb.simulated = true;

    }
    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
        anim.SetTrigger("Tap");
    }
    void Update()
    {
        if (game.GameOver) return;


        if (Input.GetMouseButtonDown(0))
        {
            tapAudio.Play();
            transform.rotation = forwardRotation;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
            anim.SetTrigger("Tap");
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ScoreZone")
        {
            //register a score event
            OnPlayerScored(); //event sent to GameManager
            //play a sound
            scoreAudio.Play();
        }
        if (col.gameObject.tag == "DeadZone")
        {
            rb.simulated = false;
            //register a dead event
            OnPlayerDied(); //event sent to GameManager
            anim.SetTrigger("Die");
            //play a sound
            dieAudio.Play();
        }
    }

    
}
