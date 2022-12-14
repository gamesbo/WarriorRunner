using UnityEngine;

public class TrapRoll : MonoBehaviour
{
    public bool y = false;
    void Update()
    {
        if (y)
        {
            transform.Rotate(new Vector3(0, 250f, 0)*Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(300f, 0f, 0) * Time.deltaTime);
        }
    }
}
