using System.Linq;
using Assets.Scripts.Contracts;
using Assets.Scripts.Engine;
using Assets.Scripts.LevelEditor.LevelScene;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Level level;

    // Start is called before the first frame update
    void Awake()
    {
        this.level = SaveEngine.LoadLevels().Last();

        Debug.Log(level.TileMap.FollowingPath);

        this.gameObject.GetComponent<TilemapController>().Initialize(level.TileMap);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
