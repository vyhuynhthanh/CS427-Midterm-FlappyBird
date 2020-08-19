using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour
{
    //array of all the pipe holders
    private GameObject[] pipeHolders;
    private float distance = 2f;
    private float lastPipeX;
    //y-axis
    private float pipeMin = -0.93f;
    private float pipeMax = 1.1f;

    void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        //mix the pipe position randomly
        for (int i = 0; i < pipeHolders.Length; ++i)
        {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[i].transform.position = temp;
        }

        lastPipeX = pipeHolders[0].transform.position.x;

        for (int i = 1; i < pipeHolders.Length; ++i)
        {
            if (lastPipeX < pipeHolders[i].transform.position.x)
            {
                lastPipeX = pipeHolders[i].transform.position.x;
            }
        }
    }

    //check when we collide with the pipe
    void OnTriggerEnter2D (Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            //get the position of the pipe holder
            Vector3 temp = target.transform.position;
            //set the x-position of the pipe holder to be the position of the last pipe holder plus the distance between 2 pipes 
            temp.x = lastPipeX + distance;
            //re-assign the y-position
            temp.y = Random.Range(pipeMin, pipeMax);
            //re-assign to our pipe holder
            target.transform.position = temp;
            lastPipeX = temp.x;
  
        }
    }
}
