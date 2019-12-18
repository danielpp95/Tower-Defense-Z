namespace Assets.Scripts.LevelEditor.UI
{
    using System.Collections.Generic;
    using Assets.Scripts.Contracts;
    using Assets.Scripts.Engine;
    using UnityEngine;

    public class LevelsUI : MonoBehaviour
    {
        public GameObject LevelPrefab;
        public GameObject Container;

        public List<Level> levels;

        // Start is called before the first frame update
        void Start()
        {
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
            while (this.Container.transform.childCount > 0)
            {
                var child = this.Container.transform.GetChild(0);
                child.transform.SetParent(null);
                Destroy(child.gameObject);
            }

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