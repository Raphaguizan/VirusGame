using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager Instance;

    private int numberOfAntiBody;// numero atual de anticorpos

    public int qtyToPass;// quantidade de anticorpos necessérios para passar de level

    //chamada quando muda o número / chama quando muda o anticorpo
    public static Action Points, SelectedChanged;

    public List<AntiBody> antibodies;// lista de cores possíveis dos anticorpos
    private int antibodySelected;// anticorpo que está selecionado no momento

    void Start()
    {
        Instance = this;
        numberOfAntiBody = 0;
        antibodySelected = 0;
    }

    // verifica se a pontuação entá entre 0 e 10
    void VerifyLife()
    {
        // ações quando os anticorpos estão abaixo de 0
        if(numberOfAntiBody < 0)
        {
            GameManager.LoadScene();
        }
        // ações quando  os anticorpos atingem o número para passar de level
        else if (numberOfAntiBody >= qtyToPass)
        {
            Debug.Log("ganhou esse level");
            GameManager.NextScene();
        }
    }

    

    /// <summary>
    /// Add a antibody to LevelManager
    /// </summary>
    public static void AddAntiBody()
    {
        Instance.numberOfAntiBody++;
        Instance.VerifyLife();
        Points?.Invoke();
    }

    /// <summary>
    /// Remove a antibody from LevelManager
    /// </summary>
    public static void RemoveAntiBody()
    {
        Instance.numberOfAntiBody--;
        Instance.VerifyLife();
        Points?.Invoke();
    }

    /// <summary>
    /// return the number of antibodies colected
    /// </summary>
    /// <returns>int number</returns>
    public static int GetAntiBody()
    {
        return Instance.numberOfAntiBody;
    }

    /// <summary>
    /// return the list of antibodies color
    /// </summary>
    /// <returns>List<Color></returns>
    public static List<AntiBody> GetAntibodyList()
    {
        return Instance.antibodies;
    }

    /// <summary>
    /// change the antibody power selected
    /// </summary>
    /// <param name="num">index change(-1, 1)</param>
    public static void ChangeAntibody(int num)
    {
        int aux = Instance.antibodySelected + num;
        aux %= Instance.antibodies.Count;
        if (aux < 0) aux = Instance.antibodies.Count -1;
        Instance.antibodySelected = aux;
        SelectedChanged?.Invoke();
    }

    /// <summary>
    /// return the antibody power selected
    /// </summary>
    /// <returns>int</returns>
    public static int GetIndexOfAntibodySelected()
    {
        return Instance.antibodySelected;
    }

    /// <summary>
    /// return the antibody power selected
    /// </summary>
    /// <returns>color</returns>
    public static AntiBody GetAntibodySelected()
    {
        return Instance.antibodies[Instance.antibodySelected];
    }

    /// <summary>
    /// return the antibody power selected
    /// </summary>
    /// <returns>color</returns>
    public static Color GetColorOfType(ColorType ct)
    {
        foreach (AntiBody a in Instance.antibodies)
        {
            if (a.colorType == ct) return a.color;
        }
        return Color.white;
    }
}
