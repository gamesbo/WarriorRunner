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
                //other.GetComponent<CharController>().isWater = true;
                //other.GetComponent<CharController>().isFire = false;
                //other.GetComponent<CharController>().levelUP.Play();
                if (value > 0)
                {
                    other.GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 7));
                    skF -= 0.1f;
                    other.GetComponentInChildren<Animator>().SetFloat("SK1", skF);
                    PlayerController.instance.fireRate += 0.02f;
                    if (PlayerController.instance.armorLevel > 0)
                    {
                        PlayerController.instance.armorLevel --;
                    }
                    EKTemplate.UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value -= 0.40f;

                }
                else
                {
                    other.GetComponent<CharController>().isFire = true;
                    other.GetComponent<CharController>().isWater = false;
                    other.GetComponent<CharController>().levelUP.Play();
                    other.GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 7));
                    skF += 0.1f;
                    other.GetComponentInChildren<Animator>().SetFloat("SK1", skF);
                    PlayerController.instance.fireRate -= 0.03f;
                    EKTemplate.UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value += 0.55f;

                }
                //PlayerController.instance.hair.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.hairblue;
                //PlayerController.instance.armor1.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.yelekblue;
                //PlayerController.instance.armor2.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.omuzlukblue;
                //PlayerController.instance.armor3.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.ayakkabýblue;
                //PlayerController.instance.armor3.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.ayakkabýblue;
                //PlayerController.instance.armor4.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.pelerinblue;
                //PlayerController.instance.body.GetComponent<SkinnedMeshRenderer>().materials[0].color = new Color(0.195f, 0.3845834f, 0.65f, 1);
                //PlayerController.instance.body.GetComponent<SkinnedMeshRenderer>().materials[1].color = new Color(0.16f, 0.5866666f, 0.8f, 1);
                //PlayerController.instance.body.GetComponent<SkinnedMeshRenderer>().materials[2].color = new Color(0.33f, 0.646875f, 0.75f, 1);
            }
            else if (isFire)
            {
                if (value > 0)
                {
                    EKTemplate.UIManager.instance.gamePanel.firebar.GetComponent<ProgressBarPro>().Value += 0.55f;
                    other.GetComponent<CharController>().isFire = true;
                    other.GetComponent<CharController>().isWater = false;
                    other.GetComponent<CharController>().levelUP.Play();
                    other.GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 7));
                    skF += 0.1f;
                    other.GetComponentInChildren<Animator>().SetFloat("SK1", skF);
                    PlayerController.instance.fireRate -= 0.03f;
                    PlayerController.instance.armorLevel++;
                    PlayerController.instance.hair.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.hairRed;
                    PlayerController.instance.armor1.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.yelekred;
                    PlayerController.instance.armor2.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.omuzlukred;
                    PlayerController.instance.armor3.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.ayakkabired;
                    PlayerController.instance.armor3.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.ayakkabired;
                    PlayerController.instance.armor4.GetComponent<SkinnedMeshRenderer>().material = PlayerController.instance.pelerinred;
                    PlayerController.instance.body.GetComponent<SkinnedMeshRenderer>().materials[0].color = new Color(0.6509804f, 0.1333333f, 0.1607843f, 1);
                    PlayerController.instance.body.GetComponent<SkinnedMeshRenderer>().materials[1].color = new Color(0.7f, 0.245f, 0.245f, 1);
                    PlayerController.instance.body.GetComponent<SkinnedMeshRenderer>().materials[2].color = new Color(0.65f, 0.2925f, 0.2925f, 1);
                }

            }
        }
    }
}
