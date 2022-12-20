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
        yield return new WaitForSeconds(0.5f);
        LevelManager.instance.Success();
        Haptic.NotificationSuccessTaptic();
        PlayerController.instance.Boss.GetComponent<Animator>().SetTrigger("Defeat");
        PlayerController.instance.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Victory");

    }
    public void Lose()
    {
        StartCoroutine(LoseDelay());
    }
    IEnumerator LoseDelay()
    {
        yield return new WaitForSeconds(0.5f);

        LevelManager.instance.Fail();
        PlayerController.instance.Boss.GetComponent<Animator>().SetTrigger("Victory");
        PlayerController.instance.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Defeat");
        Haptic.NotificationErrorTaptic();
        yield return new WaitForSeconds(0.1f);
    }
}
