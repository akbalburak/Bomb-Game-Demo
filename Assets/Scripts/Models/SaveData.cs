using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SaveData
{
    /// <summary>
    /// Level progress.
    /// </summary>
    public List<SaveDataLevel> LevelProgress;

    public SaveData()
    {
        this.LevelProgress = new List<SaveDataLevel>();
    }

    /// <summary>
    /// Apply the changes into the playerprefs.
    /// </summary>
    public void SaveChanges()
    {
        // We set the latest changes to the pref.
        PlayerPrefs.SetString(SaveLoadController.Instance.SaveDataKeyword, JsonUtility.ToJson(this));
    }

    /// <summary>
    /// Returns the star count for the given level.
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    public int GetStarCountOflevel(int levelIndex)
    {
        // We are looking for the level record.
        SaveDataLevel levelData = this.LevelProgress.Find(x => x.LevelIndex == levelIndex);

        // if level not exists we just return 0 star but if exist we return the star count.
        return levelData == null ? 0 : levelData.Star;
    }

    /// <summary>
    /// Returns the star count for the given level.
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    public int GetStarCountTillThelevel(int levelIndex)
    {
        // We are looking for the level record.
        return this.LevelProgress.Where(x => x.LevelIndex <= levelIndex).Select(x => x.Star).Sum();
    }

    /// <summary>
    /// Update any level progress for given level index.
    /// NOTE : if new star cound is less then old one do nothing.
    /// </summary>
    /// <param name="levelIndex">Index of the level.</param>
    /// <param name="star">Colleced star count.</param>
    public void UpdateLevelProgress(int levelIndex, int star)
    {
        // We wont user to save invalid score.
        if (star <= 0 || star > 3) return;

        // We find the exists progress.
        SaveDataLevel existsProg = this.LevelProgress.Find(x => x.LevelIndex == levelIndex);

        // if progress not exists we just create one.
        if (existsProg == null)
        {
            // We create a new one.
            existsProg = new SaveDataLevel(levelIndex, star);

            // Then we add to progress list.
            this.LevelProgress.Add(existsProg);

            // Lastly we apply changes.
            this.SaveChanges();
        }
        else
        {
            // We make sure we collected enough star.
            if (star <= existsProg.Star) return;

            // if collected we just update the progress.
            existsProg.Star = star;

            // And apply changes.
            this.SaveChanges();
        }
    }

    [Serializable]
    public class SaveDataLevel
    {
        [Header("Index of the level in data service.")]
        public int LevelIndex;

        [Header("Collected star count for the level.")]
        public int Star;

        public SaveDataLevel(int levelIndex, int star)
        {
            this.LevelIndex = levelIndex;
            this.Star = star;
        }
    }
}
