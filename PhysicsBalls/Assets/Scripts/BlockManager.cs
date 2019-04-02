using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject[] BlockPrefabs;
    public float LowestRowY;
    public float HighestRowY = 3;
    public float RowInerval = 0.75f;
    public float ColumnInterval = 0.8f;

    int order = 0;

    public static BlockManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        BuildBlock();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RaiseBlocks()
    {
        foreach (Transform block in transform)
        {
            block.transform.position += new Vector3(0, RowInerval, 0);
        }

        BuildBlock();
    }

    void BuildBlock()
    {
        float startX = -2;
        int count = 6;
        if (order % 2 == 1)
        {
            startX += ColumnInterval * 0.5f;
            count = 5;
        }

        for (int i = 0; i < count; i++)
        {
            if (Random.Range(0, 2) == 1)
            {
                int index = Random.Range(0, BlockPrefabs.Length);
                GameObject obj = Instantiate(BlockPrefabs[index]);
                obj.transform.SetParent(transform);
                obj.transform.position = new Vector2(startX + ColumnInterval * i, LowestRowY);
            }
        }

        order++;
    }
}
