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

    public void PlantSeed(int _seedIndex)
    {
        checkPlantSeedPanal.SetActive(true);
        nowSeedLvSelect = haveSeedLv[_seedIndex];
    }

    void ResetAllPanal()
    {
        plantDataPanal.SetActive(false);
        seedSelectPanal.SetActive(false);
    }

    public void OpenPlantDataPanal()
    {
        ResetAllPanal();
        plantDataPanal.SetActive(true);
    }

    public void OpenSeedSelectPanalPanal()
    {
        ResetAllPanal();
        seedSelectPanal.SetActive(true);
    }

    void PlantSeedToPlot()
    {
        ResetAllPanal();
        checkPlantSeedPanal.SetActive(false);
        GameManager.Instance.currentPlotSelect.PlantSeed(nowSeedLvSelect);
        RemoveSeed();
        OpenPlantDataPanal();
    }

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
}

