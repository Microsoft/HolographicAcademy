﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine.VR.WSA;
using HoloToolkit.Unity;
using HoloToolkit.Sharing;

public class HologramPlacement : Singleton<HologramPlacement>
{
    /// <summary>
    /// Tracks if we have been sent a tranform for the anchor model.
    /// The anchor model is rendererd relative to the actual anchor.
    /// </summary>
    public bool GotTransform { get; private set; }

    void Start()
    {
        // Start by making the model as the cursor.
        // So the user can put the hologram where they want.
        GestureManager.Instance.OverrideFocusedObject = this.gameObject;
    }

    void Update()
    {
        if (GotTransform == false)
        {
            transform.position = Vector3.Lerp(transform.position, ProposeTransformPosition(), 0.2f);
        }
    }

    Vector3 ProposeTransformPosition()
    {
        // Put the anchor 2m in front of the user.
        Vector3 retval = Camera.main.transform.position + Camera.main.transform.forward * 2;

        return retval;
    }

    public void OnSelect()
    {
        // Note that we have a tranform.
        GotTransform = true;

        // The user has now placed the hologram.
        // Route input to gazed at holograms.
        GestureManager.Instance.OverrideFocusedObject = null;
    }

    public void ResetStage()
    {
        // We'll use this later.
    }
}