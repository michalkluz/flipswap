using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public List<Block> blocks;

    World world;

    private void Awake()
    {
        world = FindObjectOfType<World>();
    }

    public bool PushShape(Vector3Int pushDirection)
    {
        var isShapePushable = true;
        var isBlockPushable = new List<bool>();
        foreach (var block in blocks)
        {
            isBlockPushable.Add(block.CheckIfPushable(pushDirection));
        }

        foreach (bool isPushable in isBlockPushable)
        {
            if (!isPushable)
            {
                isShapePushable = false;
            }
        }

        if (isShapePushable)
        {
            MoveShapeInDirection(pushDirection);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MoveShapeInDirection(Vector3Int pushDirection)
    {
        var keysToRemove = new List<Vector3Int>();
        var entriesToAdd = new List<KeyValuePair<Vector3Int, Block>>();
        foreach (var block in blocks)
        {
            keysToRemove.Add(Vector3Int.RoundToInt(block.transform.position));
            entriesToAdd.Add(new KeyValuePair<Vector3Int, Block>(Vector3Int.RoundToInt(block.transform.position) + pushDirection, block));
        }
        world.UpdateWorldGrid(keysToRemove, entriesToAdd); //removing all keys, then adding them again at the same time so they don't overlap
        transform.position += pushDirection;
    }

    void Update()
    {
        if (transform.childCount != blocks.Count)
        {
            ResetBlocks();
        }
    }

    void ResetBlocks()
    {
        blocks = new List<Block>();
        var blocksFound = GetComponentsInChildren<Block>();
        foreach (Block block in blocksFound)
        {
            blocks.Add(block);
        }
    }
}
