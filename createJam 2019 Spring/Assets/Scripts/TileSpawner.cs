using UnityEngine;

public class TileSpawner : MonoBehaviour {
    //public bool Overwrite;
    public GameObject gridParent;
    public bool parentIsCenter = false;

    [Header("Grid Tile look")]
    public GameObject gridTilePrefab;

    [Header("Grid size")]
    public int sizeX;
    public int sizeZ;
    public float tileSpace;
    private float posY;

    private Vector3 parentPos;

    public void generateGrid()
    {
        parentPos = gridParent.transform.position;
        posY = parentPos.y;
        if (!gridTilePrefab.GetComponent<Renderer>())
        {
            Debug.LogWarning("Grid tile prefab is missing Renderer");
        }

        //float xOffset = gridTilePrefab.GetComponent<Renderer>().bounds.size.x;
        float boundsSizeZ = gridTilePrefab.GetComponent<Renderer>().bounds.size.z;
        float boundsSizeX = gridTilePrefab.GetComponent<Renderer>().bounds.size.x;
        //float xOffset = gridTilePrefab.GetComponent<Renderer>().bounds.size.x;
        //float zOffset = gridTilePrefab.GetComponent<Renderer>().bounds.size.z;

        if (!parentIsCenter)
        {
            for (int i = 0; i < sizeX; i++)
            {
                //xOffset += tileSpace;
                for (int j = 0; j < sizeZ; j++)
                {                   
                    Vector3 position = new Vector3(i * (boundsSizeX + tileSpace) + parentPos.x, 
                        posY, 
                        j * (boundsSizeZ + tileSpace) + parentPos.z);
                    GameObject tile = Instantiate(gridTilePrefab, position, transform.rotation, gridParent.transform);
                }
            }
        }
        
        if (parentIsCenter)
        {
            Vector3 cornerPos = new Vector3(parentPos.x - Mathf.Abs(((sizeX/2) * boundsSizeX) +(tileSpace * (sizeX/2))), 
                parentPos.y, 
                parentPos.z - Mathf.Abs(((sizeZ/2) * boundsSizeZ) + (tileSpace * (sizeZ/2))));            
            for (int i = 0; i < sizeX; i++)
            {               
                for (int j = 0; j < sizeZ; j++)
                {
                    Vector3 position = new Vector3(i * (boundsSizeX + tileSpace) + cornerPos.x,
                        posY,
                        j * (boundsSizeZ + tileSpace) + cornerPos.z);
                    GameObject tile = Instantiate(gridTilePrefab, position, transform.rotation, gridParent.transform);
                }
            }
        }
    }

    public void overwriteGrid()
    {
        deleteGrid();
        generateGrid();
    }

    public void deleteGrid()
    {
        while (gridParent.transform.childCount > 0)
        {
            for (int i = 0; i < gridParent.transform.childCount; i++)
            {
                DestroyImmediate(gridParent.transform.GetChild(0).gameObject);
            }
        }
        //gridParent.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}
