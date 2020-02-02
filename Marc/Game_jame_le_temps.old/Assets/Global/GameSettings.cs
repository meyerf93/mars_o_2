using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings {

    public static int ForeuseCost = 101;
    public static int BoosterCost = 102;
    public static int AnalyserCost = 103;
    public static int ExtraFuelCost = 104;

    public static int MovementBase = 1; // conso de base d'un mouvement
    public static float AutoManualRatio = 2.0f; // facteur de surconsommation du mode manuel
    public static float TimeConso = 1f;
    public static float DoubleJumpDelay = 0.75f;

    public static int ForeuseBase = 25;
    public static int ForeuseLevelDif = -5;

    public static int BoosterBase = 40;
    public static int BoosterLevelDif = -10;

    public static int AnalyserBase = 25;
    public static int AnalyserLevelDif = -5;

    public static int AnalyseInterestPoint = 1;
    public static int WaterInterestPoint = 5;

    public static int ExtraFuelBase = 100; // 100 + Que Fuel Tank de base
    public static int ExtraFuelLevelDif = 50;

}
