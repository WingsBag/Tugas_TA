    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class GridPlayerMovement : MonoBehaviour
    {
        public Tilemap floorTilemap;
        public TileBase spawnTile;
        public Tilemap wallTilemap;

        private Vector2Int gridPos;

        void Start()
        {
            foreach (var pos in floorTilemap.cellBounds.allPositionsWithin)
            {
                if (!floorTilemap.HasTile(pos)) continue;

                TileBase tile = floorTilemap.GetTile(pos);
                if (tile != null && tile.name == spawnTile.name)
                {
                    gridPos = new Vector2Int(pos.x, pos.y);
                    transform.position = floorTilemap.GetCellCenterWorld(pos);
                    Debug.Log("Spawn Tile ditemukan di: " + pos);
                    return;
                }
            }

            Debug.LogError("Spawn Tile TIDAK ditemukan!");
        }

        void Update()
        {
            Vector2Int dir = Vector2Int.zero;

            if (Input.GetKeyDown(KeyCode.W)) dir = Vector2Int.up;
            if (Input.GetKeyDown(KeyCode.S)) dir = Vector2Int.down;
            if (Input.GetKeyDown(KeyCode.A)) dir = Vector2Int.left;
            if (Input.GetKeyDown(KeyCode.D)) dir = Vector2Int.right;

            if (dir != Vector2Int.zero)
                TryMove(dir);
        }

        void TryMove(Vector2Int dir)
        {
            Vector2Int target = gridPos + dir;

            if (wallTilemap.HasTile(new Vector3Int(target.x, target.y, 0)))
                return;

            gridPos = target;
            transform.position = GridToWorld(gridPos);
        }

        Vector3 GridToWorld(Vector2Int g)
        {
            return new Vector3(g.x + 0.5f, g.y + 0.5f, 0);
        }
    }