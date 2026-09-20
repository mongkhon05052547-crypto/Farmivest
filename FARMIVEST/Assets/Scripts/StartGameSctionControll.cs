using System;
using UnityEngine;
using UnityEngine.UI;

public class StartGameSctionControll : MonoBehaviour
{
    [Header("StartGameLobby")]
    [SerializeField] GameObject manuStartGamePanal;
    [SerializeField] Button playGameBtn;
    [SerializeField] Button continueGameBtn;
    [SerializeField] Button optionsBtn;
    [SerializeField] Button exitGameBtn;
    [Header("CutScenes")]
    [SerializeField] GameObject cutscenesPanal;
    [SerializeField] Button skipCutscenesBtn;

    void Start()
    {
        playGameBtn.onClick.AddListener(OnClickPlayGame);
        continueGameBtn.onClick.AddListener(OnClickContinueGame);
        optionsBtn.onClick.AddListener(OnClickOptionsGame);
        exitGameBtn.onClick.AddListener(OnClickExitGame);
        skipCutscenesBtn.onClick.AddListener(OnClickskipCutscenes);
    }
    private void OnClickPlayGame()
    {
        OffAllPanal();
        cutscenesPanal.SetActive(true);
    }

    private void OnClickContinueGame()
    {

    }

    private void OnClickOptionsGame()
    {

    }

    private void OnClickExitGame()
    {

    }

    private void OnClickskipCutscenes()
    {
        OffAllPanal();
        
    }

    void OffAllPanal()
    {
        manuStartGamePanal.SetActive(false);
        cutscenesPanal.SetActive(false);
    }
}
