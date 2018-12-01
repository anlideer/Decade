using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeSpawn : MonoBehaviour {

    public GameObject tree;
    GameObject[] trees;

    public int number = 7;  // how many trees should be spawned

	// Use this for initialization
	void Start () {
        for (int i = 0; i < number; i++)
        {
            spawn();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void spawn()
    {
        System.Random rand = new System.Random();
        Vector2Int blockPos = new Vector2Int(rand.Next(3, GM.width - 3), rand.Next(3, GM.height - 3));
        bool flag = true;
        for (int i = blockPos.x - 3; i <= blockPos.x + 3; i++)
            for (int j = blockPos.y - 3; j <= blockPos.y + 3; j++)
                if (GM.map[i, j] != 0)
                    flag = false;
        if (flag == false)
            spawn();
        else
        {
            for (int i = blockPos.x - 1; i <= blockPos.x + 1; i++)
                for (int j = blockPos.y - 1; j <= blockPos.y; j++)
                    GM.map[i, j] = 1;
            Vector3 pos = new Vector3(GM.zeroZero.x + blockPos.x, GM.zeroZero.y - blockPos.y, 0);
            Instantiate(tree, pos, transform.rotation);
        }
    }
}
