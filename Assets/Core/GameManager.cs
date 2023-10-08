using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        ResourceBank.ChangeResource(GameResource.Humans, 10);
        ResourceBank.ChangeResource(GameResource.Food, 5);
        ResourceBank.ChangeResource(GameResource.Wood, 5);
        ResourceBank.ChangeResource(GameResource.Stone, 0);
        ResourceBank.ChangeResource(GameResource.Gold, 0);
    }
}
