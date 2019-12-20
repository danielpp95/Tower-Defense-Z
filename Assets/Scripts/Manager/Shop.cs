namespace Assets.Scripts.Manager
{
    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.Contracts;
    using Assets.Scripts.Engine;
    using UnityEngine;
    using UnityEngine.UI;

    public class Shop : MonoBehaviour
    {

        public Button ButtonPrefab;
        public GameObject WarriorsContainer;
        public Color SelectedColor;

        private List<Warrior> warriors;
        public List<Button> ButtonPrefablist;
        private GameManager gameManager;
        private HighlightedWarriors highlightedWarriorsPrefabs;
        private WarriorPrefabs warriorsPrefabs;
        private GameObject previewPrefab;

        private int? selectedItem;
        private Warrior selectedWarrior;

        // Start is called before the first frame update
        void Start()
        {
            this.selectedItem = null;
            this.selectedWarrior = null;
            this.warriors = SaveEngine.LoadWarriors();
            this.gameManager = GetComponent<GameManager>();
            this.highlightedWarriorsPrefabs = GetComponent<HighlightedWarriors>();
            this.warriorsPrefabs = GetComponent<WarriorPrefabs>();

            foreach (var w in this.warriors)
            {
                var button = Instantiate(ButtonPrefab, this.WarriorsContainer.transform);
                button.onClick.AddListener(delegate { this.Selectitem(w, button); });

                var text = button.GetComponentInChildren<Text>();

                text.text = w.Name;

                this.ButtonPrefablist.Add(button);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(this.GetMouseOverTile());
            }

            ShowPreviewPrefab();
        }

        private void Selectitem(Warrior warrior, Button button)
        {
            if (this.previewPrefab != null)
            {
                Destroy(this.previewPrefab);
            }

            if (this.selectedItem == (int)warrior.Type)
            {
                this.selectedItem = null;
                this.SetButtonColor(button, Color.white);
                return;
            }

            this.selectedItem = (int)warrior.Type;

            this.SetButtonColor(button, this.SelectedColor);

            this.selectedWarrior = warrior;
        }

        private void SetButtonColor(Button button, Color color)
        {
            var colors = new ColorBlock();

            foreach (var b in ButtonPrefablist)
            {
                colors = b.colors;
                colors.normalColor = Color.white;
                colors.selectedColor = Color.white;
                b.colors = colors;
            }

            colors = button.colors;
            colors.normalColor = color;
            colors.selectedColor = color;
            button.colors = colors;
        }

        private void ShowPreviewPrefab()
        {
            //var preview = this.GetPreview();

            if (this.selectedItem != null && GetMouseOverTile() == TileEnum.Ground)
            {
                if (this.previewPrefab == null)
                {
                    this.previewPrefab = Instantiate(this.GetPrefab(), this.GetMouseXYZPoint() * 10, Quaternion.identity, this.transform);
                    this.previewPrefab.name = "previewPrefab";
                }
                else
                {
                    this.previewPrefab.SetActive(true);
                    this.previewPrefab.transform.position = (this.GetMouseXYZPoint() * 10);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    this.BuyTower();
                }
            }
            else if (this.selectedItem != null && GetMouseOverTile() != TileEnum.Ground)
            {
                if (this.previewPrefab != null)
                {
                    this.previewPrefab.gameObject.SetActive(false);
                }
            }
        }

        private Vector3 GetMousePosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }

            return Vector3.zero;
        }

        private Vector2 GetMouseXZPoint()
        {
            var point = this.GetMousePosition();

            return new Vector2(
                Mathf.Floor(point.x / 10),
                Mathf.Floor(point.z / 10));
        }
        private Vector3 GetMouseXYZPoint()
        {
            var point = this.GetMousePosition();

            return new Vector3(
                Mathf.Floor(point.x / 10),
                0,
                Mathf.Floor(point.z / 10));
        }

        private TileEnum GetMouseOverTile()
        {
            var point = this.GetMouseXZPoint();

            return (TileEnum)this.gameManager.level.TileMap.GetTileAt((int)point.x, (int)point.y);
        }

        private GameObject GetPrefab(bool hightlighted = true)
        {
            switch ((WarriorEnum)this.selectedItem)
            {
                case WarriorEnum.Goku:
                    return hightlighted ? this.highlightedWarriorsPrefabs.Goku : this.warriorsPrefabs.Goku;
                case WarriorEnum.GokuSSJ3:
                    return hightlighted ? this.highlightedWarriorsPrefabs.GokuSSJ3 : this.warriorsPrefabs.GokuSSJ3;
                default:
                    return null;
            }
        }

        private GameObject GetPreview()
        {
            return this.transform.Find("previewPrefab") != null ? this.transform.Find("previewPrefab").gameObject : null;
        }

        private void BuyTower()
        {
            if (this.gameManager.SubtractCash(this.selectedWarrior.Cost))
            {
                var point = this.GetMouseXYZPoint();
                this.gameManager.level.TileMap.MapData[(int)point.x, (int)point.z] = (int)TileEnum.Tower;

                Instantiate(this.GetPrefab(false), point * 10, Quaternion.identity);

                this.SetButtonColor(ButtonPrefablist.First(), Color.white);

                this.selectedItem = null;
                this.selectedWarrior = null;
                Destroy(this.previewPrefab);
            }
        }
    }
}