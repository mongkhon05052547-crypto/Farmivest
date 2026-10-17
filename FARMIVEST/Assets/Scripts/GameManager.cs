using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public enum ImprintQusets
{
    None, LifeWater, KillBug, AntiPoison, PlayMusic, AntiReverse, HpPotion
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlantDataController plantDataCtr;
    public ToolsController toolsCtr;
    public HouseController houseCtr;

    [Header("CinemachineCamera")]
    [SerializeField] CinemachineCamera vcamMain;
    [SerializeField] CinemachineCamera vcamZoom;
    [Header("UiZone")]
    public GameObject mainGamePlayUiPanal;
    [SerializeField] GameObject dataInPlotPanal;
    [Header("PlotAndSeed")]
    public Sprite[] seedLV123List;
    public Sprite[] plantLV123List;
    public PlotController[] allPlot;
    [HideInInspector] public PlotController currentPlotSelect;

    [Header("Warning")]
    Coroutine coWaring;
    [SerializeField] GameObject warningObj;
    [SerializeField] TMP_Text warningText;

    public bool canPlant;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canPlant = true;
    }

    [ContextMenu("Get All Plots")]
    void GetAllPlots()
    {
        allPlot = FindObjectsByType<PlotController>(FindObjectsSortMode.None);
    }

    public void ReturnToMainCam()
    {
        OffAllCam();
        vcamMain.gameObject.SetActive(true);
        currentPlotSelect = null;
        dataInPlotPanal.SetActive(false);
        mainGamePlayUiPanal.SetActive(true);
    }

    public void ZoomToPlot(GameObject _targetZoom)
    {
        OffAllCam();
        vcamZoom.gameObject.SetActive(true);
        vcamZoom.Target.TrackingTarget = _targetZoom.transform;
        mainGamePlayUiPanal.SetActive(false);

        StartCoroutine(DelayOpenUiDataInPlot());
    }

    IEnumerator DelayOpenUiDataInPlot()
    {
        yield return new WaitForSeconds(0.7f);
        dataInPlotPanal.SetActive(true);
    }

    void OffAllCam()
    {
        vcamMain.gameObject.SetActive(false);
        vcamZoom.gameObject.SetActive(false);
    }

    public void WarningTextShow(string _input)
    {
        if (coWaring != null)
        {
            StopCoroutine(coWaring);
        }
        coWaring = StartCoroutine(CallWarningTextShow(_input));
    }
    public IEnumerator CallWarningTextShow(string _input)
    {
        warningObj.SetActive(true);
        warningText.text = _input;
        yield return new WaitForSeconds(3f);
        warningObj.SetActive(false);
        coWaring = null;
    }
}
