using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contiene los diferentes puntos de inicio y teletransporte de una escena a otra,
/// esto para mejor comodidad al ponerlos en el inspector de Unity
/// </summary>
public enum StartAndTeleportPointsID
{
    //todo: Se debe cambiar la forma en que esto trabaja, ya que si se agrega un nivel...
    // en medio de otros, el orden se trastoca y entonces tendria que cambiar todo el juego
    // Puede ser una lista o un diccionario, no se
    nothing,
    gameStart,

    // Greed1
    greed1_Greed2,
    greed1_Greed4,
    greed1_Greed5,

    // Greed2
    greed2_Greed1,
    greed2_Greed3,

    // Greed3
    greed3_Greed2,

    // Greed4
    greed4_Greed1,

    // Greed5
    greed5_Greed1,
    greed5_Greed3,
}
