using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveScrollView : MonoBehaviour
{
    public GameObject Wave;
    public GameObject AddButton;
    public GameObject ScrollContainer;

    public List<GameObject> waves;

    // Start is called before the first frame update
    void Start()
    {
        this.waves = new List<GameObject>();
        this.InstantiateAddButton();
    }

    public void AddWave()
    {
        var wave = Instantiate(Wave);
        wave.transform.SetParent(ScrollContainer.transform);

        this.waves.Add(wave);

        this.InstantiateAddButton();
    }

    private void InstantiateAddButton()
    {
        Destroy(GameObject.FindGameObjectWithTag("UIWaveAddButton"));

        var button = Instantiate(this.AddButton, Vector3.zero, Quaternion.identity);
        button.transform.SetParent(ScrollContainer.transform);
        var btn = button.GetComponentInChildren<Button>();
        btn.onClick.AddListener(this.AddWave);
    }

    public void Save()
    {
        var errors = new List<string>();

        var waveList = new List<WaveView>();

        foreach (var wv in this.waves)
        {
            waveList.Add(wv.GetComponent<WaveView>());
        }

        errors = this.LogError(
            x => x.Enemy == EnemyEnum.None,
            "Unselected enemies",
            waveList,
            errors);

        errors = this.LogError(
            x => x.InitialCountdown <= 0,
            "Initial CountDown must be greater than zero",
            waveList,
            errors);

        errors = this.LogError(
            x => x.Level <= 0,
            "Enemy level must be greater than zero",
            waveList,
            errors);

        errors = this.LogError(
            x => x.Quantity <= 0,
            "Quantity of enemies must be greater than zero",
            waveList,
            errors);

        if (errors.Any())
        {
            return;
        }

        var uiView = FindObjectOfType<UIView>();
        uiView.Level.Waves = waveList;
        uiView.ShowEditor();
    }

    public void Back()
    {
        this.waves.Clear();

        FindObjectOfType<UIView>().ShowEditor();
    }

    private List<string> LogError(Func<WaveView, bool> condition, string errorMessage, List<WaveView> waveViews, List<string> errorList)
    {
        if (waveViews.Any(condition))
        {
            Debug.LogError(errorMessage);
            errorList.Add(errorMessage);
        }

        return errorList;
    }
}
