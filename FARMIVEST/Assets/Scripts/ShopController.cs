using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("Player Status In Shop")]
    [SerializeField] int point;

    [Header("Seed")]
    [SerializeField] Button seedSctionBtn;
    [SerializeField] GameObject seedSctionObj;
    [SerializeField] Button rndSeedBtn;
    [SerializeField] GameObject showSeedReceiveObj;
    [SerializeField] TMP_Text showSeedReceiveText;
    [Header("Tools")]
    [SerializeField] Button toolsSctionBtn;
    [SerializeField] GameObject toolsSctionObj;
    [SerializeField] Button selectWaterOfLifebtn;
    [SerializeField] Button selectRemoveInsectDebuffsbtn;
    [SerializeField] Button selectRemovePoisonDebuffsbtn;
    [SerializeField] Button selectMusicbtn;
    [SerializeField] Button selectRemoveRegressionDebuffbtn;
    [SerializeField] Button selectWaterOfLifeMagnifyingGlassbtn;
    [SerializeField] Button selectBloodBoostingbtn;

    [Header("Upgrade")]
    [SerializeField] Button upgradeSectionBtn;
    [SerializeField] GameObject upgradeSectionObj;

    void Awake()
    {
        seedSctionBtn.onClick.AddListener(() => ChangeSction(0));
        toolsSctionBtn.onClick.AddListener(() => ChangeSction(1));
        upgradeSectionBtn.onClick.AddListener(() => ChangeSction(2));
        rndSeedBtn.onClick.AddListener(RndSeedOneTime);
    }
    void Start()
    {
        ChangeSction(0);
    }

    void ChangeSction(int _section)
    {
        seedSctionBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-661.38f, -135);
        toolsSctionBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-445, -135);
        upgradeSectionBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-228.7f, -135);
        seedSctionObj.SetActive(false);
        toolsSctionObj.SetActive(false);
        upgradeSectionObj.SetActive(false);
        if (_section == 0)
        {
            seedSctionBtn.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 20f);
            seedSctionObj.SetActive(true);
        }
        if (_section == 1)
        {
            toolsSctionBtn.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 20f);
            toolsSctionObj.SetActive(true);
        }
        if (_section == 2)
        {
            upgradeSectionBtn.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 20f);
            upgradeSectionObj.SetActive(true);
        }
    }

    void RndSeedOneTime()
    {
        if (point < 50)
        {
            GameManager.Instance.WarningTextShow("แต้มไม่พอ");
            return;
        }
        point -= 50;
        bool _canAddSeed = false;
        foreach (int _sh in GameManager.Instance.plantDataCtr.haveSeedTier)
        {
            if (_sh == 0)
            {
                _canAddSeed = true;
                break;
            }
        }
        if (_canAddSeed == false)
        {
            GameManager.Instance.WarningTextShow("เมล็ดเต็มแล้ว");
            return;
        }
        int _rnd = Random.Range(1, 101);
        SeedTier _rndSeed = SeedTier.Tier1;
        if (_rnd > 70 && _rnd < 71)
        {
            _rndSeed = SeedTier.Tier1;
        }
        if (_rnd > 71 && _rnd < 95)
        {
            _rndSeed = SeedTier.Tier2;
        }
        if (_rnd > 95 && _rnd <= 100)
        {
            _rndSeed = SeedTier.Tier3;
        }
        StartCoroutine(ShowSeedReceiveDelay(_rndSeed));
        GameManager.Instance.plantDataCtr.AddSeed(_rndSeed);
    }

    IEnumerator ShowSeedReceiveDelay(SeedTier _seedTier)
    {
        showSeedReceiveObj.SetActive(true);
        showSeedReceiveText.text = "ได้รับเมล็ด : " + _seedTier.ToString();
        yield return new WaitForSeconds(1f);
        showSeedReceiveObj.SetActive(false);

    }
}
