using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Blocks : MonoBehaviour
{
    public List<Block> blocks;

    public Block GetOccupyingBlock(Vector3 position)
    {
        Block result = null;
        foreach (Block block in blocks)
        {
            if (block.transform.position.x == position.x && block.transform.position.z == position.z)
            {
                result = block;
            }
        }
        return result;
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
        var blocksFound = FindObjectsOfType<Block>();
        foreach (Block block in blocksFound)
        {
            blocks.Add(block);
        }

    }
}
