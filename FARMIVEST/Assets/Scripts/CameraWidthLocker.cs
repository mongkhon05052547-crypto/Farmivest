using Unity.Cinemachine;
using UnityEngine;

public class CameraWidthLocker : MonoBehaviour
{
    // เปลี่ยนจาก CinemachineVirtualCamera เป็น CinemachineCamera
    [SerializeField] CinemachineCamera vcam; 
    public float defaultCameraSize = 5f;
    private float targetAspect = 16f / 9f;

    void Start()
    {
        AdjustCameraSize();
    }

    public void AdjustCameraSize()
    {
        float currentAspect = (float)Screen.width / (float)Screen.height;
        
        // ดึงการตั้งค่า Lens ปัจจุบันออกมาก่อน (เพราะ Lens เป็น Struct)
        LensSettings currentLens = vcam.Lens;

        if (currentAspect < targetAspect)
        {
            float sizeMultiplier = targetAspect / currentAspect;
            currentLens.OrthographicSize = defaultCameraSize * sizeMultiplier;
        }
        else
        {
            currentLens.OrthographicSize = defaultCameraSize;
        }

        // นำค่า Lens ที่แก้แล้ว ใส่กลับคืนไปที่กล้อง
        vcam.Lens = currentLens;
    }
}