namespace Assets.Scripts.LevelEditor.UI
{
    using System.Collections.Generic;
    using Assets.Scripts.Contracts;
    using Assets.Scripts.Engine;
    using Assets.Scripts.Helpers;
    using UnityEngine;

    public class LevelsUI : MonoBehaviour
    {
        public GameObject LevelPrefab;
        public GameObject Container;

        public List<Level> levels;

        private Helpers Helpers;

        // Start is called before the first frame update
        void Awake()
        {
            this.Helpers = FindObjectOfType<Helpers>();
        }

        private void OnEnable()
        {
            this.RenderLevels();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void RenderLevels()
        {
            this.Helpers.RemoveChildrens(this.Container);

            this.levels = SaveEngine.LoadLevels();

            foreach (var level in this.levels)
            {
                var levelButton = Instantiate(LevelPrefab, this.Container.transform);

                var levelScript = levelButton.GetComponent<LevelUI>();
                levelScript.Initialize(level);
            }
        }
    }
}