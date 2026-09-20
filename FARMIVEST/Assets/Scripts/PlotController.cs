using UnityEngine;
using UnityEngine.EventSystems;

public class PlotController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAA");
        GameManager.Instance.ZoomToPlot(gameObject);
    }
}
