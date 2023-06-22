using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BrickData", menuName = "ScriptableObjects/BrickData", order = 1)]
public class BrickData : ScriptableObject
{
   public enum BrickType
   {
      A,
      B,
      C,
      D
   }
   public BrickType brickType;
   public int brickHealth;
}
