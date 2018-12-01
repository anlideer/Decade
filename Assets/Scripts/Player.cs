using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 10f;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void PlayerMove(int direction, int dis)  // direction = 0 - left, direction = 1 - right, direction = 2 - up, direction = 3 - down
    {
        switch (direction)
        {
            case 0:
                anim.Play("Left");
                transform.Translate(-1f * moveSpeed * Time.deltaTime, 0, 0);
                break;
            case 1:
                anim.Play("Right");
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                break;
            case 2:
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                break;
            case 3:
                transform.Translate(0, -1f * moveSpeed * Time.deltaTime, 0);
                break;
        }    
    }

    // 有点晚了，写一下寻路，不知道对不对，明天起来再检查，先放着
    private void MoveTo(Vector2Int sta, Vector2Int des)
    {
        List<Vector2Int> open = new List<Vector2Int>();
        List<Vector2Int> closed = new List<Vector2Int>();
        Vector2Int[,] father = new Vector2Int[GM.width, GM.height];
        Vector2Int tmp = new Vector2Int(-1, -1);
        int[,] F = new int[GM.width, GM.height];
        for (int i = 0; i < GM.width; i++)
        {
            for (int j = 0; j < GM.height; j++)
            {
                father[i, j] = tmp;
                F[i, j] = -10000;   // pretend it's -INF lol
            }
        }
        open.Add(sta);
        Vector2Int now = sta;
        while(now != des)
        {
            int minF = 100000;  // pretend it's +INF lol
            foreach (Vector2Int element in open)
            {
                if (minF > F[element.x, element.y])
                {
                    minF = F[element.x, element.y];
                    now = element;
                }
            }
            open.Remove(now);
            // left, right, up, down
            Vector2Int[] round = { new Vector2Int(now.x - 1, now.y), new Vector2Int(now.x + 1, now.y), new Vector2Int(now.x, now.y + 1), new Vector2Int(now.x, now.y - 1) };
            foreach (Vector2Int r in round)
            {
                if (r.x >= 0 && r.y >= 0 && r.x < GM.width && r.y < GM.height && GM.map[r.x, r.y] == 0)
                {
                    if (closed.Contains(r))
                        continue;
                    else if (!open.Contains(r))
                    {
                        int G = Mathf.Abs(r.x - sta.x) + Mathf.Abs(r.y - sta.y);
                        int H = Mathf.Abs(r.x - des.x) + Mathf.Abs(r.y - des.y);
                        F[r.x, r.y] = G + H;
                        father[r.x, r.y] = now;
                        open.Add(r);
                    }
                    else if (open.Contains(r))
                    {
                        int G = Mathf.Abs(r.x - sta.x) + Mathf.Abs(r.y - sta.y);
                        int H = Mathf.Abs(r.x - des.x) + Mathf.Abs(r.y - des.y);
                        if (F[r.x, r.y] > G + H)
                        {
                            F[r.x, r.y] = G + H;
                            father[r.x, r.y] = now;
                        }
                    }
                }
            }
            closed.Add(now);
        }
        // now we get the path
        // TODO: use the father of des to recall
        // 今天太困了，有事托梦...
    }

}
