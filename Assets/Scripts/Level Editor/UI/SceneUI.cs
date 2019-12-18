namespace Assets.Scripts.LevelEditor.UI
{
    using Assets.Scripts.LevelEditor.LevelScene;
    using UnityEngine;
    using UnityEngine.UI;

    public class SceneUI : MonoBehaviour
    {
        public Button Savebutton;

        // Start is called before the first frame update
        void Start()
        {
            this.Savebutton.interactable = false;
        }

        public void SaveTileMap()
        {
            var canvasUI = FindObjectOfType<CanvasUI>();
            canvasUI.ShowEditor();
            canvasUI.Level.TileMap = FindObjectOfType<TilemapController>().tileMap;
        }
    }
}