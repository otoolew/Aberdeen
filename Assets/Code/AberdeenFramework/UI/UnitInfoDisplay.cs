using UnityEngine;
using UnityEngine.UI;

public class UnitInfoDisplay : MonoBehaviour {
    /// <summary>
    /// The text component for the name
    /// </summary>
    public Text unitName;

    /// <summary>
    /// The text component for the description
    /// </summary>
    public Text description;

    /// <summary>
    /// The text component for the description
    /// </summary>
    public Text damage;

    /// <summary>
    /// The text component for the health
    /// </summary>
    public Text health;

    /// <summary>
    /// Draws the tower data on to the canvas, if the relevant text components are populated
    /// </summary>
    /// <param name="tower">The tower to gain info from</param>
    /// <param name="levelOfTower">The level of the tower</param>
    public void Show()
    {
        //if (levelOfTower >= tower.levels.Length)
        //{
        //    return;
        //}
        //DisplayText(unitName, tower.towerName);
        //DisplayText(description, towerLevel.description);
        //DisplayText(damage, towerLevel.GetTowerDps().ToString("f2"));
        //DisplayText(health, string.Format("{0}/{1}", tower.configuration.currentHealth, towerLevel.maxHealth));
        //DisplayText(level, (levelOfTower + 1).ToString());
        //DisplayText(dimensions, string.Format("{0}, {1}", tower.dimensions.x, tower.dimensions.y));
        //if (levelOfTower + 1 < tower.levels.Length)
        //{
        //    DisplayText(upgradeCost, tower.levels[levelOfTower + 1].cost.ToString());
        //}

        //int sellValue = tower.GetSellLevel(levelOfTower);
        //DisplayText(sellPrice, sellValue.ToString());
    }

    /// <summary>
    /// Draws the text if the text component is populated
    /// </summary>
    /// <param name="textBox"></param>
    /// <param name="text"></param>
    static void DisplayText(Text textBox, string text)
    {
        if (textBox != null)
        {
            textBox.text = text;
        }
    }
}
