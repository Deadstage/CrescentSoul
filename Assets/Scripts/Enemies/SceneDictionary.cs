using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneDictionary : MonoBehaviour
{
    public GameObject scenePlayer;
    public Dictionary<int, string> hScene = new Dictionary<int, string>();
    public System.Random rnd = new System.Random();
    public Animator anim { get; private set; }

    public Player player;

    public Enemy1[] enemy1;

    public Enemy2[] enemy2;

    public bool isEngaged = false;

    public MashBar mashBar;

    void Start()
    {
        hScene.Add(0, "ZGScene1");
        hScene.Add(1, "ZGScene2");
        hScene.Add(2, "ZGScene3");

        hScene.Add(4, "Maid1Scene1");
        hScene.Add(5, "Maid1Scene2");
        hScene.Add(6, "Maid1Scene3");

        anim = GetComponent<Animator>();

        foreach(var item in enemy1)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), item.GetComponent<Collider2D>());
        }

        foreach(var item in enemy2)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), item.GetComponent<Collider2D>());
        }

        mashBar.gameObject.SetActive(false);

    }

    private void Update()
    {
        if(player.isEngaged == true)
        {
            mashBar.gameObject.SetActive(true);
        }

        if(mashBar.currentMash == 100)
        {
            enemy1[0].isEngaged = false;
            enemy2[0].isEngaged = false;
            player.isEngaged = false;
            this.isEngaged = false;
            anim.SetInteger("hAnim", -1);

            if (enemy1[0].isEngaged == true)
            {
                foreach(var item in enemy1)
                {
                    item.EnemyDisengager();
                    item.EnemyDisengaged();
                }

            }

            if(enemy2[0].isEngaged == true)
            {
                foreach(var item in enemy2)
                {
                    item.EnemyDisengager();
                    item.EnemyDisengaged();
                }
            }
        }

        if(player.isEngaged == false)
        {
            mashBar.currentMash = 0;
            mashBar.gameObject.SetActive(false);
        }
    }

    public void ZGHScene()
    {
        int zgScene = rnd.Next(0, 3);
        Debug.Log(hScene[zgScene]);

        if (zgScene.Equals(0))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 0);
            Debug.Log("playing first scene!");

            if(isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy1)
                {
                    item.EnemyEngager();
                }
                //enemy1[1].EnemyEngager();
            }
        }

        if (zgScene.Equals(1))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 1);
            Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy1)
                {
                    item.EnemyEngager();
                }
                //enemy1[1].EnemyEngager();
            }
        }

        if (zgScene.Equals(2))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 2);
            Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy1)
                {
                    item.EnemyEngager();
                }
                //enemy1[1].EnemyEngager();
            }

        }

    }

    public void M1HScene()
    {
        int maid1Scene = rnd.Next(4, 7);
        Debug.Log(hScene[maid1Scene]);

        if (maid1Scene.Equals(4))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 3);
            Debug.Log("playing first scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy2)
                {
                    item.EnemyEngager();
                }
                //enemy2.EnemyEngager();
            }
        }

        if (maid1Scene.Equals(5))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 4);
            Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy2)
                {
                    item.EnemyEngager();
                }
                //enemy2.EnemyEngager();
            }
        }

        if (maid1Scene.Equals(6))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 5);
            Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy2)
                {
                    item.EnemyEngager();
                }
                //enemy2.EnemyEngager();
            }
        }

    }
    
}
