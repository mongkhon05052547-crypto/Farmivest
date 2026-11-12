using UnityEngine;

public class StarterTestGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.StarterPack();
    }
}
