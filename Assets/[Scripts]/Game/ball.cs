using UnityEngine;
using DG.Tweening;
public class ball : MonoBehaviour
{
    private Tween tw;
    void Update()
    {
        transform.Translate(Vector3.forward * 250f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        Door door = other.GetComponent<Door>();

        if (door)
        {
            if (door.isFire)
            {
                door.value++;
               GameObject plus = Instantiate(Resources.Load("plus"),new Vector3(door.transform.position.x + Random.Range(-3f,3f), door.transform.position.y + 1f
                   , door.transform.position.z +1f), Quaternion.identity)as GameObject;
                Destroy(plus.gameObject, 1.5f);
            }
            else
            {
                door.value--;
                GameObject plus = Instantiate(Resources.Load("minus"), new Vector3(door.transform.position.x + Random.Range(-3f, 3f), door.transform.position.y + 1f
              , door.transform.position.z + 1f), Quaternion.identity) as GameObject;
                Destroy(plus.gameObject, 1.5f);
            }
            Destroy(gameObject);

            if (tw != null) return;
            tw = door.text.transform.DOScale(new Vector3(2.75f, 0.8f, 1.15f), .2f).OnComplete(() =>
            {
                door.text.transform.DOScale(new Vector3(2.1f, 0.6f, 0.85f), .2f);
            });
        }
        else if (other.CompareTag("Boss"))
        {
            if (!PlayerController.instance.attack)
            {
                other.GetComponent<Animator>().SetTrigger("Hasar");
            }
            Destroy(gameObject);
        }
    }

}
