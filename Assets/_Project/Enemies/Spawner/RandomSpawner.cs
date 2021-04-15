using System;
using System.Collections.Generic;
using UnityEngine;


// classe para os objetos a serem inseridos, com sua probabilidade de ser spawnado
[Serializable]
public class ObjectsToSpawn
{
    [Tooltip("game object to spawn")]
    public GameObject gameObject;
    [Tooltip("Color of the enemy (if hematia put NONE)")]
    public ColorType color;
    [Tooltip("probability to spawn")]
    [RangeAttribute(0.0f, 100.0f)] public float probability;
}


public class RandomSpawner : MonoBehaviour
{
    //velocidade do spawn
    [Tooltip("density of spawner")]
    [SerializeField] [Range(0.0f, 10.0f)] float density;

    // limites superior e inferior para spawnar objetos
    [Tooltip("upper spawn limiter")]
    [SerializeField] Transform upBound;
    [Tooltip("botton spawn limiter")]
    [SerializeField] Transform bottonBound;

    // lista dos objetos a serem spawnados
    [Tooltip("list of the objects to spawn")]
    [SerializeField] List<ObjectsToSpawn> objects;

    private float time, timeToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        timeToSpawn = UnityEngine.Random.Range(0.0f, 10 - density);
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        ObjectsToSpawn temp = ChooseObj();
        // quando o tempo de spawn é atingido instancia o objeto selecionado
        if (time >= timeToSpawn)
        {
            GameObject aux = Instantiate(temp.gameObject, new Vector3(this.transform.position.x, UnityEngine.Random.Range(upBound.transform.position.y, bottonBound.transform.position.y), this.transform.position.z), this.transform.rotation);
            aux.GetComponent<Enemy>().Initialize(temp.color);
            time = 0;
            // aleatoriza novamante o tempo para o próximo spawn
            timeToSpawn = UnityEngine.Random.Range(0.0f, 10 - density);
        }
    }

    // Ajusta os sliders no editor para que a soma das probabilidades sempre seja 100%
    void OnValidate()
    {
        if (objects == null) return;
        if (objects.Count == 1) objects[0].probability = 100;
        // move os sliders e muda as variáveis para manter a proporção
        float sum = 0;
        for (int i = 0; i < objects.Count; i++)
        {
            sum += objects[i].probability;
        }
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].probability /= sum;
            objects[i].probability *= 100;
        }
    }

    // escolhe o objeto que será instanciado de acordo com sua probabilidade
    ObjectsToSpawn ChooseObj()
    {
        float random = UnityEngine.Random.value;
        int resp = -1;
        float sum = 0;
        for (int i = 0; i < objects.Count; i++)
        {
            sum += (objects[i].probability/100);
            if (random < sum)
            {
                resp = i;
                break;
            } 
        }
        // caso a lista de objetos estiver vazia, lança uma mensagem de erro
        if (resp == -1) 
        {
            Debug.LogError("Nenhum objeto encontrado!");
            return null;
        }

        // retorna o objeto selecionado
        return objects[resp];
    }
}
