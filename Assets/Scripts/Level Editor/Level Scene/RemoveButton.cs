namespace Assets.Scripts.LevelEditor.LevelScene
{
    using Assets.Scripts.Contracts;
    using UnityEngine;

    public class RemoveButton : MonoBehaviour
    {
        public int x;
        public int z;

        public GameObject Ground;

        public void RemovePath()
        {
            var tilemapController = GameObject.FindObjectOfType<TilemapController>();
            tilemapController.tileMap.MapData[x, z] = (int)TileEnum.Ground;

            Destroy(GameObject.Find($"[{x}],[{z}]"));

            var position = new Vector3(x * 10, 0, z * 10);

            var ground = Instantiate(Ground, position, new Quaternion());
            ground.transform.parent = this.transform.parent;
            ground.name = $"[{x}],[{z}]";

            tilemapController.RevertDrawPoint();
        }

        private void OnMouseDown()
        {
            this.RemovePath();
        }
    }
}