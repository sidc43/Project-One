using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagicHandler
{
    public void Ability(Vector3Int startPos, Vector3Int targetPos, GameObject instance);
    public void UpdateMana();
}
