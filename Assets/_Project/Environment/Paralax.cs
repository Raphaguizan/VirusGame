using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    float lenght;// tamanho da imagem
    // primeira e segunda a imagem a ser movida(devem ser iguais, porém em posições diferentes)
    [Tooltip("objects to change position")]
    [SerializeField] GameObject first, second;

    [Tooltip("main camera game object")]
    [SerializeField] GameObject cam;// posição da câmera
    [Tooltip("speed of scrolling")]
    [SerializeField] float speed;// velocidade de movimento


    // Start is called before the first frame update
    void Start()
    {
        // pega tamanho da imagem
        lenght = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        second.transform.position = new Vector3(first.transform.position.x + lenght, first.transform.position.y, first.transform.position.z);
    }
    private void FixedUpdate()
    {
        //move as imagens para a esquerda na velocidade especificada
        this.transform.Translate(new Vector2(-speed * Time.fixedDeltaTime, 0));

        // reposiciona uma imagem no final da outra ao sair da tela para manter o efeito de continuidade
        if ((first.transform.position.x + lenght) < cam.transform.position.x)
        {
            first.transform.position = new Vector3(second.transform.position.x + lenght, first.transform.position.y, first.transform.position.z);
        }else if ((second.transform.position.x + lenght) < cam.transform.position.x)
        {
            second.transform.position = new Vector3(first.transform.position.x + lenght, second.transform.position.y, second.transform.position.z);
        }
    }
}
