using Laserbean.General.GenericStuff;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TestCompData SO", menuName = "Scriptable Objects/Test")]
public class TestCompDataSO : ComponentsDataScriptableObject<ComponentData>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override Type GetComponentDataType()
    {
        return base.GetComponentDataType(); 
        // return typeof(ComponentData);
    }
}

[Serializable]
public class TestCompData : ComponentData<TestData>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(TestDynamicComp);
    }
}


[Serializable]
public class ComponentData<TTestData> : ComponentData where TTestData : TestData
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(TestDynamicComp);
        
    }
}

[Serializable]
public class TestData
{

}

