using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int level;// posição de cena que está no momento
    [Tooltip("List with all scenes of the gamer with it's order")]
    public List<string> scenes;// lista com os nomes das cenas

    private static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
        else Destroy(this.gameObject);
        
        level = LoadLevel();
    }

    // carrega a cena que está na posição level
    public static void LoadScene()
    {
        if(Instance.level < Instance.scenes.Count)
        {
            SceneManager.LoadScene(Instance.scenes[Instance.level]);
        }
        else
        {
            Debug.LogError("no More Scenes to change");
        }
        
    }

    // passa avanca o level em 1 e carrega a cena
    public static void NextScene()
    {
        Instance.level++;
        LoadScene();
    }

    /// <summary>
    /// load the level saved
    /// </summary>
    /// <returns>level's number</returns>
    int LoadLevel()
    {
        return 0;
    }
}
