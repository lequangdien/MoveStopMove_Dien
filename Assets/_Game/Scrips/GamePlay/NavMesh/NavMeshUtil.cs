using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtil
{
    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {

        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }
}
