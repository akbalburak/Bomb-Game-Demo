using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlaygroundController : MonoBehaviour
{
    [Header("Playground item to instantiate while building level.")]
    public GameObject GOPlaygroundItem;

    [Header("Place where we will put the playground items.")]
    public Transform TRPlayground;

    [Header("The number of the bombs user may use.")]
    public int LeftBomb;

    /// <summary>
    /// We store the level data.
    /// Row -> Col
    /// </summary>
    private int[][] _levelData;
    private List<Tuple<int, int>> _bombs = new List<Tuple<int, int>>();
    private List<Tuple<int, int>> _bricks = new List<Tuple<int, int>>();

    public void LoadGrid(LevelDataItem levelData)
    {
        // We bind the level data.
        this._levelData = levelData.LevelData.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray())
                .ToArray();

        // We change the constraint count.
        GetComponent<GridLayoutGroup>().constraintCount = this._levelData.Length;

        // This is the count of the bombs to use in the game.
        int initialBombCount = 2;

        // All the rows
        for (int r = 0; r < this._levelData.Length; r++)
        {
            // We loop all the cols.
            for (int c = 0; c < this._levelData[r].Length; c++)
            {
                // We create a playground item.
                GameObject pItem = Instantiate(GOPlaygroundItem, TRPlayground);

                // We change its name.
                pItem.name = CoordinateToString(c, r);

                // When click on the item do the action.
                pItem.GetComponent<Button>().onClick.AddListener(() => OnClickItem(pItem));

                // if its ball we just put the brick.
                if (this._levelData[r][c] == (int)PlaceStates.Wall)
                {
                    // We activate the brick.
                    pItem.transform.Find("Brick").gameObject.SetActive(true);

                    // We increase the bomb count.
                    initialBombCount++;

                    // We add to brick list.
                    this._bricks.Add(new Tuple<int, int>(r, c));
                }
            }
        }

        // We get the count of bricks + 2.
        LeftBomb = initialBombCount;

        // We refresh the bomb count.
        PlaygroundViewController.Instance.RefreshBombCount();

        // Update the dimension change.
        this.OnRectTransformDimensionsChange();
    }
    public void ClearLevel()
    {
        // We close the playground.
        foreach (Transform child in TRPlayground)
            Destroy(child.gameObject);

        // We clear the lists.
        _bombs.Clear();
        _bricks.Clear();

        // We also clear the level data.
        this._levelData = null;
    }

    public void OnClickItem(GameObject sender)
    {
        // if no bomb left.
        if (this.LeftBomb <= 0) return;

        // We make sure the coordinate is valid.
        if (TryGetCoordinate(sender.name, out int col, out int row))
        {
            // We also get value in the place.
            if (TryGetValueInCoordinate(row, col, out PlaceStates place))
            {
                // Only empty place will be placeable.
                if (place == PlaceStates.Empty)
                {
                    // We put the bomb into the place.
                    _levelData[row][col] = (int)PlaceStates.Bomb;

                    // We are going to just activate the bomb in the child.
                    sender.transform.Find("Bomb").gameObject.SetActive(true);

                    // We reduce the bomb count.
                    this.LeftBomb--;

                    // We refresh the bomb count.
                    PlaygroundViewController.Instance.RefreshBombCount();

                    // We add to brick list.
                    this._bombs.Add(new Tuple<int, int>(row, col));

                    // We check the game over state.
                    this.CheckIsGameOver();
                }
            }
        }
    }

    public void CheckIsGameOver()
    {
        // When the game is over.
        bool victory = _bricks.TrueForAll(e => this._bombs.Any(y => AreTwoPointsNear(e.Item1, e.Item2, y.Item1, y.Item2)));

        // if the game won.
        if (victory)
        {
            // We show the victory view.
            PlaygroundViewController.Instance.LevelCompletedView.ShowWinView(this.LeftBomb);

            // Wedont have to go more.
            return;
        }

        // if here we check the bomb count.
        // if the bomb count is 0 then we are going to tell the game is over.
        if (this.LeftBomb <= 0)
        {
            // Player lost the game.
            PlaygroundViewController.Instance.LevelCompletedView.ShowGameOverView();
        }
    }

    public bool AreTwoPointsNear(int row0, int col0, int row1, int col1)
    {
        // Row diff between two points.
        int rdiff = Mathf.Abs(row0 - row1);

        // COl diff between two points.
        int cDiff = Mathf.Abs(col0 - col1);

        // if diffrence between two point is 1 then return true.
        return cDiff + rdiff == 1;
    }
    public bool TryGetValueInCoordinate(int row, int col, out PlaceStates value)
    {
        try
        {
            // We bind the state of the clicked area.
            value = (PlaceStates)_levelData[row][col];

            // We say we converted.
            return true;
        }
        catch (Exception)
        {
            // We just set as empty.
            value = PlaceStates.Empty;

            // We return its because its failed.
            return false;
        }
    }
    public bool TryGetCoordinate(string coordinateString, out int row, out int col)
    {
        try
        {
            // We convert string to integer array.
            int[] coordinate = coordinateString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

            // We bind the coordinates.
            row = coordinate[0];
            col = coordinate[1];

            // We tell its succeed.
            return true;
        }
        catch (Exception)
        {
            // We bind the coordinates.
            col = 0;
            row = 0;

            // We tell failed.
            return false;
        }
    }
    public string CoordinateToString(int row, int col) => $"{row},{col}";

    private void OnRectTransformDimensionsChange()
    {
        /* JUST FOR MAKING ITEMS MORE BIGGER */

        // if data not exists just return.
        if (this._levelData == null) return;

        // Maximum col count in a row.
        int size = this._levelData.FirstOrDefault().Length;

        // Maximum possible is 9 we scale it with the current level size.
        float rate = 9 / (float)size;

        // We update the scale.
        transform.localScale = Vector3.one * rate;
    }
}
