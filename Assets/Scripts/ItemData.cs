using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Vector2Int posTile;
    public BrickData brickData;
    public int indexOnMap;

    public ItemData() {

    }

    public ItemData(int _indexOnMap, Vector2Int _posTile)
    {
        indexOnMap = _indexOnMap;
        posTile = _posTile;
    }
}
