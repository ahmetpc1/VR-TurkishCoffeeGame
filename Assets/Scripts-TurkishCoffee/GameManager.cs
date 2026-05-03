using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum IngredientType { Sugar, Coffee, None }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CoffeRecpie currentRecpie;
    public int currentWater = 0;
    public int currentSugar = 0;
    public int currentCoffee = 0;

    public int score = 0;
    public float brewTime = 6f;
    public IngredientType spoonIngredientType = IngredientType.None;

    public bool isCoffeeReady = false;

    public GameObject sugarObject;
    public GameObject coffeeObject;
    public GameObject waterObject;
    public GameObject spoonObject;
    public GameObject potObject;

    Vector3 sugarPosition;
    Vector3 coffeePosition;
    public Vector3 waterPosition;
    Vector3 spoonPosition;
    Vector3 potPosition;

    public ParticleSystem fire;
    bool isBrewing = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        fire.Stop();

        sugarPosition = sugarObject.transform.position;
        coffeePosition = coffeeObject.transform.position;
        waterPosition = waterObject.transform.position;
        spoonPosition = spoonObject.transform.position;
        potPosition = potObject.transform.position;

        NewRecpie();
    }

    public void CheckRecpie()
    {
        if (isBrewing)
            return;

        OrderState orderState = OrderManager.instance.GetRecpieState(currentWater, currentSugar, currentCoffee, currentRecpie);

        switch (orderState)
        {
            case OrderState.Fail:
                score--;
                UIManager.instance.RefreshScoreText();
                NewRecpie();
                break;

            case OrderState.Success:
                BrewCoffe();
                break;

            case OrderState.Continues:
                break;
        }

        UIManager.instance.RefreshRecpieText();
        
    }

    void BrewCoffe()
    {
        fire.Play();
        isBrewing = true;
        Invoke("CoffeeReady", brewTime);
    }

    void CoffeeReady()
    {
        fire.Stop();
        isBrewing = false;

        isCoffeeReady = true;
    }

    public void OrderCompleted()
    {
        score++;
        UIManager.instance.RefreshScoreText();
        NewRecpie();
    }
    void ReleasePotIfGrabbed()
    {
        XRGrabInteractable grab = potObject.GetComponent<XRGrabInteractable>();

        if (grab == null)
            return;

        if (!grab.isSelected)
            return;

        IXRSelectInteractor interactor = grab.firstInteractorSelecting;

        if (interactor != null)
        {
            grab.interactionManager.SelectExit(interactor, grab);
        }
    }
    public void NewRecpie()
    {
        ReleasePotIfGrabbed();
        potObject.transform.position = potPosition;
        waterObject.transform.position = waterPosition;
        coffeeObject.transform.position = coffeePosition;
        sugarObject.transform.position = sugarPosition;
        spoonObject.transform.position = spoonPosition;

        isCoffeeReady = false;
        currentWater = 0;
        currentSugar = 0;
        currentCoffee = 0;

        Pot.lastTriggerTime = -999;

        currentRecpie = OrderManager.instance.GetRandomRecpie();
        UIManager.instance.RefreshRecpieText();
    }

    void Update()
    {

    }
}