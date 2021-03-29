using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject number;
    TextMeshProUGUI tm_number;
    GameObject carousel, left, selected, right;

    private void OnEnable()
    {
        LevelManager.Points += setPoints;
        LevelManager.SelectedChanged += carouselManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        tm_number = number.GetComponentInChildren<TextMeshProUGUI>();

        //Start do carrossel
        
        carousel = this.transform.Find("carousel").gameObject;
        left = carousel.transform.Find("antibody -1").gameObject;
        selected = carousel.transform.Find("antibody 0").gameObject;
        right = carousel.transform.Find("antibody 1").gameObject;

        if (LevelManager.GetAntibodyList().Count == 0)
        {
            carousel.SetActive(false);
        }
        carouselManager();
    }

    // muda o número na HUD quando o personagem perde ou ganha mais anticorpos
    void setPoints()
    {
        tm_number.text = LevelManager.GetAntiBody().ToString("00");
    }

    void carouselManager()
    {
        int selectedAux = LevelManager.GetAntibodySelected();
        List<Color> auxList = LevelManager.GetAntibodyList();
        selected.GetComponent<Image>().color = auxList[selectedAux];

        int indexleft = (selectedAux - 1) % auxList.Count;
        if (indexleft < 0) indexleft = auxList.Count - 1;
        left.GetComponent<Image>().color = auxList[indexleft];
        right.GetComponent<Image>().color = auxList[Mathf.Abs((selectedAux + 1) % auxList.Count)];
    }

    private void OnDisable()
    {
        LevelManager.Points -= setPoints;
        LevelManager.SelectedChanged -= carouselManager;
    }
}
