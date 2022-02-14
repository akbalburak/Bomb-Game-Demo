using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    public static SaveLoadController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // We get the level information.
        string dataString = PlayerPrefs.GetString(SaveDataKeyword, string.Empty);

        // if no data exists just create an empty.
        if (string.IsNullOrEmpty(dataString))
            this.SaveData = new SaveData();
        else
            this.SaveData = JsonUtility.FromJson<SaveData>(dataString);
    }

    [Header("Keyword to save player progress.")]
    public string SaveDataKeyword = "SAVE_DATA";

    /// <summary>
    /// This is the user data.
    /// To apply any changes on it just call the update method on it.
    /// </summary>
    public SaveData SaveData;

    public void ClearProgress()
    {
        // We clear the progress.
        this.SaveData = new SaveData();

        // We apply the changes.
        this.SaveData.SaveChanges();
    }
}
