using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static int playerInitHp = 3;

    public static float playerInitMoveSpeed = 10f;

    public static float screenWidthX = 6f;

    public static int cardListLength = 3;

    public enum CardType{
        CardType1 = 1,
        CardType2,
    }

    public enum CardEffectType{
        effectType1 = 1,
        effectType2,
        effectType3,
        effectType4
    }
}
