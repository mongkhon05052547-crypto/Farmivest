using UnityEngine;

public class CutScenesCallFun : MonoBehaviour
{
    public void EndStartGameAnimation()
    {
        gameObject.SetActive(false);
        GameManager.Instance.StarterPack();
    }
}
