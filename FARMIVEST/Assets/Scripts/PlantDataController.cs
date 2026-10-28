using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SeedTier
{
    Tier1 = 0, Tier2 = 1, Tier3 = 2,
}

public class PlantDataController : MonoBehaviour
{
    [Header("Debuff")]
    [SerializeField] GameObject debuffPanal;
    [Header("PlantData")]
    [SerializeField] GameObject plantDataPanal;
    [SerializeField] TMP_Text namePlantText;
    [SerializeField] TMP_Text tierAndStateText;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text expText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text imprintPointText;
    [SerializeField] TMP_Text imprintQuest1Text;
    [SerializeField] public GameObject lineOffImprintQuest1;
    [SerializeField] TMP_Text imprintQuest2Text;
    [SerializeField] public GameObject lineOffImprintQuest2;
    [SerializeField] TMP_Text imprintQuest3Text;
    [SerializeField] public GameObject lineOffImprintQuest3;

    [Header("SeedSelect")]
    [SerializeField] GameObject seedSelectPanal;
    [SerializeField] public int[] haveSeedTier = new int[15];
    [SerializeField] Button[] seedListBtn = new Button[15];
    [SerializeField] Image[] seedIconList = new Image[15];
    [SerializeField] GameObject checkPlantSeedPanal;
    [SerializeField] TMP_Text checkPlantSeedText;
    [SerializeField] Button yesSelectSeedBtn;
    [SerializeField] Button noSelectSeedBtn;
    [Space]
    [SerializeField] int nowSeedTierSelect;
    [Header("AllPlantData")]
    [SerializeField] public PlantData[] allPlantDatas;

#if UNITY_EDITOR
    [ContextMenu("Get All Plant Datas")]
    void GetAllPlantDatas()
    {
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:PlantData", new[] { "Assets/DataPlant" });
        allPlantDatas = new PlantData[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
            allPlantDatas[i] = UnityEditor.AssetDatabase.LoadAssetAtPath<PlantData>(path);
        }
    }
#endif

    void Awake()
    {
        for (int i = 0; i < seedListBtn.Length; i++)
        {
            int a = i;
            seedListBtn[i].onClick.AddListener(() => PlantSeed(a));
        }
        yesSelectSeedBtn.onClick.AddListener(PlantSeedToPlot);
        noSelectSeedBtn.onClick.AddListener(() => { checkPlantSeedPanal.SetActive(false); });
    }

    void ResetAllPanal()
    {
        plantDataPanal.SetActive(false);
        seedSelectPanal.SetActive(false);
    }

    #region PlantSeed
    public void OpenSeedSelectPanalPanal()
    {
        ResetAllPanal();
        seedSelectPanal.SetActive(true);
    }

    public void PlantSeed(int _seedIndex)
    {
        int _pointSeed = 0;
        switch (haveSeedTier[_seedIndex])
        {
            case 1:
                _pointSeed = 100;
                break;
            case 2:
                _pointSeed = 250;
                break;
            case 3:
                _pointSeed = 500;
                break;
        }
        checkPlantSeedText.text = "Sure Plant Seed\n Tire : " + haveSeedTier[_seedIndex] + "\npoint " + _pointSeed;
        checkPlantSeedPanal.SetActive(true);
        nowSeedTierSelect = haveSeedTier[_seedIndex];
    }

    void PlantSeedToPlot()
    {
        ResetAllPanal();
        checkPlantSeedPanal.SetActive(false);
        GameManager.Instance.currentPlotSelect.PlantSeed(nowSeedTierSelect);
        RemoveSeed();
        OpenPlantDataPanal();
    }
    #endregion

    #region Add And Remove Seed
    public void AddSeed(SeedTier _seedTier)
    {
        for (int i = 0; i < haveSeedTier.Length; i++)
        {
            if (haveSeedTier[i] == 0)
            {
                haveSeedTier[i] = (int)_seedTier + 1;
                seedListBtn[i].gameObject.SetActive(true);
                seedIconList[i].sprite = GameManager.Instance.seedLV123List[(int)_seedTier];
                return;
            }
        }
    }

    public void RemoveSeed()
    {
        for (int i = 0; i < haveSeedTier.Length; i++)
        {
            if (haveSeedTier[i] == nowSeedTierSelect)
            {
                seedListBtn[i].gameObject.SetActive(false);
                haveSeedTier[i] = 0;
                seedIconList[i].sprite = GameManager.Instance.seedLV123List[0];
                return;
            }
        }
    }
    #endregion

    #region DataPlant in Plot
    public void OpenPlantDataPanal()
    {
        ResetAllPanal();
        plantDataPanal.SetActive(true);
        UpdateAllPlantDataText();
    }

    // ฟังก์ชันสั่งอัปเดตข้อมูลทั้งหมด
    public void UpdateAllPlantDataText()
    {
        if (GameManager.Instance.currentPlotSelect == null) return;

        UpdateNamePlantTextValue();
        UpdateTierAndStateTextValue();
        UpdateHpTextValue();
        UpdateExpTextValue();
        UpdateDescriptionTextValue();
        UpdateImprintPointTextValue();
        UpdateImprintQuest1TextValue();
        UpdateImprintQuest2TextValue();
        UpdateImprintQuest3TextValue();
    }

    void UpdateNamePlantTextValue()
    {
        if (GameManager.Instance.currentPlotSelect == null) return;
        if (!GameManager.Instance.currentPlotSelect.canShowStatus || GameManager.Instance.currentPlotSelect.plantData == null)
        {
            namePlantText.text = "???";
            return;
        }
        namePlantText.text = $"{GameManager.Instance.currentPlotSelect.plantData.namePlant}";
    }
    void UpdateTierAndStateTextValue()
    {
        tierAndStateText.text = $"Tier : {GameManager.Instance.currentPlotSelect.tier} | State : {GameManager.Instance.currentPlotSelect.state}";
    }

    void UpdateHpTextValue()
    {
        hpText.text = $"HP : {GameManager.Instance.currentPlotSelect.currentHp} / {GameManager.Instance.currentPlotSelect.maxHp}";
    }

    void UpdateExpTextValue()
    {
        expText.text = $"Exp : {GameManager.Instance.currentPlotSelect.currentExp} / {GameManager.Instance.currentPlotSelect.maxExp}";
    }

    void UpdateDescriptionTextValue()
    {
        if (GameManager.Instance.currentPlotSelect == null) return;
        if (!GameManager.Instance.currentPlotSelect.canShowStatus || GameManager.Instance.currentPlotSelect.plantData == null)
        {
            descriptionText.text = "???";
            return;
        }
        descriptionText.text = $"Plant {GameManager.Instance.currentPlotSelect.plantData.namePlant} YumYum...";
    }

    void UpdateImprintPointTextValue()
    {
        imprintPointText.text = $"Imprint : {GameManager.Instance.currentPlotSelect.imprintPoint} / 100";
    }

    void UpdateImprintQuest1TextValue()
    {
        if (GameManager.Instance.currentPlotSelect.imprintQusets[0] == ImprintQusets.None)
        {
            imprintQuest1Text.gameObject.SetActive(false);
            return;
        }
        imprintQuest1Text.gameObject.SetActive(true);
        imprintQuest1Text.text = $"{GameManager.Instance.currentPlotSelect.imprintQusets[0]} + 10p";
    }

    void UpdateImprintQuest2TextValue()
    {
        if (GameManager.Instance.currentPlotSelect.imprintQusets[1] == ImprintQusets.None)
        {
            imprintQuest2Text.gameObject.SetActive(false);
            return;
        }
        imprintQuest2Text.gameObject.SetActive(true);
        imprintQuest2Text.text = $"{GameManager.Instance.currentPlotSelect.imprintQusets[1]}  + 10p";
    }

    void UpdateImprintQuest3TextValue()
    {
        if (GameManager.Instance.currentPlotSelect.imprintQusets[2] == ImprintQusets.None)
        {
            imprintQuest3Text.gameObject.SetActive(false);
            return;
        }
        imprintQuest3Text.gameObject.SetActive(true);
        imprintQuest3Text.text = $"{GameManager.Instance.currentPlotSelect.imprintQusets[2]}  + 10p";
    }
    #endregion
}
