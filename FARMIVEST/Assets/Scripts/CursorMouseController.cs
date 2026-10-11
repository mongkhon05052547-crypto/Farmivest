using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMouseController : MonoBehaviour
{
    [SerializeField] GameObject cursor;

    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        // 2. แปลงเป็นพิกัดในเกมผ่านกล้อง Main Camera
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        worldPos.z = 0f; // ล็อคแกน Z ไว้ที่ 0 ให้ตรงกับระนาบ 2D
                         // 3. ย้ายตำแหน่งเมาส์ Sprite
        cursor.transform.position = worldPos;
    }
}
