using UnityEngine;
using UnityEngine.EventSystems;

public class PlotController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] bool havePlant;
    [Header("Plant")]
    [Space]
    [SerializeField] SpriteRenderer plantShow;
    [Header("Stat")]
    [SerializeField] public int tier;
    [SerializeField] public int state;
    [SerializeField] public int currentHp;
    [SerializeField] public int maxHp;
    [SerializeField] public int currentExp;
    [SerializeField] public int maxExp;
    [SerializeField] public int imprintPoint;
    [SerializeField] public ImprintQusets[] imprintQusetsText;

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
