using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DimensionShiftReady : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private string readyText = "DIMENSION SHIFT READY";

    [SerializeField]
    private Color readyColor;

    [SerializeField]
    private string notReadyText = "DIMENSIONAL DISTORTION";

    [SerializeField]
    private Color notReadyColor;

    [SerializeField]
    private TMP_Text textField;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.IsTouchingInActiveDimension()) {
            textField.text = notReadyText;
            textField.color = notReadyColor;
        } else
        {
            textField.text = readyText;
            textField.color = readyColor;
        }
    }
}
