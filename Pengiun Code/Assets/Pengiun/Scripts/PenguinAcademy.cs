using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PenguinAcademy : Academy
{
    //summary
    //gets/sets the current fish speed
    //summary

    public float FishSpeed
    {
        get;
        private set;


    }
    //summary
    //gets/sets the current acceptable feed radius
    //summary
    public float FeedRadius
    {
        get;
        private set;
    }
    //summary
    //called when the acdemy first get initialized
    //summary
    public override void InitializeAcademy()
    {
        FishSpeed = 1f;
        FeedRadius = 0f;
        //set-up the code to be called every-time the fish-speed parameter changes
        //during curriculum learning
        FloatProperties.RegisterCallback("fish_speed", f =>
        {
            FishSpeed = f;
        });
        //set-up the code to be called every-time the fish-radius parameter changes
        //during curriculum learning
        FloatProperties.RegisterCallback("fish_radius", f =>
        {
            FeedRadius = f;
        });

    }
}
