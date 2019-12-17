using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateLevelButton : MonoBehaviour
{
    public GameObject Main;
    public GameObject Editor;
    public GameObject WaveEditor;
    public GameObject SceneEditor;

    public GameObject Background;

    public Text Title;

    private int Cash = 0;
    private int InitialCountDown = 0;
    private string levelName = string.Empty;

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

    public void EditCash(string text)
    {
        this.Cash = int.Parse(text);
    }

    public void EditInitialCountdown(string text)
    {
        this.InitialCountDown = int.Parse(text);
    }

    public void EditName(string text)
    {
        this.levelName = text;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SaveLevel()
    {
        var tileMap = GameObject.FindObjectOfType<TilemapController>().tileMap;

        var level = new Level
        {
            InitialCash = this.Cash,
            TileMap = tileMap,
            TimeToFirtsWave = this.InitialCountDown,
            Name = this.levelName,
        };
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
