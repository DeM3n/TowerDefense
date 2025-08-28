using UnityEngine;
using UnityEngine.Rendering;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    private GameObject tower;
    private Color startColor; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }
    private void OnMouseDown()
    {
         if (tower == null) // chưa có tháp → xây
        {
            BuildTower();
        }
        else
        {
            Debug.Log("Tower already built on this plot!");
            // sau này có thể cho mở menu upgrade/sell ở đây
        }
    }
     private void BuildTower()
    {
        // tạo tower ở vị trí Plot
        GameObject towerToBuild = BuildingManager.main.GetSelectedTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
        
        Debug.Log("Building tower on plot: " + gameObject.name);
    }
    void Start()
    {
        startColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
