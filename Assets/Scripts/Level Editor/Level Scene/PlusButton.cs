namespace Assets.Scripts.LevelEditor.LevelScene
{
    using Assets.Scripts.Contracts;
    using UnityEngine;

    public class PlusButton : MonoBehaviour
    {
        public int x;
        public int z;

        public GameObject Path;

        public void AddPath()
        {
            var tilemapController = GameObject.FindObjectOfType<TilemapController>();
            tilemapController.tileMap.MapData[x, z] = (int)TileEnum.Path;

            Destroy(GameObject.Find($"[{x}],[{z}]"));

            var position = new Vector3(x * 10, 0, z * 10);

            var path = Instantiate(Path, position, new Quaternion());
            path.transform.parent = this.transform.parent;
            path.name = $"[{x}],[{z}]";

            tilemapController.SetDrawPoint(x, z);
        }

        private void OnMouseDown()
        {
            this.AddPath();
        }
    }
}