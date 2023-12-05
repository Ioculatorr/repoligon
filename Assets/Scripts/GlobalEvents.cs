using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GlobalEvents
{
    public static event Action GameLoadedFromSave;
    public static void OnGameLoadedFromSave() => GameLoadedFromSave?.Invoke();


    public static event Action<CustomEventArguments> GameLoadingProgressed;
    public static void OnGameLoadingProgressed(CustomEventArguments arguments) => GameLoadingProgressed?.Invoke(arguments);
} 


 
public class CustomEventArguments : EventArgs
{
    public float Progress;
}
