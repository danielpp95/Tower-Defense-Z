namespace Assets.Scripts.LevelEditor.LevelScene
{
    using System.Linq;
    using Assets.Scripts.Contracts;
    using Assets.Scripts.LevelEditor.UI;
    using UnityEngine;

    public class TilemapController : MonoBehaviour
    {
        private int sizeX;
        private int sizeZ;
        public int tileSize = 10;

        public bool EditorMode = true;

        public GameObject SpawnObject;
        public GameObject EndObject;
        public GameObject Ground;
        public GameObject Path;
        public GameObject AddButton;
        public GameObject RemoveButton;

        public Transform Camera;

        public TileMap tileMap;

        private Vector2Int DrawPoint;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void Initialize(TileMap tileMap)
        {
            if (tileMap == null)
            {
                Debug.LogError("TileMap cannot be null");
                return;
            }

            this.tileMap = tileMap;
            this.sizeX = tileMap.SizeX;
            this.sizeZ = tileMap.SizeY;

            DrawPoint = tileMap.FollowingPath.Any() ?
                tileMap.FollowingPath.Last() :
                new Vector2Int(this.tileMap.StartPoint.x, this.tileMap.StartPoint.y);

            Camera.transform.position = new Vector3(sizeX * tileSize / 2, sizeX * tileSize, -sizeZ * 1.5f);
            Camera.transform.rotation = Quaternion.Euler(60f, 0, 0);

            BuildTerrain();

            AddBuildButton();

            this.EnableSaveButton();
        }

        private void BuildTerrain()
        {
            this.RemoveChildrens(this.gameObject);

            for (int x = 0; x < sizeX; x++)
            {
                for (int z = 0; z < sizeZ; z++)
                {
                    var tileType = (TileEnum)this.tileMap.GetTileAt(x, z);

                    var position = new Vector3(x * tileSize, 0, z * tileSize);

                    var tile = (GameObject)default;

                    switch (tileType)
                    {
                        case TileEnum.Unknown:
                            Debug.LogError($"Unknown tiletype found on: X: {x}, Y: {z}");
                            break;
                        case TileEnum.Ground:
                            tile = Instantiate(Ground, position, new Quaternion());
                            break;
                        case TileEnum.Path:
                            tile = Instantiate(Path, position, new Quaternion());
                            break;
                        case TileEnum.Spawn:
                            tile = Instantiate(SpawnObject, position, new Quaternion());
                            break;
                        case TileEnum.End:
                            tile = Instantiate(EndObject, position, new Quaternion());
                            break;
                        default:
                            break;
                    }

                    tile.name = $"[{x}],[{z}]";
                    tile.transform.parent = this.transform;
                }
            }
        }

        private void AddBuildButton()
        {
            if (this.EditorMode)
            {
                // left
                this.InstantiatePlusButton(
                    DrawPoint.x > 0 && tileMap.GetTileAt(DrawPoint.x - 1, DrawPoint.y) == (int)TileEnum.Ground,
                    (DrawPoint.x - 1) * this.tileSize,
                    DrawPoint.y * this.tileSize);

                // right
                this.InstantiatePlusButton(
                    DrawPoint.x < this.tileMap.SizeX - 1 && tileMap.GetTileAt(DrawPoint.x + 1, DrawPoint.y) == (int)TileEnum.Ground,
                    (DrawPoint.x + 1) * this.tileSize,
                    DrawPoint.y * this.tileSize);


                // top
                this.InstantiatePlusButton(
                    DrawPoint.y < this.tileMap.SizeY - 1 && this.tileMap.GetTileAt(DrawPoint.x, DrawPoint.y + 1) == (int)TileEnum.Ground,
                    DrawPoint.x * this.tileSize,
                    (DrawPoint.y + 1) * this.tileSize);

                // bottom
                this.InstantiatePlusButton(
                    (DrawPoint.y > 0 && this.tileMap.GetTileAt(DrawPoint.x, DrawPoint.y - 1) == (int)TileEnum.Ground),
                    DrawPoint.x * this.tileSize,
                    (DrawPoint.y - 1) * this.tileSize);

                // delete
                if (DrawPoint.x != this.tileMap.StartPoint.x ||
                    DrawPoint.y != this.tileMap.StartPoint.y)
                {
                    var position = new Vector3(
                        this.DrawPoint.x * this.tileSize,
                        0,
                        this.DrawPoint.y * this.tileSize);

                    var removebutton = Instantiate(RemoveButton, position, Quaternion.identity, this.transform);
                    var button = removebutton.GetComponent<RemoveButton>();
                    button.x = this.DrawPoint.x;
                    button.z = this.DrawPoint.y;
                }
            }
        }

        private void InstantiatePlusButton(bool condition, int x, int z)
        {
            if (condition)
            {
                var position = new Vector3(x, 0, z);

                var buttonGO = Instantiate(AddButton, position, new Quaternion(), this.transform);

                var button = buttonGO.GetComponent<PlusButton>();
                button.x = x / tileSize;
                button.z = z / tileSize;
            }
        }

        public void SetDrawPoint(int x, int z)
        {
            this.DrawPoint = new Vector2Int(x, z);

            this.tileMap.FollowingPath.Add(this.DrawPoint);

            this.RemoveButtons();
            this.AddBuildButton();
        }

        public void RevertDrawPoint()
        {
            this.tileMap.FollowingPath.RemoveAt(
                this.tileMap.FollowingPath.Count - 1);

            var point = this.tileMap.FollowingPath.Last();

            this.DrawPoint = new Vector2Int(point.x, point.y);

            this.RemoveButtons();
            this.AddBuildButton();
        }

        public void RemoveButtons()
        {
            var plusButtons = GameObject.FindObjectsOfType<PlusButton>();
            foreach (var button in plusButtons)
            {
                Destroy(button.gameObject);
            }

            var removeButtons = GameObject.FindObjectsOfType<RemoveButton>();
            foreach (var button in removeButtons)
            {
                Destroy(button.gameObject);
            }
        }

        public void EnableSaveButton()
        {
            if (this.EditorMode)
            {
                var enableButton = false;

                if (DrawPoint.x == this.tileMap.EndPoint.x &&
                    DrawPoint.y == this.tileMap.EndPoint.y + 1)
                {
                    enableButton = true;
                }
                if (DrawPoint.x == this.tileMap.EndPoint.x &&
                    DrawPoint.y == this.tileMap.EndPoint.y - 1)
                {
                    enableButton = true;
                }
                if (DrawPoint.x == this.tileMap.EndPoint.x + 1 &&
                    DrawPoint.y == this.tileMap.EndPoint.y)
                {
                    enableButton = true;
                }
                if (DrawPoint.x == this.tileMap.EndPoint.x - 1 &&
                    DrawPoint.y == this.tileMap.EndPoint.y)
                {
                    enableButton = true;
                }

                FindObjectOfType<SceneUI>().Savebutton.interactable = enableButton;
            }
        }
        private void RemoveChildrens(GameObject go)
        {
            while (go.transform.childCount > 0)
            {
                var child = go.transform.GetChild(0);
                child.transform.SetParent(null);
                Destroy(child.gameObject);
            }
        }
    }
}