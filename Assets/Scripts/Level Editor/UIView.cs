﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    public GameObject Main;
    public GameObject Editor;
    public GameObject WaveEditor;
    public GameObject SceneEditor;

    public GameObject Background;

    public Text Title;

    public Level Level = new Level();

    private void Awake()
    {
        this.DisableViewsExcept(this.Main, "Creator Mode");
    }

    public void ShowEditor()
    {
        this.DisableViewsExcept(this.Editor, "Level Editor");
    }

    public void EditScene()
    {
        this.DisableViewsExcept(this.SceneEditor, disableUI: true);
    }

    public void WaveManager()
    {
        this.DisableViewsExcept(this.WaveEditor, "Configure Waves");
    }

    public void SetCash(string text)
    {
        if (text == string.Empty)
        {
            text = "0";
        }

        this.Level.InitialCash = int.Parse(text);
    }

    public void SetInitialCountdown(string text)
    {
        if (text == string.Empty)
        {
            text = "0";
        }

        this.Level.InitialCountDown = int.Parse(text);
    }

    public void SetName(string text)
    {
        this.Level.Name = text;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SaveLevel()
    {
        this.Level.TileMap = FindObjectOfType<TilemapController>().tileMap;

        // Todo: save level
    }

    private void DisableViewsExcept(GameObject except, string title = null, bool disableUI = false)
    {
        this.Main.SetActive(false);
        this.Editor.SetActive(false);
        this.WaveEditor.SetActive(false);

        this.Background.SetActive(!disableUI);

        this.Title.enabled = !disableUI;
        this.Title.text = title ?? this.Title.text;

        except.SetActive(true);
    }
}