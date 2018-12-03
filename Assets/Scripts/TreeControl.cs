using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeControl : MonoBehaviour {

    public float fruitColddown = GM.timeFly / 4;    // the speed of picking fruit
    bool picking = false;
    public Text restTimeText;
    bool shown = false;
    float  restTime;
    float last;
    Text obj;
    float colddown = GM.timeFly;

    SpriteRenderer spr;
    public Sprite TreeWithFruit;
    public Sprite TreeWithoutFruit;

    // Use this for initialization
    void Start () {
        picking = false;
        shown = false;
        last = Time.time;
        colddown = GM.timeFly;
        spr = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if (picking && TreeSpawn.treeStatus[int.Parse(name.Substring(name.Length - 1, 1))] == true)
        {
            restTime = fruitColddown;
            if (!shown)
            {
                shown = true;
                obj = Instantiate(restTimeText, Camera.main.WorldToScreenPoint(transform.position), transform.rotation);
                restTimeText.text = ((int)restTime).ToString();
                last = Time.time;
            }
            restTime -= (Time.time - last);
            last = Time.time;
            restTimeText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            if (restTime <= 0)
            {
                Destroy(obj.gameObject);
                TreeSpawn.treeStatus[int.Parse(name.Substring(name.Length - 1, 1))] = false;
                last = Time.time;
                spr.sprite = TreeWithoutFruit;
            }
        }
        if (TreeSpawn.treeStatus[int.Parse(name.Substring(name.Length - 1, 1))] == false)
        {
            if (last + colddown < Time.time)
            {
                TreeSpawn.treeStatus[int.Parse(name.Substring(name.Length - 1, 1))] = true;
                spr.sprite = TreeWithFruit;
            }
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Player>().status == 1)
        {
            picking = true;
        }
    }
}
