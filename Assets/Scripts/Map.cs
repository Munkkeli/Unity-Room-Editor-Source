using UnityEngine;
using System.Collections;
using Tiles;

public class Map : MonoBehaviour {
    public Tile[,] tiles;
    public int width = 16, height = 16;
    public Geometry geometry;
    public Transform pointer;
    public LayerMask layers;
    public Material normal, outside;

    private System.Type current = typeof(Basement);

    void Awake() {
        Textures.Create();
        normal.mainTexture = Textures.TILEMAP;

        tiles = new Tile[width, height];
        geometry = new Geometry(2);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                tiles[x, y] = new Outside();
            }
        }

        GetComponent<MeshRenderer>().sharedMaterials = new Material[] { normal, outside };

        Create();
    }

	void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layers)) {
            Vector3 pos = new Vector3(Mathf.Floor(hit.point.x) + 0.5f, 3.1f, Mathf.Floor(hit.point.z) + 0.5f);
            pointer.position = pos;

            Vector2 tile = new Vector2((int)Mathf.Floor(pos.x - transform.position.x), (int)Mathf.Floor(pos.z - transform.position.z));
            if (Input.GetMouseButton(0) && tiles[(int)tile.x, (int)tile.y].GetType() != current) {
                tiles[(int)tile.x, (int)tile.y] = (Tile)System.Activator.CreateInstance(current);
                Create();
            }

            if (Input.GetMouseButton(1)  && tiles[(int)tile.x, (int)tile.y].GetType() != typeof(Outside)) {
                tiles[(int)tile.x, (int)tile.y] = new Outside();
                Create();
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
            current = typeof(Basement);

        if (Input.GetKeyUp(KeyCode.Alpha2))
            current = typeof(Warehouse);

        if (Input.GetKeyUp(KeyCode.Alpha3))
            current = typeof(Hallway);
    }

    public void Create() {
        geometry.Clear();

        Debug.Log("Created");

        for(int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Tile[] sides = new Tile[4];

                if (y < height - 1)
                    sides[0] = tiles[x, y + 1];

                if (x < width - 1)
                    sides[1] = tiles[x + 1, y];

                if (y > 0)
                    sides[2] = tiles[x, y - 1];

                if (x > 0)
                    sides[3] = tiles[x -1 , y];

                tiles[x, y].Create(x, y, geometry, sides);
            }
        }

        Mesh mesh = geometry.Mesh();
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
