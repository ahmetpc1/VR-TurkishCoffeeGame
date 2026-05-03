using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.5f;

    private int count;
    public static float lastTriggerTime = -999f;
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
        string tagName = other.tag;
        switch (tagName)
        {
            case "Water":
                if (Time.time - lastTriggerTime < cooldown)
                    return;

                lastTriggerTime = Time.time;

                GameManager.instance.waterObject.transform.position = GameManager.instance.waterPosition;
                GameManager.instance.currentWater++;
                GameManager.instance.CheckRecpie();

                break;
            case "Spoon":
                if (GameManager.instance.spoonIngredientType == IngredientType.Sugar)
                {
                    GameManager.instance.currentSugar++;

                }
                else if (GameManager.instance.spoonIngredientType == IngredientType.Coffee)
                {
                    GameManager.instance.currentCoffee++;

                }
                GameManager.instance.spoonIngredientType = IngredientType.None;
                GameManager.instance.CheckRecpie();
                break;
            case "Cup":
                if (GameManager.instance.isCoffeeReady) 
                {
                    GameManager.instance.OrderCompleted();
                }
                break;
            
        }
    }
}
