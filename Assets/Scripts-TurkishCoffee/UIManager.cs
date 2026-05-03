using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TMP_Text CoffeText;
    [SerializeField] TMP_Text WaterText;
    [SerializeField] TMP_Text SugarText;
    [SerializeField] TMP_Text HeaderText;
    [SerializeField] TMP_Text ScoreText;

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
        RefreshScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RefreshRecpieText() 
    {
        HeaderText.text = GameManager.instance.currentRecpie.name;
        CoffeText.text = $"Coffee {GameManager.instance.currentCoffee}/{GameManager.instance.currentRecpie.CoffeeAmount}";
        WaterText.text = $"Water {GameManager.instance.currentWater}/{GameManager.instance.currentRecpie.WaterAmount}";
        SugarText.text = $"Sugar {GameManager.instance.currentSugar}/{GameManager.instance.currentRecpie.SugarAmount}";
    }
    public void RefreshScoreText() 
    {
        ScoreText.text = $"Score: {GameManager.instance.score}";
    }
}
