using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D/Tiles/LevelTile")]
public class LevelTile : Tile
{
    public GameObject prefab;
    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (go is null) return false;
        go.transform.rotation = prefab.transform.rotation;
        go.transform.position += prefab.transform.position;
        go.transform.localScale = prefab.transform.localScale;
        return false;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }
}