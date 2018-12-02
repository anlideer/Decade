using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float colddown = 0.1f;
    public float currentTime;
    public Vector2Int[,] next = new Vector2Int[GM.width, GM.height];

    Animator anim;
    public Vector2Int sta, des;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        currentTime = Time.time;
        for (int i = 0; i < GM.width; i++)
            for (int j = 0; j < GM.height; j++)
                next[i, j] = new Vector2Int(-1, -1);
    }
	
	// Update is called once per frame
	void Update () {
        MoveTo(GM.PosToGrid(transform.position), new Vector2Int(0, 0));
        if (currentTime + colddown < Time.time)
        {
            currentTime = Time.time;
            Moving(GM.PosToGrid(transform.position));
        }
    }

    private void PlayerMove(int direction)  // direction = 0 - left, direction = 1 - right, direction = 2 - up, direction = 3 - down  move 1 grid
    {
        switch (direction)
        {
            case 0:
                anim.Play("Left");
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                break;
            case 1:
                anim.Play("Right");
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                break;
        }    
    }

    // search the best path
    private void MoveTo(Vector2Int sta, Vector2Int des)
    {
        if (sta == des)
            return;
        List<Vector2Int> open = new List<Vector2Int>();
        List<Vector2Int> closed = new List<Vector2Int>();
        Vector2Int[,] father = new Vector2Int[GM.width, GM.height];
        Vector2Int tmp = new Vector2Int(-1, -1);
        int[,] F = new int[GM.width, GM.height];
        int[,] G = new int[GM.width, GM.height];
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
        G[sta.x, sta.y] = 0;
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
                        int thisG = G[now.x, now.y] + 1;
                        int H = Mathf.Abs(r.x - des.x) + Mathf.Abs(r.y - des.y);
                        F[r.x, r.y] = thisG + H;
                        G[r.x, r.y] = thisG;
                        father[r.x, r.y] = now;
                        open.Add(r);
                    }
                    else if (open.Contains(r))
                    {
                        int thisG = G[now.x, now.y] + 1;
                        int H = Mathf.Abs(r.x - des.x) + Mathf.Abs(r.y - des.y);
                        if (G[r.x, r.y] > thisG)
                        {
                            F[r.x, r.y] = thisG + H;
                            G[r.x, r.y] = thisG;
                            father[r.x, r.y] = now;
                        }
                    }
                }
                else
                {
                    closed.Add(r);
                }
            }
            closed.Add(now);
        }


        // now we get the path
     List<Vector2Int> path = new List<Vector2Int>();
        now = des;
        path.Add(des);
        while(father[now.x, now.y] != new Vector2Int(-1, -1))
        {
            path.Insert(0, father[now.x, now.y]);
            now = father[now.x, now.y];
        }

        Vector2Int last = new Vector2Int();
        foreach (Vector2Int v in path)
        {
            if (v == sta)
            {
                last = v;
                continue;
            }
            next[last.x, last.y] = v;
            last = v;
        }
    }

    private void Moving(Vector2Int sta)
    {
        if (next[sta.x, sta.y] == new Vector2Int(-1, -1))
            return;
        Vector2Int moveVec = next[sta.x, sta.y] - sta;
        if (moveVec.x == 1) // right
            PlayerMove(1);
        else if (moveVec.x == -1)   // left
            PlayerMove(0);
        else if (moveVec.y == 1)    //  down
            PlayerMove(3);
        else if (moveVec.y == -1)   //   up
            PlayerMove(2);
        else
            Debug.Log("Error occurs in moving");
    }
}
