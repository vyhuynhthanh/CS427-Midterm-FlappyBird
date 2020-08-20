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

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip flapClick, pointClip, diedClip;

    public int score;
    

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
        score = 0;

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

            if (didFlap)
            {
                //the bird flap once every time we touch the screen
                //if the bird is already flap, we stop
                didFlap = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                audioSource.PlayOneShot(flapClick);
                anim.SetTrigger("Flap");
            }

            //Make the bird face downward when it's falling
            if (myRigidBody.velocity.y >= 0)
            {
                //use quaternion for angle
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                float angle = 0;
                //create angle: lerp will go from 0 to -90 in the given time
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 10);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
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

    //when our bird collide (died)
    void OnCollisionEnter2D (Collision2D target)
    {
        if(target.gameObject.tag=="Ground" || target.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Died");
                audioSource.PlayOneShot(diedClip);
                GamePlayController.instance.PlayerDiedShowScore(score);
            }
        }
    }

    //bird gaining score
    void OnTriggerEnter2D(Collider2D target)
    {
         if(target.tag == "PipeHolder")
        {
            score++;
            GamePlayController.instance.SetScore(score);
            audioSource.PlayOneShot(pointClip);
        }
    }
}
