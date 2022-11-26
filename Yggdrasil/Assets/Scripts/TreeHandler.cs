using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHandler : Singleton<TreeHandler>
{
    [SerializeField] private int treeSize;
    [SerializeField] private float scaleValue;
    [SerializeField] private float scaleScreenValue;
    public int GetTreeSize => treeSize;
    public Health treeHealth;
    private int preSize;
    private void Start()
    {
        treeSize = 1;
        preSize = 1;
    }

    private void Update()
    {
        if (treeSize % 5 == 0 && treeSize != preSize)
        {
            preSize = treeSize;
            var scale = this.gameObject.transform.localScale;
            this.gameObject.transform.localScale = new Vector3(scale.x + scaleValue, scale.y + scaleValue, scale.z + scaleValue);
            EnemySpawner.Instance.UpdateScreenSize(scaleScreenValue);
        }
    }

    public void AddCoin()
    {
        treeSize++;
    }
}
