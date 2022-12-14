using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
public class SplineRebuild : MonoBehaviour
{
    void Start()
    {
        SplineComputer[] splines = FindObjectsOfType<SplineComputer>();
        for (int i = 0; i < splines.Length; i++)
        {
            splines[i].RebuildImmediate();
        }
    }

}
