using UnityEngine;

public class CutScenesCallFun : MonoBehaviour
{
    public void EndStartGameAnimation()
    {
        gameObject.SetActive(false);
        GameManager.Instance.plantDataControllerUi.AddSeed(SeedTier.Tier1);
        GameManager.Instance.plantDataControllerUi.AddSeed(SeedTier.Tier1);
        GameManager.Instance.plantDataControllerUi.AddSeed(SeedTier.Tier1);
    }
}
