using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundThroughScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    //Play Global
    private static SoundThroughScene instance = null;
    public static SoundThroughScene Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance !=null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
