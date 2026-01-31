using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Gravity_calculater1 : MonoBehaviour
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

    // Start is called before the first frame update
    private bool IsInRange_1()
    {
        return Physics2D.OverlapCircle(bindCheck.position, 0.2f, player_2);
    }

    private bool IsInRange_2()
    {
        return Physics2D.OverlapCircle(bind_Check2.position, 0.2f, player_1);
    }

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        combine1 = GameObject.FindGameObjectWithTag("Combine1");
        combine2 = GameObject.FindGameObjectWithTag("Combine2");
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();

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
    }

    void Connect()
    {
    
        Rigidbody2D rbcb1 = combine1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();

        isConnected = true;

        rbcb1.mass = rb1.mass - rb2.mass;
    }

    void Deconnect()
    {
        Rigidbody2D rb1 = player1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = player2.GetComponent<Rigidbody2D>();

        isConnected = false;
    }
}
