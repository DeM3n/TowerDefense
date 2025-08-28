using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private GameObject[] TowerPrefabs;
    private int currentSelectTower = 0;
    public static BuildingManager main;

    private void Awake()
    {
        main = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject GetSelectedTower()
    {
        return TowerPrefabs[currentSelectTower];
    }
}
