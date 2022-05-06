using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    public bool isDead;
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;


    public override void Init(Core core)
    {
        base.Init(core);

        Stats.HealthZero += Die;
    }

    private void OnEnable(){
        Stats.HealthZero += Die;
    }

    private void OnDisable(){
        Stats.HealthZero -= Die;
    }

    public void Die()
    {
        isDead = true;
        //core.transform.parent.gameObject.SetActive(false);
    }
}
