using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float tileSize = 125f;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private List<Sprite> possibleBackgrounds;

    private int gridRadius = 1; // 1 genera una grilla 3x3 (de -1 a 1 en x e y)
    private Dictionary<Vector2Int, GameObject> tileMap = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int lastPlayerTilePos;

    private void Start()
    {
        lastPlayerTilePos = GetPlayerTilePos();

        // Instanciar la grilla inicial
        for (int offsetX = -gridRadius; offsetX <= gridRadius; offsetX++)
        {
            for (int offsetY = -gridRadius; offsetY <= gridRadius; offsetY++)
            {
                Vector2Int tileCoord = new Vector2Int(lastPlayerTilePos.x + offsetX, lastPlayerTilePos.y + offsetY);
                Vector3 worlPosition = TileCoordToWorld(tileCoord);

                GameObject tile = Instantiate(tilePrefab, worlPosition, Quaternion.identity);
                SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = possibleBackgrounds[Random.Range(0, possibleBackgrounds.Count)];

                tileMap[tileCoord] = tile;
            }
        }
    }

    private void Update()
    {
        Vector2Int currentPlayerTilePos = GetPlayerTilePos();

        // Si el jugador cambió de tile, actualizamos la grilla
        if (currentPlayerTilePos != lastPlayerTilePos)
        {
            UpdateTilePositions(currentPlayerTilePos);
            lastPlayerTilePos = currentPlayerTilePos;
        }
    }

    private Vector2Int GetPlayerTilePos()
    {
        int x = Mathf.FloorToInt(player.position.x / tileSize);
        int y = Mathf.FloorToInt(player.position.y / tileSize);
        return new Vector2Int(x, y);
    }

    private Vector3 TileCoordToWorld(Vector2Int coord)
    {
        return new Vector3(coord.x * tileSize, coord.y * tileSize, 0f);
    }

    private void UpdateTilePositions(Vector2Int centerTile)
    {
        Dictionary<Vector2Int, GameObject> newTileMap = new Dictionary<Vector2Int, GameObject>();

        for (int dx = -gridRadius; dx <= gridRadius; dx++)
        {
            for (int dy = -gridRadius; dy <= gridRadius; dy++)
            {
                Vector2Int desiredCoord = new Vector2Int(centerTile.x + dx, centerTile.y + dy);

                if (tileMap.TryGetValue(desiredCoord, out GameObject tile))
                {
                    // Ya existe un tile en esa posición, mantenerlo
                    newTileMap[desiredCoord] = tile;
                }
                else
                {
                    // Reutilizar un tile que quedó fuera del nuevo rango
                    Vector2Int reusableCoord = FindFarTileCoord(centerTile);
                    GameObject reusableTile = tileMap[reusableCoord];
                    tileMap.Remove(reusableCoord);

                    reusableTile.transform.position = TileCoordToWorld(desiredCoord);
                    SpriteRenderer sr = reusableTile.GetComponent<SpriteRenderer>();
                    sr.sprite = possibleBackgrounds[Random.Range(0, possibleBackgrounds.Count)];

                    newTileMap[desiredCoord] = reusableTile;
                }
            }
        }

        tileMap = newTileMap;
    }

    private Vector2Int FindFarTileCoord(Vector2Int center)
    {
        foreach (var kvp in tileMap)
        {
            Vector2Int coord = kvp.Key;
            if (Mathf.Abs(coord.x - center.x) > gridRadius || Mathf.Abs(coord.y - center.y) > gridRadius)
            {
                return coord;
            }
        }

        // Esto no debería pasar
        Debug.LogWarning("No se encontró un tile para reutilizar.");
        return Vector2Int.zero;
    }
}
