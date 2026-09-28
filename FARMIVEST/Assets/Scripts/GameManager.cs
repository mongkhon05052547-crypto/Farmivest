using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlantDataControllerUi plantDataControllerUi;

    [Header("CinemachineCamera")]
    [SerializeField] CinemachineCamera vcamMain;
    [SerializeField] CinemachineCamera vcamZoom;
    [Header("UiZone")]
    public GameObject mainGamePlayUiPanal;
    [SerializeField] GameObject dataInPlotPanal;
    [Header("PlotAndSeed")]
    public Sprite[] seedLV123List;
    public Sprite[] plantLV123List;
    public PlotController currentPlotSelect;
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
}
