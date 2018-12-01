using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public static int width = 87;
    public static int height = 47;  // measure the map
    public static Vector2 zeroZero = new Vector2(-53.8f, 24f);  
    public static int[,] map = new int [width, height]; // 0 - grass, 1 - tree, 2 - wolf, 3 - sheep, 4 - house, 5 - others

    public GameObject[] Houses;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                map[i, j] = 0;
        foreach(GameObject house in Houses)
        {
            int x = (int)(house.transform.position.x - zeroZero.x);
            int y = (int)(zeroZero.y - house.transform.position.y);
            if (x <= 0 || y <= 0)
                Debug.Log("Error occurs in house position.");
            else
            {
                // 3 * 3
                for (int i = x - 1; i <= x + 1; i++)
                    for (int j = y - 1; j <= y + 1; j++)
                        map[i, j] = 4;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
