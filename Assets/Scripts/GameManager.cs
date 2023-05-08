using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this; // this is the current instance of the class
            DontDestroyOnLoad(gameObject); // don't destroy this object when loading a new scene

            return;
        }

        Destroy(gameObject); // destroy the new instance of the class
    }
}
