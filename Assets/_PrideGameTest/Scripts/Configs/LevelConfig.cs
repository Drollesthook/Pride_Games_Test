using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Config/LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject {
   public int FieldSize;
   public int MaximumAmountOfObjects;
}
