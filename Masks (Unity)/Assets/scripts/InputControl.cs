using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class InputControl : MonoBehaviour
{
    public bool isConnecting_1 = false;
    public bool isConnecting_2 = false;
    public bool isInRange_1 = false;
    public bool isInRange_2 = false;
    public bool isConnected = false;

    [SerializeField]
    private Transform bindCheck;

    [SerializeField]
    private Transform bind_Check2;

    [SerializeField]
    private LayerMask player_2;

    [SerializeField]
    private LayerMask player_1;

    GameObject player1;
    GameObject player2;
    GameObject combine1;
    GameObject combine2;

    public GameObject heavierPlayer;
    public GameObject lighterPlayer;
    public GameObject leader;
    public GameObject follower;
    public Vector3 relativeOffset;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        combine1 = GameObject.FindGameObjectWithTag("Combine1");
        combine2 = GameObject.FindGameObjectWithTag("Combine2");
    }

    private bool IsInRange_1()
    {
        return Physics2D.OverlapCircle(bindCheck.position, 0.2f, player_2);
    }

    private bool IsInRange_2()
    {
        return Physics2D.OverlapCircle(bind_Check2.position, 0.2f, player_1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isConnecting_1 = !isConnecting_1;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            isConnecting_2 = !isConnecting_2;
        }

        if (!isConnected && isConnecting_1 && isConnecting_2 && IsInRange_1() && IsInRange_2())
            Connect();

        if (isConnected && !isConnecting_1 || !isConnecting_2 && isConnected)
            Deconnect();

        //if (isConnected)
            //Synchronization();
    }

    void Connect()
    {
        // 改变父对象为Mass大的一方，将子对象移动至父对象的pivot, 最后禁用子对象


        isConnected = true;
        Synchronization();

        
            Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();

            if (rb1 == null || rb2 == null)
            {
                Debug.LogError("Both players must have a Rigidbody component.");
                return;
            }

            if (rb1.mass > rb2.mass)
            {
                heavierPlayer = player1;
                lighterPlayer = player2;
            }
            else
            {
                heavierPlayer = player2;
                lighterPlayer = player1;
            }
    }

    void Deconnect()
    {
        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();
        BoxCollider2D bccb1 = combine1.GetComponent<BoxCollider2D>();
        BoxCollider2D bccb2 = combine2.GetComponent<BoxCollider2D>();
        BoxCollider2D bc1 = player1.GetComponent<BoxCollider2D>();
        BoxCollider2D bc2 = player2.GetComponent<BoxCollider2D>();
        Playermovement playermovement = player1.GetComponent<Playermovement>();
        Player2movement player2movement = player2.GetComponent<Player2movement>();
        Combinemovement combinemovement = combine1.GetComponent<Combinemovement>();
        Combine2movement combine2movement = combine2.GetComponent<Combine2movement>();
        Rigidbody2D rbcb2 = combine2.GetComponent<Rigidbody2D>();
        Rigidbody2D rbcb1 = combine1.GetComponent<Rigidbody2D>();
        SpriteRenderer sprite1 = player1.GetComponent<SpriteRenderer>();
        SpriteRenderer sprite2 = player2.GetComponent<SpriteRenderer>();
        SpriteRenderer spritecb1 = combine1.GetComponent<SpriteRenderer>();
        SpriteRenderer spritecb2 = combine2.GetComponent<SpriteRenderer>();

        isConnected = false;
        isConnecting_1 = !isConnecting_1;
        isConnecting_1 = !isConnecting_2;

        if (rb1.mass > rb2.mass)
        {
            player1.transform.SetParent(null);
            player2.transform.SetParent(null);

            combine1.transform.SetParent(player1.transform);
            rb2.isKinematic = false;
            rb1.isKinematic = false;
            rbcb1.isKinematic = true;
            isConnecting_1 = false;
            isConnecting_2 = false;
            bccb1.enabled = false;
            bc1.enabled = true;
            bc2.enabled = true;
            combinemovement.enabled = false;
            playermovement.enabled = true;
            player2movement.enabled = true;
            spritecb1.enabled = false;
            sprite1.enabled = true;
            sprite2.enabled = true;
            
           

        }
        else
        {
            player1.transform.SetParent(null);
            player2.transform.SetParent(null);

            combine2.transform.SetParent(player2.transform);

            rb2.isKinematic = false;
            rb1.isKinematic = false;
            rbcb2.isKinematic = true;
            isConnecting_1 = false;
            isConnecting_2 = false;
            bccb2.enabled = false;
            bc1.enabled = true;
            bc2.enabled = true;
            combine2movement.enabled = false;
            playermovement.enabled = true;
            player2movement.enabled = true;
            spritecb2.enabled = false;
            sprite1.enabled = true;
            sprite2.enabled = true;
        }
    }

    void Synchronization()
    {
        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();
        Rigidbody2D rbcb2 = combine2.GetComponent<Rigidbody2D>();
        Rigidbody2D rbcb1 = combine1.GetComponent<Rigidbody2D>();
        BoxCollider2D bccb1 = combine1.GetComponent<BoxCollider2D>();
        BoxCollider2D bccb2 = combine2.GetComponent<BoxCollider2D>();
        BoxCollider2D bc1 = player1.GetComponent<BoxCollider2D>();
        BoxCollider2D bc2 = player2.GetComponent<BoxCollider2D>();
        Playermovement playermovement = player1.GetComponent<Playermovement>();
        Player2movement player2movement = player2.GetComponent<Player2movement>();
        Combinemovement combinemovement = combine1.GetComponent<Combinemovement>();
        Combine2movement combine2movement = combine2.GetComponent<Combine2movement>();
        SpriteRenderer sprite1 = player1.GetComponent<SpriteRenderer>();
        SpriteRenderer sprite2 = player2.GetComponent<SpriteRenderer>();
        SpriteRenderer spritecb1 = combine1.GetComponent<SpriteRenderer>();
        SpriteRenderer spritecb2 = combine2.GetComponent<SpriteRenderer>();


        if (rb1 == null || rb2 == null)
        {
            Debug.LogError("Both players must have a Rigidbody component.");
            return;
        }

        if (rb1.mass > rb2.mass)
        {
            combine1.transform.SetParent(null);

            player1.transform.SetParent(combine1.transform);
            player2.transform.SetParent(combine1.transform);

            player2.transform.position = combine1.transform.position;
            player1.transform.position = combine1.transform.position;
            //Debug.Log(combine1.transform.position);

            if (rb1 != null)
            {
                rb1.velocity = Vector3.zero;
                rb1.isKinematic = true;
                bc1.enabled = false;
            }

            if (rb2 != null)
            {
                rb2.velocity = Vector3.zero;
                rb2.isKinematic = true;
                bc2.enabled = false;
            }
            rbcb1.isKinematic = false;
            rbcb1.mass = rb1.mass + rb2.mass;
            rbcb1.gravityScale = (rb1.gravityScale*rb1.mass + rb2.gravityScale*rb2.mass)/(rb1.mass + rb2.mass);
            bccb1.enabled = true;
            playermovement.enabled = false;
            player2movement.enabled = false;
            combinemovement.enabled = true;
            sprite1.enabled = false;
            sprite2.enabled = false;
            spritecb1.enabled = true;
        }
        else
        {
            combine2.transform.SetParent(null);

            player1.transform.SetParent(combine2.transform);
            player2.transform.SetParent(combine2.transform);
            player1.transform.position = combine2.transform.position;
            player2.transform.position = combine2.transform.position;

            if (rb1 != null)
            {
                rb1.velocity = Vector3.zero;
                rb1.isKinematic = true;
                bc1.enabled = false;
            }

            if (rb2 != null)
            {
                rb2.velocity = Vector3.zero;
                rb2.isKinematic = true;
                bc2.enabled = false;
            }
            rbcb2.isKinematic = false;
            rbcb2.mass = rb2.mass + rb1.mass;
            //rbcb2.gravityScale = rb2.gravityScale;
            rbcb2.gravityScale = (rb2.gravityScale*rb2.mass + rb1.gravityScale*rb1.mass)/(rb1.mass + rb2.mass);
            bccb2.enabled = true;
            playermovement.enabled = false;
            player2movement.enabled = false;
            combine2movement.enabled = true;
            sprite1.enabled = false;
            sprite2.enabled = false;
            spritecb2.enabled = true;
        }
    }
}
