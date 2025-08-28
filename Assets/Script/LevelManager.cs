using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform StartPoint;
    public Transform[] path;

    public int currency;
    private void Awake()
    {
        main = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currency = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IncreaseCurrency(int amount)
    {
        currency += amount;

    }
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("not enough money");
            return false;
        }
    }
}
