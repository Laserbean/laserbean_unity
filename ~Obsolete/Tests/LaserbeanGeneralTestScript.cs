// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;

// using Laserbean.General;


// public class LaserbeanGeneralTestScript
// {
//     // A Test behaves as an ordinary method
//     [Test]
//     public void TestSaveClassJson()
//     {
//         const float num1 = 0.12345f; 
//         const float num2 = 12345.6789f; 
//         FishTest fishtest = new (); 

//         fishtest.fishparts.Add(new (num1));
//         fishtest.fishparts.Add(new (num2, FishPartTest.FishType.B));

//         var strin = SaveAnything.ToJson(fishtest); 
        
//         Debug.Log(strin);


//         FishTest chicken = SaveAnything.FromJson<FishTest>(strin);

              

//         Assert.IsTrue(chicken.fishparts[0].number == num1, "num1 failed" + chicken.fishparts[0].number);
//         Assert.IsTrue(chicken.fishparts[1].number == num2, "num2 failed" + chicken.fishparts[1].number);
//     }

//     // // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
//     // // // `yield return null;` to skip a frame.
//     // // [UnityTest]
//     // // public IEnumerator LaserbeanGeneralTestScriptWithEnumeratorPasses()
//     // // {
//     // //     // Use the Assert class to test conditions.
//     // //     // Use yield to skip a frame.
//     // //     yield return null;
//     // // }


//     [System.Serializable]
//     public class FishTest {
//         public string [] fish =  {"fish", "chicken", "cat"};
//         public List<FishPartTest> fishparts = new (); 
        

//     }

//     [System.Serializable]
//     public class FishPartTest {
//         public enum FishType {
//             A, B, C
//         }
//         public float number = 0;

//         public FishPartTest(float num, FishType ttt = FishType.A) {
//             number = num; 
//         }
//     }
// }
