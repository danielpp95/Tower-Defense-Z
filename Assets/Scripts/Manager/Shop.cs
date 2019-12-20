namespace Assets.Scripts.Manager
{
    using System.Collections.Generic;
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

        private int? selectedItem;


        // Start is called before the first frame update
        void Start()
        {
            this.selectedItem = null;
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
            var preview = this.GetPreview();
            if (preview != null)
            {
                Destroy(preview);
            }
            if (this.selectedItem == (int)warrior.Type)
            {
                this.selectedItem = null;
                this.SetButtonColor(button, Color.white);
                return;
            }

            this.selectedItem = (int)warrior.Type;

            this.SetButtonColor(button, this.SelectedColor);

            Debug.Log(warrior.Name);
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
            var preview = this.GetPreview();

            if (this.selectedItem != null && GetMouseOverTile() == TileEnum.Ground)
            {
                if (preview == null)
                {
                    var go = Instantiate(this.GetPrefab(), this.GetMouseXYZPoint() * 10, Quaternion.identity, this.transform);
                    go.name = "previewPrefab";
                }
                else
                {
                    preview.SetActive(true);
                    preview.transform.position = (this.GetMouseXYZPoint() * 10);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    this.BuyTower();
                }
            }
            else if (this.selectedItem != null && GetMouseOverTile() != TileEnum.Ground)
            {
                if (preview != null)
                {
                    preview.gameObject.SetActive(false);
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

        }
    }
}