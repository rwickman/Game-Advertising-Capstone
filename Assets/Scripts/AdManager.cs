using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used in conjunction with CollectableManager to indicate which type of bottle caps to spawn.
/// </summary>
public class AdManager : MonoBehaviour
{
    private bool useGeneric;

    public void ToggleUseGeneric()
    {
        useGeneric = !useGeneric;
    }

    public bool GetUseGeneric()
    {
        return useGeneric;
    }
}
