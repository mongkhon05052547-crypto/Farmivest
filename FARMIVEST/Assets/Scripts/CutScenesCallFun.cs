using UnityEngine;

public class CutScenesCallFun : MonoBehaviour
{
    public void EndStartGameAnimation()
    {
        gameObject.SetActive(false);
        GameManager.Instance.plantDataCtr.AddSeed(SeedTier.Tier1);
        GameManager.Instance.plantDataCtr.AddSeed(SeedTier.Tier1);
        GameManager.Instance.plantDataCtr.AddSeed(SeedTier.Tier1);
    }
}
