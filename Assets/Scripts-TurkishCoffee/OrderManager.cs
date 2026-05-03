using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    public CoffeRecpie unsweetened = new CoffeRecpie(0,1,1, "Unsweetened");
    public CoffeRecpie mediumSugarnew = new CoffeRecpie(1,1,1, "Medium Sugar");
    public CoffeRecpie sweetnew = new CoffeRecpie(2,1,1, "Sweet");
    public List<CoffeRecpie> coffeRecpies = new List<CoffeRecpie>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        coffeRecpies.Add(unsweetened);
        coffeRecpies.Add(mediumSugarnew);
        coffeRecpies.Add(sweetnew);
    }

    public CoffeRecpie GetRandomRecpie() 
    {
    return coffeRecpies.OrderBy(x => Random.Range(0,10)).First();
    }
    public OrderState GetRecpieState(int currentWaterAmount,int currentSugarAmount,int currentCoffeAmount,CoffeRecpie currentCoffeeRecpie) 
    {
        if (currentCoffeAmount == currentCoffeeRecpie.CoffeeAmount
            && currentWaterAmount == currentCoffeeRecpie.WaterAmount
            && currentSugarAmount == currentCoffeeRecpie.SugarAmount) { return OrderState.Success; }
        if (currentCoffeAmount > currentCoffeeRecpie.CoffeeAmount
            || currentWaterAmount > currentCoffeeRecpie.WaterAmount
            || currentSugarAmount > currentCoffeeRecpie.SugarAmount) { return OrderState.Fail; }

        return OrderState.Continues;
    }
}
public struct CoffeRecpie
{
    public int SugarAmount;
    public int WaterAmount;
    public int CoffeeAmount;

    public string name;
    public CoffeRecpie(int SugarAmount, int WaterAmount, int CoffeeAmount, string name)
    {
        this.SugarAmount = SugarAmount;
        this.WaterAmount = WaterAmount;
        this.CoffeeAmount = CoffeeAmount;
        this.name = name;
    }
}
public enum OrderState 
{
Fail, Success, Continues
}
