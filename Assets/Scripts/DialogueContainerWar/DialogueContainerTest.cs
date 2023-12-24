using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ContainerObject", menuName = "Custom/ContainerObject", order = 1)]
public class ContainerObject : ScriptableObject
{
    public List<SmallerObject> smallerObjects = new List<SmallerObject>();
}

