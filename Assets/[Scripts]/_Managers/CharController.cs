using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharController : MonoBehaviour
{
    public bool isFire = false;
    public bool isWater = false;
    public ParticleSystem levelUP;
    #region Singleton
    public static CharController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
}
