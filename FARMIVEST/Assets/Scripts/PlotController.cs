using UnityEngine;
using UnityEngine.EventSystems;

public class PlotController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] bool havePlant;
    [Header("Plant")]
    [Space]
    [SerializeField] SpriteRenderer plantShow;
    [Header("Stat")]
    [SerializeField] public PlantData plantData;
    [SerializeField] public int tier;
    [SerializeField] public int state;
    [SerializeField] public int currentHp;
    [SerializeField] public int maxHp;
    [SerializeField] public int currentExp;
    [SerializeField] public int maxExp;
    [SerializeField] public int imprintPoint;
    [SerializeField] public ImprintQusets[] imprintQusets = new ImprintQusets[3];
    public bool canShowStatus = false;
    void Awake()
    {
        imprintQusets = new ImprintQusets[3];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.currentPlotSelect = this;
        GameManager.Instance.ZoomToPlot(gameObject);
        if (havePlant == true)
        {
            GameManager.Instance.plantDataCtr.OpenPlantDataPanal();
        }
        if (havePlant == false)
        {
            GameManager.Instance.plantDataCtr.OpenSeedSelectPanalPanal();
        }
    }

    public void PlantSeed(int _seedTier)
    {
        havePlant = true;
        plantShow.sprite = GameManager.Instance.plantLV123List[0];
        tier = _seedTier;
        state = 1;
        UpdateMaxExp();
        int _rndPlantIndex = Random.Range(0, GameManager.Instance.plantDataCtr.allPlantDatas.Length);
        plantData = GameManager.Instance.plantDataCtr.allPlantDatas[_rndPlantIndex];

        // สุ่มเควส Imprint แต่ละช่องตามจำนวนใน enum ImprintQusets
        for (int i = 0; i < _seedTier; i++)
        {
            imprintQusets[i] = (ImprintQusets)Random.Range(1, System.Enum.GetValues(typeof(ImprintQusets)).Length);
        }
        GameManager.Instance.plantDataCtr.UpdateAllPlantDataText();
    }

    void UpdateMaxExp()
    {
        maxHp = 100 * state * tier;
        currentHp = maxHp;
        currentExp = 0;
        maxExp = 100 * state * tier;

    }

    public void ModifyExp(int _input)
    {
        if (state == 4 && currentExp == maxExp)
        {
            GameManager.Instance.WarningTextShow("MaxLv : Can Havest");
            return;
        }
        currentExp += _input;
        currentExp = Mathf.Clamp(currentExp, 0, maxExp);
        if (currentExp == maxExp)
        {
            state++;
            UpdateMaxExp();
            if (state < 4)
            {
                plantShow.sprite = GameManager.Instance.plantLV123List[state - 1];
            }
            else
            {
                plantShow.sprite = plantData.icon;
                canShowStatus = true;
            }
        }
        GameManager.Instance.plantDataCtr.UpdateAllPlantDataText();
    }
}
