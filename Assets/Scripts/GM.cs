using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public static int width = 87;
    public static int height = 47;  // measure the map
    public static Vector2 zeroZero = new Vector2(-53.8f, 24f);  
    public static int[,] map = new int [width, height]; // 0 - grass, 1 - tree, 2 - wolf, 3 - sheep, 4 - house, 5 - others
    public static float timeFly = 15f;  // how many seconds means a year
    public LayerMask mask;
    public static bool infoOpen;
    public GameObject info;


    public GameObject[] Houses;

	// Use this for initialization
	void Start () {
        infoOpen = false;
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                map[i, j] = 0;
        foreach(GameObject house in Houses)
        {
            Vector2Int gridPos = PosToGrid(house.transform.position);
            if (gridPos == new Vector2Int(-1, -1))
                Debug.Log("Error occurs in house pos transfering");
            else
            {
                // 3 * 3
                for (int i = gridPos.x - 1; i <= gridPos.x + 1; i++)
                    for (int j = gridPos.y - 1; j <= gridPos.y + 1; j++)
                        map[i, j] = 4;
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            OpenInfo();
        }
	}

    public static Vector2Int PosToGrid(Vector3 pos)
    {
        int x = (int)(pos.x - zeroZero.x);
        int y = (int)(zeroZero.y - pos.y);
        if (x < 0 || y < 0)
        {
            Debug.Log("Error occurs in finding grid coordinate");
            return new Vector2Int(-1, -1);
        }
        return new Vector2Int(x, y);
    }

    // when we click the character, open the info window of the character.
    private void OpenInfo()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, mask);
        if (hit.collider)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Player>().ShowInfo();
            }
        }
    }

    public void closeInfo()
    {
        if (infoOpen == true)
        {
            infoOpen = false;
            info.SetActive(false);
        }
    }
}
