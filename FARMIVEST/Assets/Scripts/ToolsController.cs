using UnityEngine;
using UnityEngine.UI;

public class ToolsController : MonoBehaviour
{
    public Button lifeWaterBtn;
    public Button killBugBtn;
    public Button antiPoisonBtn;
    public Button musicBtn;
    public Button antiReverseBtn;
    public Button glassShowBtn;
    public Button hpPotionShowBtn;

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
        GameManager.Instance.currentPlotSelect.ModifyExp(35);
    }

    // 2. ยาฆ่าแมลง
    void KillBug()
    {

    }

    // 3. ยาแก้พิษ
    void AntiPoison()
    {

    }

    // 4. เปิดเพลง (ทำเควส Imprint)
    void PlayMusic()
    {

    }

    // 5. ยาแก้การถดถอย
    void AntiReverse()
    {

    }

    // 6. แว่นขยาย (ส่องสเตตัสพืช)
    void GlassShow()
    {

    }

    // 7. ยาเพิ่มเลือด/ฟื้นฟู HP
    void HpPotion()
    {

    }
}
