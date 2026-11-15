using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlotController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public bool havePlant;
    [Header("Plant")]
    [Space]
    [SerializeField] public SpriteRenderer plantShow;
    [SerializeField] Collider2D plantShowColl;
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
    [SerializeField] public int currentLifeWaterStack;
    [SerializeField] public int countLifeWaterStack;
    [SerializeField] public int maxlifeWaterStack;
    [SerializeField] public float durationPlantReceiveLiftWater;

    [SerializeField] bool onHoldHavest = false;
    [SerializeField] bool plantOnHavestPoint = false;
    [Header("Effect LifeWater")]
    [SerializeField] public Animator useLifeWaterEffect;

    [Header("Effect LifeWater Bar")]
    [SerializeField] GameObject progressBarLifeWaterObj;
    [SerializeField] Image progressBarLifeWater;
    [SerializeField] TMP_Text lifeWaterStackText;

    [Header("Effect Havest Bar")]
    [SerializeField] GameObject progressBarHavestObj;
    [SerializeField] Image progressBarHavest;

    Coroutine progressAddWarterLife;
    Coroutine progressHoldHavest;
    void Awake()
    {
        imprintQusets = new ImprintQusets[3];
        canDoimprintQusets = new bool[3];
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (progressHoldHavest != null)
        {
            StopCoroutine(progressHoldHavest);
            progressHoldHavest = null;
            progressBarHavestObj.SetActive(false);
            plantShowColl.enabled = true;
            if (plantOnHavestPoint)
            {
                GameManager.Instance.panalConfirmHavestObj.SetActive(true);
            }
            else
            {
                GameManager.Instance.currentPlotSelect = null;
                ResetPlantPosition();
            }
            plantOnHavestPoint = false;
        }
        if (onHoldHavest)
        {
            onHoldHavest = false;
            return;
        }
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
        maxlifeWaterStack = _seedTier * 3;
    }

    void UpdateMaxExp()
    {
        maxHp = 100 * tier;
        currentHp = maxHp;
        currentExp = 0;
        maxExp = 100 * tier;
    }

    public void AddLifeWaterToPlant()
    {
        if (countLifeWaterStack == maxlifeWaterStack || state == 4)
        {
            GameManager.Instance.WarningTextShow("พืชไม่ต้องการน้ำชีวิตแล้ว");
            GameManager.Instance.toolsCtr.AddLifeWater(1);
            return;
        }
        GameManager.Instance.currentPlotSelect.useLifeWaterEffect.SetTrigger("Use");
        currentLifeWaterStack++;
        lifeWaterStackText.text = currentLifeWaterStack.ToString();
        countLifeWaterStack++;
        if (progressAddWarterLife == null)
        {
            progressAddWarterLife = StartCoroutine(StartProgressAddLifeWaterToPlant());
        }
    }

    IEnumerator StartProgressAddLifeWaterToPlant()
    {
        progressBarLifeWaterObj.SetActive(true);
        while (currentLifeWaterStack > 0)
        {
            float _time = 0;
            while (_time < durationPlantReceiveLiftWater)
            {
                _time += Time.deltaTime;
                progressBarLifeWater.fillAmount = _time / durationPlantReceiveLiftWater;
                yield return null;
            }
            currentLifeWaterStack--;
            lifeWaterStackText.text = currentLifeWaterStack.ToString();
            ModifyExp(100);
        }
        currentLifeWaterStack = 0;
        lifeWaterStackText.text = "0";
        progressBarLifeWaterObj.SetActive(false);
        progressAddWarterLife = null;
    }

    void ModifyExp(int _input)
    {
        if (!havePlant) return;
        // ToolsController handles amountLifeWater decrement
        currentExp += _input;
        currentExp = Mathf.Clamp(currentExp, 0, maxExp);
        if (GameManager.Instance.currentPlotSelect == this)
        {
            GameManager.Instance.plantDataCtr.UpdateAllPlantDataText();
        }

        if (currentExp == maxExp)
        {
            state++;
            UpdateMaxExp();
            if (tier > 1)
            {
                RndImprintQusets();
            }
            if (state < 4)
            {
                plantShow.sprite = GameManager.Instance.plantLV123List[state - 1];
            }
            else
            {
                plantShow.sprite = plantData.icon;
                canShowStatus = true;
                currentExp = maxExp;
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
            StopAllCoroutines();
            progressBarLifeWaterObj.SetActive(false);
            havePlant = false;
            plantShow.sprite = null;
            currentLifeWaterStack = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!havePlant) return;
        progressHoldHavest = StartCoroutine(CountDelayHoldHavest());
    }

    IEnumerator CountDelayHoldHavest()
    {
        yield return new WaitForSeconds(0.5f);
        onHoldHavest = true;
        GameManager.Instance.currentPlotSelect = this;
        if (state != 4)
        {
            GameManager.Instance.WarningTextShow("พืชยังไม่พร้อมเก็บเกียว");
            yield break;
        }
        progressBarHavestObj.SetActive(true);
        float _time = 0;
        while (_time < 1f)
        {
            _time += Time.deltaTime;
            progressBarHavest.fillAmount = _time / 1f;
            yield return null;
        }
        progressBarHavestObj.SetActive(false);

        while (onHoldHavest)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            // 2. แปลงเป็นพิกัดในเกมผ่านกล้อง Main Camera
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            worldPos.z = 0f; // ล็อคแกน Z ไว้ที่ 0 ให้ตรงกับระนาบ 2D
                             // 3. ย้ายตำแหน่งเมาส์ Sprite
            plantShow.transform.position = worldPos;
            yield return null;
        }
    }

    public void ResetPlantPosition()
    {
        plantShow.transform.localPosition = new Vector3(0, -0.86f, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        plantOnHavestPoint = true;
    }

    public int GetRealPointHavest()
    {
        SeedTier _currentSelect = (SeedTier)System.Enum.GetValues(typeof(SeedTier)).GetValue(tier - 1);
        float _realPoint = (int)_currentSelect + ((int)_currentSelect * (imprintPoint / 100f));
        return (int)_realPoint;
    }
}
