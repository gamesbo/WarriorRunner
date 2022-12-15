using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Boss"))
        //{
        //    Haptic.MediumTaptic();
        //    Break();
        //}
    }
    public void Break()
    {
        GameObject frac = Instantiate(Resources.Load("prefabs/blocks"), transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(Random.onUnitSphere * Random.Range(50f, 400f), ForceMode.Force);
        }
    }
}
