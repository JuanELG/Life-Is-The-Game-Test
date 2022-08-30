using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public static MainSceneUI Instance;

    [Header("Error panel")]
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private Button okButton;

    [Header("Main UI")] 
    [SerializeField] private Button houseDanceButton;
    [SerializeField] private Button macarenaDanceButton;
    [SerializeField] private Button hipHopDanceButton;
    [SerializeField] private Button letsGameButton;

    private void Awake () 
    {
        if(!Instance) 
        {
            Instance = this;
        }
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        errorPanel.transform.localScale = Vector3.zero;

        letsGameButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.CheckSelectedAnimation())
                GameManager.Instance.GoToGameScene();
            else
                ShowErrorPanel();
        });
        
        okButton.onClick.AddListener(HideErrorPanel);
    }

    private void ShowErrorPanel()
    {
        houseDanceButton.interactable = false;
        macarenaDanceButton.interactable = false;
        hipHopDanceButton.interactable = false;
        letsGameButton.interactable = false;

        errorPanel.SetActive(true);
        LeanTween.scale(errorPanel, Vector3.one, 0.5f);
    }

    private void HideErrorPanel()
    {
        StartCoroutine(HidePanelCoroutine());
    }

    private IEnumerator HidePanelCoroutine()
    {
        LeanTween.scale(errorPanel, Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.5f);
        
        houseDanceButton.interactable = true;
        macarenaDanceButton.interactable = true;
        hipHopDanceButton.interactable = true;
        letsGameButton.interactable = true;

        errorPanel.SetActive(false);
    }
}
