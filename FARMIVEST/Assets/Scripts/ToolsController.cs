using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolsController : MonoBehaviour
{
    [Header("1. Life Water")]
    public Button lifeWaterBtn;
    public int amountLifeWater;
    public TMP_Text amountLifeWaterText;

    [Header("2. Kill Bug")]
    public Button killBugBtn;
    public int amountKillBug;
    public TMP_Text amountKillBugText;

    [Header("3. Anti Poison")]
    public Button antiPoisonBtn;
    public int amountAntiPoison;
    public TMP_Text amountAntiPoisonText;

    [Header("4. Music")]
    public Button musicBtn;
    public int amountMusic;
    public TMP_Text amountMusicText;

    [Header("5. Anti Reverse / Regression")]
    public Button antiReverseBtn;
    public int amountAntiReverse;
    public TMP_Text amountAntiReverseText;

    [Header("6. Magnifying Glass")]
    public Button glassShowBtn;
    public int amountGlassShow;
    public TMP_Text amountGlassShowText;

    [Header("7. HP Potion")]
    public Button hpPotionShowBtn;
    public int amountHpPotion;
    public TMP_Text amountHpPotionText;

    void Awake()
    {
        lifeWaterBtn.onClick.AddListener(UseLifeWaterAddExp);
        killBugBtn.onClick.AddListener(KillBug);
        antiPoisonBtn.onClick.AddListener(AntiPoison);
        musicBtn.onClick.AddListener(PlayMusic);
        antiReverseBtn.onClick.AddListener(AntiReverse);
        glassShowBtn.onClick.AddListener(GlassShow);
        hpPotionShowBtn.onClick.AddListener(HpPotion);
    }

    void Start()
    {
        UpdateAllToolsText();
    }

    public void UpdateAllToolsText()
    {
        UpdateLifeWaterText();
        UpdateKillBugText();
        UpdateAntiPoisonText();
        UpdateMusicText();
        UpdateAntiReverseText();
        UpdateGlassShowText();
        UpdateHpPotionText();
    }

    public void UpdateLifeWaterText()
    {
        if (amountLifeWaterText != null) amountLifeWaterText.text = amountLifeWater.ToString();
    }

    public void UpdateKillBugText()
    {
        if (amountKillBugText != null) amountKillBugText.text = amountKillBug.ToString();
    }

    public void UpdateAntiPoisonText()
    {
        if (amountAntiPoisonText != null) amountAntiPoisonText.text = amountAntiPoison.ToString();
    }

    public void UpdateMusicText()
    {
        if (amountMusicText != null) amountMusicText.text = amountMusic.ToString();
    }

    public void UpdateAntiReverseText()
    {
        if (amountAntiReverseText != null) amountAntiReverseText.text = amountAntiReverse.ToString();
    }

    public void UpdateGlassShowText()
    {
        if (amountGlassShowText != null) amountGlassShowText.text = amountGlassShow.ToString();
    }

    public void UpdateHpPotionText()
    {
        if (amountHpPotionText != null) amountHpPotionText.text = amountHpPotion.ToString();
    }

    // 1. น้ำวิเศษ (เพิ่ม EXP)
    void UseLifeWaterAddExp()
    {
        if (amountLifeWater == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have LifeWater");
            return;
        }
        amountLifeWater--;
        UpdateLifeWaterText();
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.LifeWater);
        GameManager.Instance.currentPlotSelect.AddLifeWaterToPlant();
        GameManager.Instance.currentPlotSelect.useLifeWaterEffect.SetTrigger("Use");
    }

    public void AddLifeWater(int _input)
    {
        amountLifeWater += _input;
        UpdateLifeWaterText();
    }

    // 2. ยาฆ่าแมลง
    void KillBug()
    {
        if (amountKillBug == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have KillBug");
            return;
        }
        amountKillBug--;
        UpdateKillBugText();
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.KillBug);
    }

    public void AddKillBug(int _input)
    {
        amountKillBug += _input;
        UpdateKillBugText();
    }

    // 3. ยาแก้พิษ
    void AntiPoison()
    {
        if (amountAntiPoison == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have AntiPoison");
            return;
        }
        amountAntiPoison--;
        UpdateAntiPoisonText();
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.AntiPoison);
    }

    public void AddAntiPoison(int _input)
    {
        amountAntiPoison += _input;
        UpdateAntiPoisonText();
    }

    // 4. เปิดเพลง (ทำเควส Imprint)
    void PlayMusic()
    {
        if (amountMusic == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have Music");
            return;
        }
        amountMusic--;
        UpdateMusicText();
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.PlayMusic);
    }

    public void AddMusic(int _input)
    {
        amountMusic += _input;
        UpdateMusicText();
    }

    // 5. ยาแก้การถดถอย
    void AntiReverse()
    {
        if (amountAntiReverse == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have AntiReverse");
            return;
        }
        amountAntiReverse--;
        UpdateAntiReverseText();
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.AntiReverse);
    }

    public void AddAntiReverse(int _input)
    {
        amountAntiReverse += _input;
        UpdateAntiReverseText();
    }

    // 6. แว่นขยาย (ส่องสเตตัสพืช)
    void GlassShow()
    {
        if (amountGlassShow == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have GlassShow");
            return;
        }
        amountGlassShow--;
        UpdateGlassShowText();
    }

    public void AddGlassShow(int _input)
    {
        amountGlassShow += _input;
        UpdateGlassShowText();
    }

    // 7. ยาเพิ่มเลือด/ฟื้นฟู HP
    void HpPotion()
    {
        if (amountHpPotion == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have HpPotion");
            return;
        }
        amountHpPotion--;
        UpdateHpPotionText();
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.HpPotion);
    }

    public void AddHpPotion(int _input)
    {
        amountHpPotion += _input;
        UpdateHpPotionText();
    }
}
