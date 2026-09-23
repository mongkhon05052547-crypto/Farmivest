using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("CinemachineCamera")]
    [SerializeField] CinemachineCamera vcamMain;
    [SerializeField] CinemachineCamera vcamZoom;
    [Header("UiZone")]
    [SerializeField] GameObject dataInPlot;
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

    void Start()
    {

    }

    public void ReturnToMainCam()
    {
        OffAllCam();
        vcamMain.gameObject.SetActive(true);

        dataInPlot.SetActive(false);
    }

    public void ZoomToPlot(GameObject _targetZoom)
    {
        OffAllCam();
        vcamZoom.gameObject.SetActive(true);
        vcamZoom.Target.TrackingTarget = _targetZoom.transform;

        StartCoroutine(DelayOpenUiDataInPlot());
    }

    IEnumerator DelayOpenUiDataInPlot()
    {
        yield return new WaitForSeconds(0.7f);
        dataInPlot.SetActive(true);
    }

    void OffAllCam()
    {
        vcamMain.gameObject.SetActive(false);
        vcamZoom.gameObject.SetActive(false);
    }
}
