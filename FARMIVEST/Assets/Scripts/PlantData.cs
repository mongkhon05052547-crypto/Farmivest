using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Scriptable Objects/PlantData")]
public class PlantData : ScriptableObject
{
    public string namePlant;
    public Sprite icon;
    [TextArea(0, 10)]
    public string Description;
}
