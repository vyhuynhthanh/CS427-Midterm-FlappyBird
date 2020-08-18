using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    //to create an instance of our script
    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 3f;
    private float bounceSpeed = 4f;
    private bool didFlap;

    public bool isAlive;

    private Button flapButton;

    void Awake()
    {
        //This will slow our game if we have many getComponet
        //myRigidBody = GetComponent<Rigidbody2D>();

        //If we don't have a script pointing to this instance
        if (instance == null)
        {
            instance = this;
        }
        isAlive = true;

        //get the button component of the FlapButton object
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        //Add the listener to the button: so that when we touch the screen, the bird is flapping and fly (for every bird object)
        flapButton.onClick.AddListener(() => FlapTheBird());

        SetCameraX();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called every 2 or 3 frames
    void FixedUpdate()
    {
        //if our bird is alive => move it to the right
        if (isAlive)
        {
            //get the current position
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;
        }
        //if our bird need to flap
        if (didFlap)
        {
            //the bird flap once every time we touch the screen
            //if the bird is already flap, we stop
            didFlap = false;
            myRigidBody.velocity = new Vector2(0, bounceSpeed);
            anim.SetTrigger("Flap");
        }
    }

    void SetCameraX()
    {
        //the position of the camera minus the position of our bird
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void FlapTheBird()
    {
        didFlap = true;
    }
}
