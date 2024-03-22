using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
struct TilePattern
{
    public Tile[] tiles;
    public Vector2Int bounds;
}

public class GroundSpawner : MonoBehaviour
{
    [SerializeField]
    TilePattern pattern = new TilePattern();

    [SerializeField]
    Transform followedObject = null;

    [SerializeField]
    float spawnDistance = 10;

    [SerializeField]
    Tilemap tilemap = null;

    Vector3 lastSpawnPosition;

    [SerializeField]
    Transform yAnchor = null;

    [SerializeField]
    float cellWidth = 1;

    [SerializeField]
    float cellHeight = 10;

    Vector3Int WorldPosToCellCoord(float x, float y)
    {
        Vector3Int cell = tilemap.WorldToCell(new Vector3(x, y, 0));
        return cell;
    }

    void Start()
    {
        lastSpawnPosition = transform.position;
        Vector3 cellsize = tilemap.layoutGrid.cellSize;
        cellWidth = cellsize.x;
        cellHeight = cellsize.y;
    }

    void Update()
    {
        if (followedObject == null)
            return;

        if (followedObject.position.x < lastSpawnPosition.x + spawnDistance)
        {
            Spawn(followedObject.position.x - spawnDistance, false);
        }
    }

    // takes topright corner of the world position to spawn the pattern
    private void Spawn(float x_pos, bool x_dir = false)
    {
        // x_dir is true if the pattern is to be spawned to the right
        float x = lastSpawnPosition.x;
        // lambda function to check if x reaches the x_pos
        bool check(float x)
        {
            return x_dir ? x < x_pos : x > x_pos;
        }

        while (check(x))
        {
            // pattern.bounds is the shape of the pattern
            for (int i = 0; i < pattern.bounds.x; i++)
            {
                for (int j = 0; j < pattern.bounds.y; j++)
                {
                    // pattern.tiles is the tiles to be spawned
                    tilemap.SetTile(
                        WorldPosToCellCoord(
                            x_dir ? (int)x + (i * cellWidth) : (int)x - (i * cellWidth),
                            (int)(-(j * cellHeight) + yAnchor.position.y)
                        ),
                        pattern.tiles[(i * pattern.bounds.y) + j]
                    );
                }
            }

            if (x_dir)
            {
                x += cellWidth * pattern.bounds.x;
            }
            else
            {
                x -= cellWidth * pattern.bounds.x;
            }
        }
        lastSpawnPosition = new Vector3(x, lastSpawnPosition.y, lastSpawnPosition.z);
    }
}
