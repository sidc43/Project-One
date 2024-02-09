using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedHandler
{
    public void ThrowInstance(Vector3Int initPos, Vector3Int targetPos, GameObject instance);
}
