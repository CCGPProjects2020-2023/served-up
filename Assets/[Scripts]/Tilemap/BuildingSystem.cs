using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private TileBase whiteTile;

    public GameObject[] prefabs;

    private PlaceableObject objectToPlace;

    private void Awake()
    {
        instance = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(prefabs[0]);
        }
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        } else
        {
            return Vector3.zero;
        }
    }
    
    public Vector3 SnapCoordinateToGrid(Vector3 pos)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(pos);
        pos = grid.GetCellCenterWorld(cellPos);
        return pos;
    }

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 pos = SnapCoordinateToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);

        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }
}
