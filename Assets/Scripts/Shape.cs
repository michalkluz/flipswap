using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class Shape : MonoBehaviour
{
    public List<Block> blocks;
    public bool shouldSnap;

    public UnityEvent switchSnapToGrid;

    World world;

    private void Awake()
    {
        world = FindObjectOfType<World>();
    }
    public bool ShouldSnap
    {
        get
        {
            return shouldSnap;
        }
        set
        {
            shouldSnap = value;
            switchSnapToGrid.Invoke();
        }
    }

    public bool PushShape(Vector3 pushDirection)
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

    private void MoveShapeInDirection(Vector3 pushDirection)
    {
        var keysToRemove = new List<Vector3>();
        var valuesToAdd = new List<KeyValuePair<Vector3, Block>>();
        foreach (var block in blocks)
        {
            keysToRemove.Add(block.transform.position);
            valuesToAdd.Add(new KeyValuePair<Vector3, Block>(block.transform.position + pushDirection, block));
        }
        world.UpdateWorldGrid(keysToRemove, valuesToAdd);
        transform.position += pushDirection;
    }

    public Block GetOccupyingBlock(Vector3 position)
    {
        Block result = null;
        foreach (Block block in blocks)
        {
            if (block.transform.position.x == position.x && block.transform.position.y == position.y && block.transform.position.z == position.z)
            {
                result = block;
            }
        }
        return result;
    }

    //public void SwitchSnappingToGrid()
    //{
    //    foreach (Block block in blocks)
    //    {
    //        block.gridsnapper.shouldSnapToGrid = shouldSnap;
    //    }
    //}

    void Update()
    {

        //SwitchSnappingToGrid();

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
