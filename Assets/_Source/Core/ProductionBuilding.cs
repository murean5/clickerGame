using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuilding : MonoBehaviour
{
    [SerializeField] private GameResource currentGameResource;
    private const float ProductionTime = 2f;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonPushed);
    }

    private void OnButtonPushed()
    {
        ResourceBank.ChangeResource(currentGameResource, 1);
    }
}