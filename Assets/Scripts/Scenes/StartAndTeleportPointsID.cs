using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contiene los diferentes puntos de inicio y teletransporte de una escena a otra,
/// esto para mejor comodidad al ponerlos en el inspector de Unity
/// </summary>
public enum StartAndTeleportPointsID
{
    nothing = 0,
    gameStart = 1,
    forestFromCaves = 2,
    cavesFromForest = 3,
    hell1_1FromCaves = 4,
    CavesFromHell1_1 = 5
}
