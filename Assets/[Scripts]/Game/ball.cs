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
            door.value++;
            Destroy(transform.GetChild(1).gameObject,1.2f);
            transform.GetChild(1).parent = null;
            Destroy(gameObject);

            if (tw != null) return;
            tw = door.text.transform.DOScale(new Vector3(2.75f, 0.8f, 1.15f), .2f).OnComplete(() =>
            {
                door.text.transform.DOScale(new Vector3(2.1f, 0.6f, 0.85f), .2f);
            });
        }
    }

}
