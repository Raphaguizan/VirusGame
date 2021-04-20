using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] string text;
    bool talking;
    TextMeshProUGUI line;
    // Start is called before the first frame update
    void Start()
    {

        line = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
       
    }

    private void OnEnable()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTalk()
    {
        talking = true;
        StartCoroutine(Talking());
    }

    IEnumerator Talking()
    {
        char[] letters = text.ToCharArray();
        int i = 0;
        int max = letters.Length;

        while(talking)
        {
            if(i < max)
            {
                line.text += letters[i].ToString();
                i++;
            }
            else
                talking = false;

            yield return new WaitForSeconds(.01f);
        }

    }
}
