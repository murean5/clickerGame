using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResourceVisual : MonoBehaviour
{
    [FormerlySerializedAs("_currentGameResource")] [SerializeField]
    private GameResource currentGameResource;

    private ObservableInt _currentCounter;
    private Text _gameResourceAmountText;

    private void Awake()
    {
        _gameResourceAmountText = GetComponent<Text>();
        _currentCounter = ResourceBank.GetResource(currentGameResource);
        _gameResourceAmountText.text = _currentCounter.Value.ToString();
    }

    private void Update()
    {
        _currentCounter.OnValueChanged =
            value => _gameResourceAmountText.text = value.ToString();
    }
}