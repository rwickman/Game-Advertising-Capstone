using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
