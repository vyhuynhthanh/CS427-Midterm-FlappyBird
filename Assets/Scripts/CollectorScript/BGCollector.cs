using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour
{
    //array to store all the game object that has background and ground tag
    private GameObject[] backgrounds;
    private GameObject[] grounds;
    //get the last position x of the last ground and bg
    private float lastBGX;
    private float lastGroundX;
    //when we collide with any background, we'll get that position and set it to the last position

    void Awake()
    {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");

        lastBGX = backgrounds[0].transform.position.x;
        lastGroundX = grounds[0].transform.position.x;

        for(int i = 1; i < backgrounds.Length; ++i)
        {
            if(lastBGX < backgrounds[i].transform.position.x)
            {
                lastBGX = backgrounds[i].transform.position.x;
            }
        }

        for (int i = 1; i < grounds.Length; ++i)
        {
            if (lastGroundX < grounds[i].transform.position.x)
            {
                lastGroundX = grounds[i].transform.position.x;
            }
        }
    }

    //check when the bgCollector collide with the background
    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Background")
        {
            //get the position of the background that we collide
            Vector3 temp = target.transform.position;
            //casting the target into the box collider
            //converting the game obj in the box collider and get the size x of that collider
            float width = ((BoxCollider2D)target).size.x;

            temp.x = lastBGX + width;
            target.transform.position = temp;
            lastBGX = temp.x;
        }
        else if (target.tag == "Ground")
        {
            //get the position of the background that we collide
            Vector3 temp = target.transform.position;
            //casting the target into the box collider
            //converting the game obj in the box collider and get the size x of that collider
            float width = ((BoxCollider2D)target).size.x;

            temp.x = lastGroundX + width;
            target.transform.position = temp;
            lastGroundX = temp.x;
        }
    }
}
