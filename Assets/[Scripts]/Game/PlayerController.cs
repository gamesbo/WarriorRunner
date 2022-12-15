using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using EKTemplate;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static int coinValue = 1;
    public float speedstrech = 20f;
    public Transform MidPlayer;
    public float runSpeed = 10f;
    public Transform spawnPos;
    [HideInInspector] public SplineFollower sF;
    [HideInInspector] public bool canMove = false;
    [HideInInspector] public bool isDead = false;
    public bool isSwerve = false;

    public bool onLeft = false;
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
    private void OnGameStart()
    {
        canMove=true;
        transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Run");
        transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk"+Random.Range(0,7));
    }
    public void Shoot()
    {
        Instantiate(Resources.Load("Fireball"),spawnPos.position , Quaternion.identity);
    }
    private void Update()
    {
        if (canMove == true)
        {
            sF.follow = true;
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
            sF.follow = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        //if (other.CompareTag("FusionEnd"))
        //{
        //    float deger = 0f;
        //    float y = sF.motion.offset.y;
        //    DOTween.To(x =>
        //    {
        //        deger = x;
        //        sF.motion.offset = new Vector2(0, y + deger);
        //    }, deger, -4.4f, 0.3f);
        //}
    }
 
   
    
}