using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileOnly : MonoBehaviour
{
    [SerializeField]
    private PlayerPrefs playerPrefs;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("OnScreenControls", 0));
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer ||
            PlayerPrefs.GetInt("OnScreenControls", 0) == 1
        ) {
            return;
        }

        gameObject.SetActive(false);
    }
}
