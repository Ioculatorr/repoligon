using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public ContainerObject containerObject;

    private int currentIndex = 0;

    void Start()
    {
        // Access the smaller objects in the list
        foreach (SmallerObject smallerObject in containerObject.smallerObjects)
        {
            // Do something with each smaller object, e.g., print its name
            Debug.Log(smallerObject.objectText);
        }
    }

    // Example method to read objects one by one
    public SmallerObject GetNextObject()
    {
        if (currentIndex < containerObject.smallerObjects.Count)
        {
            SmallerObject nextObject = containerObject.smallerObjects[currentIndex];
            currentIndex++;
            return nextObject;
        }
        else
        {
            return null; // All objects have been read
        }
    }
}
