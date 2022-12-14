using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EKTemplate;
public class WinLoseController : MonoBehaviour
{
    public bool win = false;
    public bool lose = false;
    #region Singleton
    public static WinLoseController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    private void Update()
    {
        if (lose == true)
        {
            Lose();
            lose = false;
        }
        else if (win == true)
        {
            Win();
            win = false;
        }
    }
    private void Start()
    {
        win = false;
        lose = false;
    }
    public void Win()
    {
        StartCoroutine(WinDelay());
    }
    IEnumerator WinDelay()
    {       
        yield return new WaitForSeconds(0.1f);
        LevelManager.instance.Success();
        Haptic.NotificationSuccessTaptic();
    }
    public void Lose()
    {
        StartCoroutine(LoseDelay());
    }
    IEnumerator LoseDelay()
    {
        LevelManager.instance.Fail();
        Haptic.NotificationErrorTaptic();
        yield return new WaitForSeconds(0.1f);
    }
}
