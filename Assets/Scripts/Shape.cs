using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class Shape : MonoBehaviour
{
    public List<Block> blocks;
    [SerializeField] bool isShapePushable;

    World world;

    private void Awake()
    {
        world = FindObjectOfType<World>();
    }

    public bool CheckIfPushable(Vector3Int pushDirection)
    {
        if (!isShapePushable)
        {
            return false;
        }
        var isShapePushLegal = true;
        var isBlockPushLegal = (blocks.Select(block => block.CheckIfPushable(pushDirection))).ToList();

        foreach (bool isPushable in isBlockPushLegal)
        {
            if (!isPushable)
            {
                isShapePushLegal = false;
            }
        }

        if (isShapePushLegal)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveShapeInDirection(Vector3Int pushDirection)
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
