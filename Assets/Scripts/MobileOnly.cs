using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer
        ) {
            return;
        }

        gameObject.SetActive(false);
    }
}
