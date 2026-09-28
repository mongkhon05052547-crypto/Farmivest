using UnityEngine;
using UnityEngine.EventSystems;

public class PlotController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] bool havePlant;
    [Header("Plant")]
    [SerializeField] SpriteRenderer plantShow;
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.currentPlotSelect = this;
        GameManager.Instance.ZoomToPlot(gameObject);
        if (havePlant == true)
        {
            GameManager.Instance.plantDataControllerUi.OpenPlantDataPanal();
        }
        if (havePlant == false)
        {
            GameManager.Instance.plantDataControllerUi.OpenSeedSelectPanalPanal();
        }
    }

    public void PlantSeed(int _seedLv)
    {
        havePlant = true;
        plantShow.sprite = GameManager.Instance.seedLV123List[_seedLv];
    }

}
