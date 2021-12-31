using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetPlacement
{
    public static void SpawnAssetsOnChunkVerts(Vector3 pos, Vector2 centre, float assetOffset, Vector2 offsetMultiplier, GameObject treePrefab, Transform treeParent)
    {
        if(pos.y >= 2 && pos.y <= 2.5f) {
            if(offsetMultiplier.x == 0 && offsetMultiplier.y != 0)
            {
                GameObject tree0Y = Object.Instantiate(treePrefab, new Vector3(pos.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                tree0Y.name = "Tree_at_X=0";
                tree0Y.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y == 0)
            {
                GameObject treeX0 = Object.Instantiate(treePrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z), Quaternion.identity);
                treeX0.name = "Tree_at_Y=0";
                treeX0.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x == 0 && offsetMultiplier.y == 0)
            {
                GameObject tree00 = Object.Instantiate(treePrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                tree00.name = "Tree_at_XY=0";
                tree00.transform.parent = treeParent.transform;
            }
            if (offsetMultiplier.x != 0 && offsetMultiplier.y != 0)
            {
                GameObject treeXY = Object.Instantiate(treePrefab, new Vector3(pos.x + assetOffset * offsetMultiplier.x, pos.y, pos.z + assetOffset * offsetMultiplier.y), Quaternion.identity);
                treeXY.name = "Tree_value";
                treeXY.transform.parent = treeParent.transform;
            }
        }
    }
}
