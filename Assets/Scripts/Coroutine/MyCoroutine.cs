using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyCoroutine
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        //get the duration of our game since starting
        //(ex: the game has start for 5s)
        float start = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < (start + time))
        {
            //skip each frame
            yield return null;    
        }
    }
}
