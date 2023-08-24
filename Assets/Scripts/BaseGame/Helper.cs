using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static bool SafeSpawnPoint(Vector3 pos, LayerMask layerMask, float searchRadius)
    {
        Ray ray = new Ray(new Vector3(pos.x, pos.y + 10f, pos.z), Vector3.down);
        RaycastHit hit;
        if(Physics.SphereCast(ray, searchRadius, out hit, 100f, ~layerMask))
        {
            //Debug.Log($"I hit {hit.collider.name}");
            return false;
        }
        else
        {
            //Debug.Log("Safely spawned");
            return true;
        }
    }
}
