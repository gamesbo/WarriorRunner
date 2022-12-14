using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EKTemplate;
using DG.Tweening;
public class HumanMid : MonoBehaviour
{
    private bool onTapTap = false;
    public GameObject Boss;
    public int ballPoints = 0;
    private bool firstTap = false;
    private bool secondTap = false;
    private void Update()
    {
        if (onTapTap)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                UIManager.instance.gamePanel.ibre.anchoredPosition += Vector2.right * 32f;
                if (!secondTap)
                {
                    firstTap = true;
                    secondTap = true;
                }
            }
            if (firstTap)
            {
                ParticleSystem[] particle = Boss.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particle.Length; i++)
                {
                    particle[i].Play();
                }
                ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particles.Length; i++)
                {
                    particles[i].Play();
                }
                transform.GetComponent<Animator>().SetBool("Attack", true);
                Boss.GetComponent<Animator>().SetBool("Attack", true);

                firstTap = false;
            }

            UIManager.instance.gamePanel.ibre.anchoredPosition -= Vector2.right * Time.deltaTime * 90f;

            if (UIManager.instance.gamePanel.ibre.anchoredPosition.x >= 240f) Fight();
            else if (UIManager.instance.gamePanel.ibre.anchoredPosition.x <= -240f) Fail();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DragonBall"))
        {
            transform.GetChild(1).GetComponentInChildren<ParticleSystem>().Play();
            ballPoints += 5;
            Haptic.HeavyTaptic();
            transform.GetComponentInChildren<ProgressBarPro>().Value += 0.09f;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Coin"))
        {
            transform.GetChild(1).GetComponentInChildren<ParticleSystem>().Play();
            Haptic.HeavyTaptic();
            Destroy(other.gameObject);
        }   
        else if (other.CompareTag("Fence"))
        {
            float deger = 0f;
            float y = PlayerController.instance.sF.motion.offset.y;
            DOTween.To(x =>
            {
                deger = x;
                PlayerController.instance.sF.motion.offset = new Vector2(0, y + deger);
            }, deger, 2.7f, 0.1f);
        }
        else if (other.CompareTag("Final"))
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                transform.DOMoveX(0, 0.5f).SetEase(Ease.Linear);             
                yield return new WaitForSeconds(1.05f);
                PlayerController.instance.canMove = false;
                transform.GetComponent<Animator>().SetBool("idle", true);
                transform.GetChild(0).gameObject.SetActive(false);
                CameraManager.instance.transform.GetComponent<Animator>().enabled = true;
                CameraManager.instance.enabled = false;
                UIManager.instance.gamePanel.ibre.parent.gameObject.SetActive(true);
                onTapTap = true;
            }
        }
    }
    public void Fail()
    {
        onTapTap = false;
        WinLoseController.instance.lose = true;
        UIManager.instance.gamePanel.ibre.parent.gameObject.SetActive(false);
        Boss.GetComponent<Animator>().SetBool("Fun", true);
    }
    public void Fight()
    {
        onTapTap = false;
        UIManager.instance.gamePanel.ibre.parent.gameObject.SetActive(false);
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3.3f);
            Boss.GetComponent<Animator>().SetBool("Death", true);
            //BossForce();
        }
    }
    //public void BossForce()
    //{
    //    CameraManager.instance.enabled = true;
    //    CameraManager.instance.transform.GetComponent<Animator>().enabled = false;
    //    CameraManager.instance.mzdistance = 50f;
    //    CameraManager.instance.mxdistance = -60f;
    //    CameraManager.instance.mydistance = -15f;
    //    CameraManager.instance.positionTarget = Boss.transform;
    //    CameraManager.instance.transform.DORotate(new Vector3(CameraManager.instance.transform.rotation.x, -34f,
    //    CameraManager.instance.transform.rotation.z), 0.45f);
    //    Boss.transform
    //        .DOMoveZ(Boss.transform.transform.position.z + (ballPoints * 2.25f * GameManager.instance.pushFactors[GameManager.instance.powerLevel]), 2.25f)
    //        .SetEase(Ease.InSine)
    //        .OnUpdate(() => Haptic.SelectionHaptic())
    //        .OnComplete(() => WinLoseController.instance.win = true);
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fence"))
        {
            float deger = 0f;
            float y = PlayerController.instance.sF.motion.offset.y;
            DOTween.To(x =>
            {
                deger = x;
                PlayerController.instance.sF.motion.offset = new Vector2(0, y + deger);
            }, deger, -2.7f, 0.1f);
        }
    }
}
