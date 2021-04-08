using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[AddComponentMenu("Move/Move Things")]
public class MoveThings : MonoBehaviour
{
    public float speed;// velocidade de movimento
    public bool applyRandomRotate;// aplicar rotação aleatória
    public float force;// força que será aplicada
    private Rigidbody2D rb;
    
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.left;

        //aplica rotação caso esteja ligado
        if (applyRandomRotate)
        {
            rb.AddTorque(Random.Range(-force*2, force*2));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // física para movimentar o objeto
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // se destroi quando bate no collider "Destroy"
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
    }
}

//                                 ATENÇÃO 
//(Qualquer parametro adicionado a esse script que deve ser mostrado no editor
// deverá ser adicionado na classe abaixo)


// Ajusta o editor para sumir com a opção "Force" quando o "Add Random Rotate" está desmarcado
[CustomEditor(typeof(MoveThings))]
public class MoveThingsEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var MoveThings = target as MoveThings;

        // Adiciona parâmetros ao editor
        MoveThings.speed = EditorGUILayout.FloatField(new GUIContent("speed","speed of the object"), MoveThings.speed);
        MoveThings.applyRandomRotate = EditorGUILayout.Toggle(new GUIContent("Add Random Rotate","apply a rotation when spawned"), MoveThings.applyRandomRotate);
        if (MoveThings.applyRandomRotate)
            MoveThings.force = EditorGUILayout.Slider(new GUIContent("force", "set the bounds [-force, force] of the rotation speed"), MoveThings.force, 1, 100);

    }
}
