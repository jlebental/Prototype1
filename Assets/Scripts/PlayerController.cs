using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //good practice is t keep variables public while testing
    //and private when you have the values you like
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float speed;
    [SerializeField] float horsepower = 0;
    [SerializeField] float turnSpeed = 45.0f;
    private Rigidbody playerRb;
    private float horizontalInput;
    private float forwardInput;
    private int rpm;
    [SerializeField] GameObject centerOfMass;
    public List<WheelCollider> allWheels;
    public int wheelsOnGround;
    //[SerializeField] is a keyword which will make your private
    //variables visible in the inspector while not being public
    //TODO: try more keywords: const(duh), readonly(duh), static(makes a variable global across scripts)

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.Find("Vehicle").GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }
    //TODO: try and find a purpose for another protecteed monobehaviour method: Awake()
    //Awake() is like start but activates in response to something like a button press
    //instead of happening at the start of the game

    // Update is called once per frame
    void FixedUpdate()  //FixedUpdate is an update that occurs right before Update()
        //FixedUpdate is ideal for calculating physics and movement
    {
        if (WheelsOnGround())
        {

            //Input class refers to our input manager.
            //See Unity/edit/projectSettings/Input to see more Input Axes
            //this is where we get player input
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            //this is where we move the vehicle forward and rotate
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRb.AddRelativeForce(Vector3.forward * horsepower * forwardInput);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
            //turnspeed is the static rate (as it is set for now) that you turn
            //horizontalInput decides the direction you turn because these
            //inputs come in as either 1,0, or -1
            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f);   //change 2.237 to 3.6 to go from mph to kmph
            speedometerText.SetText("Speed: " + speed + " mph");

            rpm = Mathf.RoundToInt((speed % 30) * 40);
            rpmText.SetText("RPM: " + rpm);
        }
    }

    bool WheelsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                ++wheelsOnGround;
            }
        }
        if (wheelsOnGround == 4)
            return true;
        else
            return false;
    }
}
