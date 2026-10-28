using UnityEngine;
using UnityEngine.EventSystems;

public class PlotController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public bool havePlant;
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
    [SerializeField] public bool[] canDoimprintQusets = new bool[3];
    public bool canShowStatus = false;
    [Header("Effect")]
    [SerializeField] public Animator useLifeWaterEffect;
    void Awake()
    {
        imprintQusets = new ImprintQusets[3];
        canDoimprintQusets = new bool[3];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.canPlant) return;
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
        if (GameManager.Instance.currentPlotSelect == this)
        {
            GameManager.Instance.plantDataCtr.UpdateAllPlantDataText();
        }
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
        if (!havePlant) return;
        if (state == 4 && currentExp == maxExp)
        {
            if (GameManager.Instance.currentPlotSelect == this)
            {
                GameManager.Instance.WarningTextShow("MaxLv : Can Havest");
            }
            return;
        }
        // ToolsController handles amountLifeWater decrement
        currentExp += _input;
        currentExp = Mathf.Clamp(currentExp, 0, maxExp);
        if (GameManager.Instance.currentPlotSelect == this)
        {
            GameManager.Instance.plantDataCtr.UpdateAllPlantDataText();
        }

        if (currentExp == maxExp && state != 4)
        {
            state++;
            UpdateMaxExp();
            RndImprintQusets();
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

        if (GameManager.Instance.currentPlotSelect == this)
        {
            GameManager.Instance.plantDataCtr.UpdateAllPlantDataText();
        }
    }

    void RndImprintQusets()
    {
        for (int a = 0; a < canDoimprintQusets.Length; a++)
        {
            canDoimprintQusets[a] = false;
        }
        GameManager.Instance.plantDataCtr.lineOffImprintQuest1.SetActive(false);
        GameManager.Instance.plantDataCtr.lineOffImprintQuest2.SetActive(false);
        GameManager.Instance.plantDataCtr.lineOffImprintQuest3.SetActive(false);
        // สุ่มเควส Imprint แต่ละช่องตามจำนวนใน enum ImprintQusets
        for (int i = 0; i < state - 1; i++)
        {
            imprintQusets[i] = (ImprintQusets)Random.Range(1, System.Enum.GetValues(typeof(ImprintQusets)).Length);
            canDoimprintQusets[i] = true;
        }
    }

    public void CheckImprintQusetsCanDo(ImprintQusets _input)
    {
        for (int i = 0; i < imprintQusets.Length; i++)
        {
            if (imprintQusets[i] == _input && canDoimprintQusets[i] == true)
            {
                canDoimprintQusets[i] = false;
                GameManager.Instance.plantDataCtr.lineOffImprintQuest1.SetActive(!canDoimprintQusets[0]);
                GameManager.Instance.plantDataCtr.lineOffImprintQuest2.SetActive(!canDoimprintQusets[1]);
                GameManager.Instance.plantDataCtr.lineOffImprintQuest3.SetActive(!canDoimprintQusets[2]);
                imprintPoint += 10;
                imprintPoint = Mathf.Clamp(imprintPoint, 0, 100);
                if (GameManager.Instance.currentPlotSelect == this)
        {
            GameManager.Instance.plantDataCtr.UpdateAllPlantDataText();
        }
                return;
            }
        }
    }


    public void TakeDamage(int _damage)
    {
        currentHp -= _damage;
        if (currentHp <= 0)
        {
            havePlant = false;
            plantShow.sprite = null;
        }
    }
}
