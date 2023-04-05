using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MashBar : MonoBehaviour
{
    private PlayerInputHandler playerInputHandler;
    public event Action<float> OnMashChange;

    private bool MashInput;
    private bool MashInputStop;

    [SerializeField]public int maxMash;
    public int currentMash;
    public int minMash = 0;
    public Slider slider;
    public MashBar mashBar;

    private int amountOfClicksLeft;
    private WaitForSeconds decayTick = new WaitForSeconds(0.01f);
    private Coroutine decay;


    private void Start() 
    {
        playerInputHandler = FindObjectOfType<PlayerInputHandler>();

        amountOfClicksLeft = 1;

        currentMash = 0;
        SetMash(0);   
        //SetMaxMash(maxMash);

    }

    private void Update()
    {
        MashInput = playerInputHandler.MashInput;
        MashInputStop = playerInputHandler.MashInputStop;

        if (MashInput && amountOfClicksLeft == 1)
        {
            //Debug.Log("mash added");
            amountOfClicksLeft = 0;
            IncreaseMash(8);
            StopCoroutine(decay);

            
        }
        if (MashInputStop && amountOfClicksLeft < 1)
        {
            amountOfClicksLeft = 1;

            decay = StartCoroutine(DecayMash());
        }
    }

    public void SetMash(int mash)
    {
        slider.value = mash;
    }

    //public void SetMaxMash(int mash)
    //{
    //    slider.maxValue = mash;
    //    slider.value = 0;
    //}

    public void IncreaseMash(int mash)
    {
        //currentMash += mash;
        currentMash = Mathf.Clamp(currentMash + mash, 0, maxMash);
        OnMashChange?.Invoke(currentMash);

        //SetMash(currentMash);
    }

    public void DecreaseMash(int mash)
    {
        currentMash -= mash;
        //SetMash(currentMash);
        OnMashChange?.Invoke(currentMash);

        if(currentMash <= 0)
        {
            currentMash = 0;
            //Debug.Log("Mash is Min!");

            if(decay != null)
                StopCoroutine(decay);

            decay = StartCoroutine(DecayMash());
            
        }

        if(currentMash > 0)
        {
            decay = StartCoroutine(DecayMash());
        }
    }

    private IEnumerator DecayMash()
    {
        //yield return new WaitForSeconds(0.1f);

        while(currentMash > minMash)
        {
            currentMash -= 1;
            mashBar.slider.value = currentMash;
            yield return decayTick;
        }
        decay = null;
    }
}
