using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject[] BlockPrefabs;
    public GameObject[] ItemPrefabs;
    public float ItemProbability = 0.1f;
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

        CheckGameOver();
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
            // 随机该位置是否有砖块
            if (Random.Range(0, 2) == 1)
            {
                float randomSeed = Random.Range(0, 1f);
                if (randomSeed < ItemProbability) // 生成一个道具
                {
                    int index = Random.Range(0, ItemPrefabs.Length);
                    GameObject obj = Instantiate(ItemPrefabs[index]);
                    obj.transform.SetParent(transform);
                    obj.transform.position = new Vector2(startX + ColumnInterval * i, LowestRowY);
                }
                else // 生成一个砖块
                {
                    int index = Random.Range(0, BlockPrefabs.Length);
                    GameObject obj = Instantiate(BlockPrefabs[index]);
                    obj.transform.SetParent(transform);
                    obj.transform.position = new Vector2(startX + ColumnInterval * i, LowestRowY);
                }
            }
        }

        order++;
    }

    internal void ResetBlocks()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        BuildBlock();
    }

    void CheckGameOver()
    {
        foreach (Transform block in transform)
        {
            if (block.transform.position.y > HighestRowY)
            {
                Debug.Log("游戏结束");
                GameManager.Instance.OnGameOver();
            }
        }
    }
}
