using System;
using UnityEngine;

public class CanvasResizeListener : UnityEngine.EventSystems.UIBehaviour
{
    [SerializeField] CameraWidthLocker mainCamera;
    protected override void OnRectTransformDimensionsChange()
    {
        mainCamera.AdjustCameraSize();
    }
}
