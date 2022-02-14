using UnityEngine;

[CreateAssetMenu(fileName = "Level-", menuName = "Levels/New Level", order = 1)]
public class LevelDataItem : ScriptableObject
{
    [Header("Level grid")]
    [Multiline(10)]
    public string LevelData;
}
