using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum SeedTier
{
    Tier1 = 0, Tier2 = 1, Tier3 = 2,
}

public class PlantDataControllerUi : MonoBehaviour
{
    [Header("Debuff")]
    [SerializeField] GameObject debuffPanal;
    [Header("PlantData")]
    [SerializeField] GameObject plantDataPanal;
    [SerializeField] TMP_Text tierAndStateText;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text expText;
    [SerializeField] TMP_Text descriptionText;
    [SerializeField] TMP_Text imprintPointText;
    [SerializeField] TMP_Text imprintQuest1Text;
    [SerializeField] TMP_Text imprintQuest2Text;
    [SerializeField] TMP_Text imprintQuest3Text;

    [Header("SeedSelect")]
    [SerializeField] GameObject seedSelectPanal;
    [SerializeField] int[] haveSeedLv;
    [SerializeField] Button[] seedListBtn;
    [SerializeField] Image[] seedIconList;
    [SerializeField] GameObject checkPlantSeedPanal;
    [SerializeField] Button yesSelectSeedBtn;
    [SerializeField] Button noSelectSeedBtn;
    [Space]
    [SerializeField] int nowSeedLvSelect;
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
        checkPlantSeedPanal.SetActive(true);
        nowSeedLvSelect = haveSeedLv[_seedIndex];
    }
    void PlantSeedToPlot()
    {
        ResetAllPanal();
        checkPlantSeedPanal.SetActive(false);
        GameManager.Instance.currentPlotSelect.PlantSeed(nowSeedLvSelect);
        RemoveSeed();
        OpenPlantDataPanal();
    }
    #endregion

    #region Add And Remove Seed
    public void AddSeed(SeedTier _seedTier)
    {
        for (int i = 0; i < haveSeedLv.Length; i++)
        {
            if (haveSeedLv[i] == 0)
            {
                haveSeedLv[i] = (int)_seedTier + 1;
                seedListBtn[i].gameObject.SetActive(true);
                seedIconList[i].sprite = GameManager.Instance.seedLV123List[(int)_seedTier];
                return;
            }
        }
    }
    public void RemoveSeed()
    {
        for (int i = 0; i < haveSeedLv.Length; i++)
        {
            if (haveSeedLv[i] == nowSeedLvSelect)
            {
                seedListBtn[i].gameObject.SetActive(false);
                haveSeedLv[i] = 0;
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

    // ฟังก์ชันอัปเดตข้อความทั้งหมด
    public void UpdateAllPlantDataText()
    {
        UpdateTierAndStateTextValue();
        UpdateHpTextValue();
        UpdateExpTextValue();
        UpdateDescriptionTextValue();
        UpdateImprintPointTextValue();
        UpdateImprintQuest1TextValue();
        UpdateImprintQuest2TextValue();
        UpdateImprintQuest3TextValue();
    }

    void UpdateTierAndStateTextValue()
    {
        tierAndStateText.text = $"Tier : {GameManager.Instance.currentPlotSelect.tier} | State : {GameManager.Instance.currentPlotSelect.state}";
    }

    void UpdateHpTextValue()
    {
        hpText.text = "HP : 100 / 100";
    }

    void UpdateExpTextValue()
    {
        expText.text = "Exp : 0 / 15";
    }

    void UpdateDescriptionTextValue()
    {
        descriptionText.text = "คำอธิบายพืช...";
    }

    void UpdateImprintPointTextValue()
    {
        imprintPointText.text = "Imprint : 0 / 100";
    }

    void UpdateImprintQuest1TextValue()
    {
        imprintQuest1Text.text = "เควสที่ 1 : รดน้ำ";
    }

    void UpdateImprintQuest2TextValue()
    {
        imprintQuest2Text.text = "เควสที่ 2 : ใส่ปุ๋ย";
    }

    void UpdateImprintQuest3TextValue()
    {
        imprintQuest3Text.text = "เควสที่ 3 : กำจัดวัชพืช";
    }
    #endregion
}

