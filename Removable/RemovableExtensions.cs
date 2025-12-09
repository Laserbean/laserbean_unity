using UnityEngine;
namespace Laserbean.Removable
{
    public static class RemovableExtensions
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        public static void Remove(this GameObject gameOBJ)
        {
            gameOBJ.GetComponent<IRemovable>().Remove();
        }
    }
}
