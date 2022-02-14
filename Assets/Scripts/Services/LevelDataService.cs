using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class LevelDataService : MonoBehaviour
{
    public static LevelDataService Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Header("For each xth level we will make star calculation.")]
    public int StarConditionPeriod;

    [Header("For each star calculation required average of total stars.")]
    [Range(0, 3)]
    public float StarConditionAverage;

    [Header("We store the levels.")]
    public List<LevelDataItem> Levels;

    public int GetLevelIndex(LevelDataItem levelData) => this.Levels.IndexOf(levelData) + 1;
    public LevelDataItem GetLevelData(int levelIndex) => this.Levels.ElementAt(levelIndex - 1);

    public int GetLevelStar(int levelIndex) => SaveLoadController.Instance.SaveData.GetStarCountOflevel(levelIndex);
    public bool IsLevelCompleted(int levelIndex) => SaveLoadController.Instance.SaveData.GetStarCountOflevel(levelIndex) > 0;
    public bool IsLevelReadyToPlay(int levelIndex)
    {
        // First level always playable.
        if (levelIndex == 1) return true;

        // if previous level not completed just return false.
        if (!IsLevelCompleted(levelIndex - 1)) return false;

        // if not the x or multiple of 5. We will return true.
        if (levelIndex % StarConditionPeriod != 0) return true;

        // For each x level we are going to check is the next level playable?

        // We get the avarage of previous 5.
        float averageOfPrev = GetStarAvgTillTheLevel(levelIndex);

        // if average is enough we tell its playable.
        if (averageOfPrev >= StarConditionAverage) return true;

        // Othewerise we just return false.
        return false;
    }
    public bool IsPreviousLevelPlayed(int levelIndex)
    {
        // if levels are same just return true.
        return GetLevelStar(levelIndex - 1) > 0;
    }
    public float GetStarAvgTillTheLevel(int levelIndex)
    {
        // We get the avarage of previous x.
        return Enumerable.Range(1, levelIndex - 1).Select(x => (float)GetLevelStar(x)).DefaultIfEmpty(0).Average();
    }
    public int GetStarCountTillTheLevel(int levelIndex)
    {
        return SaveLoadController.Instance.SaveData.GetStarCountTillThelevel(levelIndex);
    }
    public int GetStarCountForPlayingTheLevel(int levelIndex)
    {
        // We return the required star count.
        // We have to remove itself to play.
        return Mathf.FloorToInt((levelIndex - 1) * StarConditionAverage);
    }
}
