using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveView : MonoBehaviour
{
    public Dropdown EnemyDropdown;

    public EnemyEnum Enemy { get; private set; }
    public int Level { get; private set; }
    public int Quantity { get; private set; }
    public float InitialCountdown { get; private set; }

    private void Start()
    {
        InitializeEnemyDropdown();
        this.Level = 0;
        this.Quantity = 0;
        this.InitialCountdown = 0;
    }

    public void SetLevel(string text)
    {
        this.Level = int.Parse(text);
    }

    public void SetQuantity(string text)
    {
        this.Quantity = int.Parse(text);
    }

    public void SetInitialCountDown(string text)
    {
        this.InitialCountdown = float.Parse(text);
    }

    public void SetEnemy(int enemy)
    {
        this.Enemy = (EnemyEnum)enemy;
    }

    private void InitializeEnemyDropdown()
    {
        this.EnemyDropdown.ClearOptions();


        var optionData = new List<Dropdown.OptionData>();

        optionData.Add(new Dropdown.OptionData
        {
            text = "-- Character"
        });

        var enumList = Enum.GetNames(typeof(EnemyEnum))
        .Where(x => x != "None");
        //.ToList()

        foreach (var enemy in enumList)
        {
            optionData.Add(new Dropdown.OptionData
            {
                text = enemy.ToString()
            });
        }

        this.EnemyDropdown.AddOptions(optionData);
    }
}
