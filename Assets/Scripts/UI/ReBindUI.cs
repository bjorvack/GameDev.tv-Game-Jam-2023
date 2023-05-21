using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.Utilities;

public class ReBindUI : MonoBehaviour
{
    [SerializeField]
    private InputActionReference inputActionReference;

    [SerializeField]
    private string actionNameOverride;

    [SerializeField]
    private bool excludeMouse = true;

    [Range(0, 10)]
    [SerializeField]
    private int selectedBinding;

    [SerializeField]
    private InputBinding.DisplayStringOptions displayStringOptions;

    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField]
    private InputBinding inputBinding;

    [SerializeField]
    private int bindingIndex;

    private string actionName;

    [Header("UI Fields")]
    [SerializeField]
    private TMP_Text actionText;

    [SerializeField]
    private Button rebindButton;

    [SerializeField]
    private TMP_Text rebindText;

    [SerializeField]
    private Button resetButton;

    private List<string> PossibleBindings()
    {
        var list = new List<string>();

        foreach (var binding in inputActionReference.action.bindings) {
            list.Add(binding.name);
        }

        return list;
    }

    private void OnEnable()
    {
        rebindButton.onClick.AddListener(() => DoRebind());
        resetButton.onClick.AddListener(() => ResetBinding());

        if (inputActionReference != null) {
            if (actionName == null)
            {
                GetBindingInfo();
            }

            InputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }

        InputManager.rebindComplete += UpdateUI;
        InputManager.rebindCanceled += UpdateUI;
    }

    private void OnDisable()
    {
        InputManager.rebindComplete -= UpdateUI;
        InputManager.rebindCanceled -= UpdateUI;
    }

    private void OnValidate()
    {
        if (inputActionReference == null) {
            return;
        }

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if (inputActionReference.action != null) {
            actionName = inputActionReference.action.name;
        }

        if (inputActionReference.action.bindings.Count > selectedBinding) {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    private void UpdateUI()
    {
        if (actionText != null) {
            actionText.text = actionName;
            if (actionNameOverride != null && actionNameOverride != "")
            {
                actionText.text = actionNameOverride;
            }
        }

        if (rebindText != null) {
            if (Application.isPlaying)
            {
                // Grap info from Input manager
                rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
            } else {
                rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
            }
        }
    }

    private void ResetBinding()
    {
        if (actionName == null)
        {
            GetBindingInfo();
        }

        InputManager.ResetBinding(actionName, bindingIndex);
        UpdateUI();
    }

    private void DoRebind()
    {
        if (actionName == null)
        {
            GetBindingInfo();
        }

        InputManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse);
    }
}
