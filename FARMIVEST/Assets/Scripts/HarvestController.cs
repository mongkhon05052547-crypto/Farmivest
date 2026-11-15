using UnityEngine;

public class HarvestController : MonoBehaviour
{
    [SerializeField] int currentPointHarvest;
    public void AddPointHavest(int _point)
    {
        currentPointHarvest += _point;
    }
}
