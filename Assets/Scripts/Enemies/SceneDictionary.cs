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

    public Enemy3[] enemy3;

    public bool isEngaged = false;

    public MashBar mashBar;

    private Coroutine changeAnimationCoroutine;

    void Start()
    {
        hScene.Add(0, "ZGScene1_1");
        hScene.Add(1, "ZGScene2_1");
        hScene.Add(2, "ZGScene3_1");

        hScene.Add(4, "Maid1Scene1_1");
        hScene.Add(5, "Maid1Scene2_1");
        hScene.Add(6, "Maid1Scene3_1");

        hScene.Add(8, "Maid3Scene1_1");
        hScene.Add(9, "Maid3Scene2_1");
        hScene.Add(10, "Maid3Scene3_1");

        anim = GetComponent<Animator>();

        foreach(var item in enemy1)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), item.GetComponent<Collider2D>());
        }

        foreach(var item in enemy2)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), item.GetComponent<Collider2D>());
        }

        foreach(var item in enemy3)
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
            enemy3[0].isEngaged = false;
            player.isEngaged = false;
            this.isEngaged = false;
            anim.SetInteger("hAnim", -1);

            if (changeAnimationCoroutine != null)
            {
                StopCoroutine(changeAnimationCoroutine);
                changeAnimationCoroutine = null;
            }

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

            if(enemy3[0].isEngaged == true)
            {
                foreach(var item in enemy3)
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

    private IEnumerator ChangeAnimationPhaseAfterDelay(float delay, int firstPhase, int nextPhase, int nextPhase2, float thirdPhaseDuration)
    {
        yield return new WaitForSeconds(delay);
        if (mashBar.currentMash == 100) yield break;
        anim.SetInteger("hAnim", nextPhase);

        yield return new WaitForSeconds(delay);
        if (mashBar.currentMash == 100) yield break;
        anim.SetInteger("hAnim", nextPhase2);

        yield return new WaitForSeconds(thirdPhaseDuration);
        if (mashBar.currentMash == 100) yield break;
        anim.SetInteger("hAnim", firstPhase);
        changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(delay, firstPhase, nextPhase, nextPhase2, thirdPhaseDuration));
    }

    public void ZGHScene()
    {
        int zgScene = rnd.Next(0, 3);
        //Debug.Log(hScene[zgScene]);

        if (zgScene.Equals(0))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 0);
            //Debug.Log("playing first scene!");

            if(isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy1)
                {
                    item.EnemyEngager();
                }
                //enemy1[1].EnemyEngager();
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 0, 100, 200, 6.37f));
            }
        }

        if (zgScene.Equals(1))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 1);
            //Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy1)
                {
                    item.EnemyEngager();
                }
                //enemy1[1].EnemyEngager();
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 1, 101, 201, 6.25f));
            }
        }

        if (zgScene.Equals(2))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 2);
            //Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy1)
                {
                    item.EnemyEngager();
                }
                //enemy1[1].EnemyEngager();
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 2, 102, 202, 7.11f));
            }

        }

    }

    public void M1HScene()
    {
        int maid1Scene = rnd.Next(4, 7);
        //Debug.Log(hScene[maid1Scene]);

        if (maid1Scene.Equals(4))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 3);
            //Debug.Log("playing first scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy2)
                {
                    item.EnemyEngager();
                }
                //enemy2.EnemyEngager();
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 3, 103, 203, 8.42f));
            }
        }

        if (maid1Scene.Equals(5))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 4);
            //Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy2)
                {
                    item.EnemyEngager();
                }
                //enemy2.EnemyEngager();
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 4, 104, 204, 6.06f));
            }
        }

        if (maid1Scene.Equals(6))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 5);
            //Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy2)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 5, 105, 205, 7f));
            }
        }

    }

    public void M3HScene()
    {
        int maid3Scene = rnd.Next(8, 11);
        //Debug.Log(hScene[maid3Scene]);

        if (maid3Scene.Equals(8))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 6);
            //Debug.Log("playing first scene!");

            if(isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy3)
                {
                    item.EnemyEngager();
                }

                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 6, 106, 206, 5.21f));
            }
        }

        if (maid3Scene.Equals(9))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 7);
            //Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy3)
                {
                    item.EnemyEngager();
                }
               
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 7, 107, 207, 6.15f));
            }
        }

        if (maid3Scene.Equals(10))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            anim.SetInteger("hAnim", 8);
            //Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy3)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 8, 108, 208, 5.08f));
            }
        }
    }
}
