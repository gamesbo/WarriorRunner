using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using EKTemplate;
using DG.Tweening;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public bool _Creative = false;
    public static int coinValue = 1;
    public float speedstrech = 20f;
    public Transform MidPlayer;
    public float runSpeed = 10f;
    public Material hairRed,hairblue;
    public Material ayakkabired,ayakkabiblue;
    public Material omuzlukred,omuzlukblue;
    public Material pantolonred,pantolonblue;
    public Material pelerinred,pelerinblue;
    public Material tshirtred,tshirtblue;
    public Material yelekred, yelekblue;
    public GameObject hair;
    public GameObject body;
    public Transform spawnPos;
    public GameObject armor1, armor2, armor3, armor4;
    [HideInInspector] public SplineFollower sF;
    [HideInInspector] public bool canMove = false;
    [HideInInspector] public bool isDead = false;
    public bool isSwerve = false;
    public float fireRate = 0.6f;
    public bool onLeft = false;
    public GameObject fusionParticle;
    public Sprite[] barColors;
    #region Singleton
    public static PlayerController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    private void Start()
    {
        sF = GetComponent<SplineFollower>();
        LevelManager.instance.startEvent.AddListener(OnGameStart);
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(delay());
    }
    private void OnGameStart()
    {
        canMove=true;
        StartCoroutine(delay());
        transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Run");
        transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk"+Random.Range(1,7));
    }
    public int armorLevel = 0;
    public int charnameLevel = 0;
    public void CharacterArmors()
    {
        switch (armorLevel)
        {
            case 0:
                armor1.SetActive(false);
                armor2.SetActive(false);
                armor3.SetActive(false);
                armor4.SetActive(false);

                return;
            case 1:
                armor1.SetActive(true);
                armor2.SetActive(false);
                armor3.SetActive(false);
                armor4.SetActive(false);

                return;
            case 2:
                armor2.SetActive(true);
                armor1.SetActive(true);
                armor3.SetActive(false);
                armor4.SetActive(false);

                return;
            case 3:
                armor1.SetActive(true);
                armor2.SetActive(true);
                armor3.SetActive(true);
                armor4.SetActive(false);

                return;
            case 4:
                armor1.SetActive(true);
                armor2.SetActive(true);
                armor3.SetActive(true);
                armor4.SetActive(true);
                return;
        }
    }
    public void ChangeScaleBarText(int _i)
    {
        UIManager.instance.gamePanel.textChar.transform.DOScale(1.5f, .3f).OnComplete(() =>
        {
            UIManager.instance.gamePanel.textChar.transform.DOScale(Vector3.one, .3f);
        });
        UIManager.instance.gamePanel.barColor.sprite = barColors[_i];
    }
    public void CharacterNames()
    {
        switch (charnameLevel)
        {
            case 0:
                UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value = 0;
                UIManager.instance.gamePanel.textChar.text = "Warrior".ToString();
                StartCoroutine(BarDelay2(0));

                return;
            case 1:
                UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value = 0;
                UIManager.instance.gamePanel.textChar.text = "Fighter".ToString();
                StartCoroutine(BarDelay2(1));

                return;
            case 2:
                UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value = 0;
                UIManager.instance.gamePanel.textChar.text = "Ninja".ToString();
                StartCoroutine(BarDelay2(2));

                return;
            case 3:
                UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value = 0;
                UIManager.instance.gamePanel.textChar.text = "Supreme".ToString();
                StartCoroutine(BarDelay2(3));

                return;
            case 4:
                UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value = 0;
                UIManager.instance.gamePanel.textChar.text = "Combatant".ToString();
                StartCoroutine(BarDelay2(4));

                return;
            case 5:
                UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value = 0;

                UIManager.instance.gamePanel.textChar.text = "Sergant".ToString();
                StartCoroutine(BarDelay2(5));

                return;
            case 6:
                UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value = 0;
                UIManager.instance.gamePanel.textChar.text = "Martial".ToString();
                StartCoroutine(BarDelay2(6));
                return;
        }
    }
    IEnumerator BarDelay2(int _i)
    {
        yield return new WaitForSeconds(0.35f);
        ChangeScaleBarText(_i);

    }
    public bool onTapTap = false;
    public GameObject Boss;
    public bool firstTap = false;
    private bool secondTap = false;

    IEnumerator BarDelay()
    {
        yield return new WaitForSeconds(1);
        charnameLevel++;
        CharacterNames();
        checkBar = false;
    }
    bool checkBar = false;
    private void Update()
    {
        if (!checkBar)
        {
            if(UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value >= 1)
            {
                StartCoroutine(BarDelay());
                checkBar = true;
            }
        }
        CharacterArmors();
        if (canMove == true)
        {
            if (!_Creative)
            {
                if (!isSwerve)
                {
                    Vector3 temp = MidPlayer.localPosition;
                    temp.x += InputManager.instance.input.x * Time.deltaTime * speedstrech;
                    temp.x = Mathf.Clamp(temp.x, -11f, 11f);
                    MidPlayer.localPosition = temp;

                    if (InputManager.instance.input.x > 0f && Input.GetMouseButton(0))
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, 45, -0), 0.4f).SetEase(Ease.Linear);
                    }
                    else
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                    }
                    if (InputManager.instance.input.x < 0f && Input.GetMouseButton(0))
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, -30, 0), 0.4f).SetEase(Ease.Linear);
                    }
                    else
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                    }
                }
            }
            else
            {
                Vector3 temp = MidPlayer.localPosition;
                temp.x -= InputManager.instance.input.x * Time.deltaTime * speedstrech;
                temp.x = Mathf.Clamp(temp.x, -11f, 11f);
                MidPlayer.localPosition = temp;

                if (InputManager.instance.input.x > 0f && Input.GetMouseButton(0))
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, -45, -0), 0.4f).SetEase(Ease.Linear);
                }
                else
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                }
                if (InputManager.instance.input.x < 0f && Input.GetMouseButton(0))
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, 30, 0), 0.4f).SetEase(Ease.Linear);
                }
                else
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                }
            }
            sF.follow = true;
        }
        else
        {
            sF.follow = false;
        }
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
              
                firstTap = false;
            }

            UIManager.instance.gamePanel.ibre.anchoredPosition -= Vector2.right * Time.deltaTime * 90f;

            if (UIManager.instance.gamePanel.ibre.anchoredPosition.x >= 240f) Fight();
            else if (UIManager.instance.gamePanel.ibre.anchoredPosition.x <= -240f) Fail();
        }
    }
    public void Fail()
    {
        onTapTap = false;
        stopattack = true;
        WinLoseController.instance.Lose();
        UIManager.instance.gamePanel.ibre.parent.gameObject.SetActive(false);
    }
    public void Fight()
    {
        onTapTap = false;

        UIManager.instance.gamePanel.ibre.parent.gameObject.SetActive(false);
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            attack = true;
            transform.GetChild(0).GetComponentInChildren<Animator>().SetFloat("SK1", 3f);
            transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 7));
            yield return new WaitForSeconds(2f);
            transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 7));
            yield return new WaitForSeconds(2);
            transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 7));
            yield return new WaitForSeconds(2);
            transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 7));
            yield return new WaitForSeconds(2f);
            stopattack = true;
            WinLoseController.instance.Win();
            fusionParticle.SetActive(true);
        }
    }
    private bool stopattack = false;
    public bool attack = false;
    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(2f);
        if (stopattack) yield break;
        Boss.GetComponent<Animator>().SetTrigger("Attack" + Random.Range(0, 4));
        StartCoroutine(BossAttack());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            fireRate -= 0.035f;
            UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value += 0.15f;
            Destroy(other.gameObject);

        }
        else if (other.CompareTag("Trap"))
        {
            StartCoroutine(Delay());
            transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Trap");
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(0.75f);
                transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 5));
            }
        }
        else if (other.CompareTag("Final"))
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                UIManager.instance.gamePanel.firebar.SetActive(false);
                UIManager.instance.gamePanel.img.SetActive(false);
                transform.DOMoveX(0, 0.5f).SetEase(Ease.Linear);
                yield return new WaitForSeconds(0.7f);
                transform.GetChild(0).GetComponentInChildren<Animator>().SetFloat("SK1", 3f);
                canMove = false;
                StartCoroutine(BossAttack());
                if (_Creative)
                {
                    CameraManager.instance.offsetvector = new Vector3(62.5f, 70f, -32.5f);
                }
                else
                {
                    CameraManager.instance.offsetvector = new Vector3(40f, 50f, -15f);
                }
                CameraManager.instance.transform.DORotate(new Vector3(40f, -45f, 0), 0.5f);
                transform.GetChild(0).GetComponent<Animator>().SetBool("idle", true);
                UIManager.instance.gamePanel.ibre.parent.gameObject.SetActive(true);
                onTapTap = true;
                attack = true;
            }
        }
        else if (other.CompareTag("WaterBall"))
        {
            fireRate += 0.02f;
            Destroy(other.gameObject);
            UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value -= 0.15f;

        }
    }
}