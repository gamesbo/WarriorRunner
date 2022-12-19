using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Door : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int value;
    public Image doorImage;
    public bool isWater = false;
    public bool isFire = false;
    private float skF = 3.5f;
    void Update()
    {
        if (value <= 0)
        {
            doorImage.color = new Color(1, 0.3f, 0.25f, 1);
            text.text = value.ToString();
        }
        else
        {
            doorImage.color = new Color(0.2f, 0.95f, 0.31f, 1);
            text.text = "+" + value.ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWater)
            {
                other.GetComponent<CharController>().isWater = true;
                other.GetComponent<CharController>().isFire = false;
                other.GetComponent<CharController>().levelUP.Play();
                other.GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 5));
                skF += 0.1f;
                other.GetComponentInChildren<Animator>().SetFloat("SK1", skF);
                PlayerController.instance.fireRate -= 0.03f;
                PlayerController.instance.armorLevel ++;
                PlayerController.instance.hair.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.hairBlue;
            }
            else if (isFire)
            {
                other.GetComponent<CharController>().isFire = true;
                other.GetComponent<CharController>().isWater = false;
                other.GetComponent<CharController>().levelUP.Play();
                other.GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 5));
                skF += 0.1f;
                other.GetComponentInChildren<Animator>().SetFloat("SK1", skF);
                PlayerController.instance.fireRate -= 0.03f;
                PlayerController.instance.armorLevel++;
                PlayerController.instance.hair.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.hairRed;

            }
        }
    }
}
