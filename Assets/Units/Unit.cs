using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Ownable
{

    public int maxHitpoints;
    public int currentHitpoints;
    public int firepower;
    public int maxMovementRange;
    public int remainingMovementRange;
    public int length;
    
    // 0: vertical
    // 1: horizontal
    public int orientation = 0;


    public void Turn ()
    {
        if (orientation == 0)
        {
            orientation = 1;
        }
        else
        {
            orientation = 0;
        }
    }
}
