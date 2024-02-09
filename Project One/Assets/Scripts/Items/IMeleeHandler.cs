using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeHandler
{
    public void SwingLeft(Collider2D weaponCollider, Collider2D targetCollider, Vector3Int pos);
    public void SwingRight(Collider2D weaponCollider, Collider2D targetCollider, Vector3Int pos);
}
