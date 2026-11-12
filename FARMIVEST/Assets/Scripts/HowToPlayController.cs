using System;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayController : MonoBehaviour
{
    [SerializeField] Button openHowToPlayBtn;
    [SerializeField] Button closeHowToPlayBtn;
    [SerializeField] GameObject howToPlayPanal;
    [SerializeField] Button killEnemyBtn;
    [SerializeField] GameObject killEnemyPanal;
    [SerializeField] Button createWateLiftBtn;
    [SerializeField] GameObject createWateLiftPanal;
    [SerializeField] Button havestPlantBtn;
    [SerializeField] GameObject havestPlantPanal;

    bool inGuide;

    void Start()
    {
        openHowToPlayBtn.onClick.AddListener(OpenPanal);
        closeHowToPlayBtn.onClick.AddListener(ClosePanal);
        killEnemyBtn.onClick.AddListener(OpenKillEnemyPanal);
        createWateLiftBtn.onClick.AddListener(OpenCreateWateLiftPanal);
        havestPlantBtn.onClick.AddListener(OpenHavestPlantPanal);
    }

    private void OpenHavestPlantPanal()
    {
        inGuide = true;
        OffAllPanal();
        havestPlantPanal.SetActive(true);
    }

    private void OpenCreateWateLiftPanal()
    {
        inGuide = true;
        OffAllPanal();
        createWateLiftPanal.SetActive(true);
    }

    private void OpenKillEnemyPanal()
    {
        inGuide = true;
        OffAllPanal();
        killEnemyPanal.SetActive(true);
    }

    private void ClosePanal()
    {
        OffAllPanal();
        if (inGuide == true)
        {
            inGuide = false;
            OffAllPanal();
        }
        else
        {
            howToPlayPanal.SetActive(false);
            OffAllPanal();
        }

    }

    private void OpenPanal()
    {
        howToPlayPanal.SetActive(true);
    }

    void OffAllPanal()
    {
        killEnemyPanal.SetActive(false);
        createWateLiftPanal.SetActive(false);
        havestPlantPanal.SetActive(false);
    }
}
