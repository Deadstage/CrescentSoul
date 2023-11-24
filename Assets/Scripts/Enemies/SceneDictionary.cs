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

    public Enemy4[] enemy4;

    public Enemy5[] enemy5;

    public Enemy6[] enemy6;

    public Enemy7[] enemy7;

    public bool isEngaged = false;

    public MashBar mashBar;

    private Coroutine changeAnimationCoroutine;

    void Start()
    {
        hScene.Add(0, "ZGScene1_1");
        hScene.Add(1, "ZGScene2_1");
        hScene.Add(2, "ZGScene3_1");

        hScene.Add(4, "MaidR1Scene1_1");
        hScene.Add(5, "MaidR1Scene2_1");
        hScene.Add(6, "MaidR1Scene3_1");

        hScene.Add(8, "Maid1Scene1_1");
        hScene.Add(9, "Maid1Scene2_1");
        hScene.Add(10, "Maid1Scene3_1");

        hScene.Add(12, "Maid2Scene1_1");
        hScene.Add(13, "Maid2Scene2_1");
        hScene.Add(14, "Maid2Scene3_1");

        hScene.Add(16, "Maid3Scene1_1");
        hScene.Add(17, "Maid3Scene2_1");
        hScene.Add(18, "Maid3Scene3_1");

        hScene.Add(20, "Maid4Scene1_1");
        hScene.Add(21, "Maid4Scene2_1");
        hScene.Add(22, "Maid4Scene3_1");

        hScene.Add(24, "DG1Scene1_1");
        hScene.Add(25, "DG1Scene2_1");
        hScene.Add(26, "DG1Scene3_1");
        

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

        foreach(var item in enemy4)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), item.GetComponent<Collider2D>());
        }

        foreach(var item in enemy5)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), item.GetComponent<Collider2D>());
        }

        foreach(var item in enemy6)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), item.GetComponent<Collider2D>());
        }

        foreach(var item in enemy7)
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
            enemy4[0].isEngaged = false;
            enemy5[0].isEngaged = false;
            enemy6[0].isEngaged = false;
            enemy7[0].isEngaged = false;
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

            if(enemy4[0].isEngaged == true)
            {
                foreach(var item in enemy4)
                {
                    item.EnemyDisengager();
                    item.EnemyDisengaged();
                }
            }

            if(enemy5[0].isEngaged == true)
            {
                foreach(var item in enemy5)
                {
                    item.EnemyDisengager();
                    item.EnemyDisengaged();
                }
            }

            if(enemy6[0].isEngaged == true)
            {
                foreach(var item in enemy6)
                {
                    item.EnemyDisengager();
                    item.EnemyDisengaged();
                }
            }

            if(enemy7[0].isEngaged == true)
            {
                foreach(var item in enemy7)
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

    public void MR1HScene()
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

    public void M1Hscene()
    {
        int maid1Scene = rnd.Next(8, 11);
        //Debug.Log(hScene[maid1Scene]);

        if (maid1Scene.Equals(8))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            //Debug.Log("playing first scene!");
            anim.SetInteger("hAnim", 6);

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy4)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 6, 106, 206, 6.56f));
            }
        }

        if (maid1Scene.Equals(9))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            //debug.log("playing second scene!");
            anim.SetInteger("hAnim", 7);

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy4)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 7, 107, 207, 6.19f));
            }
        }

        if (maid1Scene.Equals(10))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            //Debug.Log("playing third scene!");
            anim.SetInteger("hAnim", 8);

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy4)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 8, 108, 208, 6.44f));
            }
        }
    }

    public void M2HScene()
    {
        int maid2Scene = rnd.Next(12, 15);
        //Debug.Log(hScene[maid2Scene]);

        if (maid2Scene.Equals(12))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 9);
            //Debug.Log("playing first scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy5)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 9, 109, 209, 6.50f));
            }
        }

        if (maid2Scene.Equals(13))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 10);
            //Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy5)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 10, 110, 210, 6.00f));
            }
        }

        if (maid2Scene.Equals(14))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 11);
            //Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy5)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 11, 111, 211, 6f));
            }
        }
    }

    public void M3HScene()
    {
        int maid3Scene = rnd.Next(16, 19);
        //Debug.Log(hScene[maid3Scene]);

        if (maid3Scene.Equals(16))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 12);
            //Debug.Log("playing first scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy3)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 12, 112, 212, 8.42f));
            }
        }

        if (maid3Scene.Equals(17))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 13);
            //Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy3)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 13, 113, 213, 6.06f));
            }
        }

        if (maid3Scene.Equals(18))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 14);
            //Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy3)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 14, 114, 214, 7f));
            }
        }
    }

    public void M4HScene()
    {
        int maid4Scene = rnd.Next(20, 23);
        //Debug.Log(hScene[maid4Scene]);

        if (maid4Scene.Equals(20))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 15);
            //Debug.Log("playing first scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy6)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 15, 115, 215, 8.00f));
            }
        }

        if (maid4Scene.Equals(21))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 16);
            //Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy6)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 16, 116, 216, 7.19f));
            }
        }

        if (maid4Scene.Equals(22))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 17);
            //Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy6)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 17, 117, 217, 8.63f));
            }
        }
    }

    public void DG1Scene()
    {
        int dg1Scene = rnd.Next(24, 27);
        //Debug.Log(hScene[dg1Scene]);

        if (dg1Scene.Equals(24))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);

            anim.SetInteger("hAnim", 18);
            //Debug.Log("playing first scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy7)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 18, 118, 218, 6.50f));
            }
        }

        if (dg1Scene.Equals(25))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            
            anim.SetInteger("hAnim", 19);
            //Debug.Log("playing second scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy7)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 19, 119, 219, 6.00f));
            }
        }

        if (dg1Scene.Equals(26))
        {
            isEngaged = true;
            scenePlayer.transform.position = player.transform.position + new Vector3(0, -1, 0);
            
            anim.SetInteger("hAnim", 20);
            //Debug.Log("playing third scene!");

            if (isEngaged == true)
            {
                player.isEngaged = true;
                foreach(var item in enemy7)
                {
                    item.EnemyEngager();
                }
                
                changeAnimationCoroutine = StartCoroutine(ChangeAnimationPhaseAfterDelay(5, 20, 120, 220, 6f));
            }
        }
    }

}
