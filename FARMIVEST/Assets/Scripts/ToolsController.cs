using UnityEngine;
using UnityEngine.UI;

public class ToolsController : MonoBehaviour
{
    public Button lifeWaterBtn;
    public int amountLifeWater;
    public Button killBugBtn;
    public int amountKillBug;
    public Button antiPoisonBtn;
    public int amountAntiPoison;
    public Button musicBtn;
    public int amountMusic;
    public Button antiReverseBtn;
    public int amountAntiReverse;
    public Button glassShowBtn;
    public int amountGlassShow;
    public Button hpPotionShowBtn;
    public int amountHpPotion;

    void Awake()
    {
        lifeWaterBtn.onClick.AddListener(AddExp);
        killBugBtn.onClick.AddListener(KillBug);
        antiPoisonBtn.onClick.AddListener(AntiPoison);
        musicBtn.onClick.AddListener(PlayMusic);
        antiReverseBtn.onClick.AddListener(AntiReverse);
        glassShowBtn.onClick.AddListener(GlassShow);
        hpPotionShowBtn.onClick.AddListener(HpPotion);
    }

    // 1. น้ำวิเศษ (เพิ่ม EXP)
    void AddExp()
    {
        if (amountLifeWater == 0)
        {
            GameManager.Instance.WarningTextShow("Not Have LifeWater");
            return;
        }
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.LifeWater);
        GameManager.Instance.currentPlotSelect.ModifyExp(35);
    }

    public void AddLifeWater(int _input)
    {
        amountLifeWater += _input;
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
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.KillBug);
    }

    public void AddKillBug(int _input)
    {
        amountKillBug += _input;
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
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.AntiPoison);
    }

    public void AddAntiPoison(int _input)
    {
        amountAntiPoison += _input;
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
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.PlayMusic);
    }

    public void AddMusic(int _input)
    {
        amountMusic += _input;
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
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.AntiReverse);
    }

    public void AddAntiReverse(int _input)
    {
        amountAntiReverse += _input;
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
    }

    public void AddGlassShow(int _input)
    {
        amountGlassShow += _input;
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
        GameManager.Instance.currentPlotSelect.CheckImprintQusetsCanDo(ImprintQusets.HpPotion);
    }

    public void AddHpPotion(int _input)
    {
        amountHpPotion += _input;
    }
}
