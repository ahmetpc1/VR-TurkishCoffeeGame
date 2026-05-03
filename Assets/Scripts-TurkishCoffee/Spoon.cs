using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spoon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sugar")) 
        {
        GameManager.instance.spoonIngredientType = IngredientType.Sugar;
        }else if (other.CompareTag("Coffee"))
        {
            GameManager.instance.spoonIngredientType = IngredientType.Coffee;

        }

    }
    }
