using UnityEngine;
using UnityEngine.EventSystems;

public class PlotController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.ZoomToPlot(gameObject);
    }
}
