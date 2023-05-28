using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenControls : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetInt("OnScreenControls", 0);
    }

    public void OnValueChanged(float value)
    {
        PlayerPrefs.SetInt("OnScreenControls", (int) value);
    }
}
