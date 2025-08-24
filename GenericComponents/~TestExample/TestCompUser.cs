using Laserbean.General.GenericStuff;
using UnityEngine;

public class TestCompUser : MonoBehaviour, IComponentUser
{

    [EasyButtons.Button]
    public void GenerateComponents()
    {
        GetComponent<TestCompGen>().Generate_Components(); 
    }

    public void SetComponentData(ComponentsDataScriptableObject data)
    {
        throw new System.NotImplementedException();
    }

    public void SetIsUsable(bool val)
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
