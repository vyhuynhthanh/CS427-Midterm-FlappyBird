using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //this offset is calculated in the birdscript
    public static float offsetX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the bird exists in the game
        if(BirdScript.instance != null)
        {
            //if the bird is alive, the camera will follow the bird
            if (BirdScript.instance.isAlive)
            {
                MoveTheCamera();
            }
        }
    }

    void MoveTheCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX() + offsetX;
        transform.position = temp;
    }
}
