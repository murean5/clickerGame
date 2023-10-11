using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuilding : MonoBehaviour
{
    [SerializeField] private GameResource currentGameResource;
    private const float BaseProductionTime = 1.1f;
    private float _totalProductionTime;
    private Button _productionButton;
    private GameManager _gameManager;
    private Image _fillImage;
    private Slider _waitingSlider;
    private Button _lvlUpButton;
    private int _productAmount;

    private Text _amountText;

    private void Awake()
    {
        InitComponents();

        _productionButton.onClick.AddListener(Product);
        _lvlUpButton.onClick.AddListener(LevelUp);
        _productAmount = 1;
        CalculateTotalProductionTime();
    }

    private void InitComponents()
    {
        if (Camera.main != null) _gameManager = Camera.main.GetComponent<GameManager>();
        _productionButton = GetComponent<Button>();
        _amountText = _productionButton.transform.GetComponentInChildren<Text>();
        _waitingSlider = _productionButton.transform.Find("WaitingSlider").GetComponent<Slider>();
        _fillImage = _waitingSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
        _lvlUpButton = _gameManager.footer.Find($"StorePanel/LvlUp{currentGameResource}Button").GetComponent<Button>();
    }

    private void LevelUp()
    {
        _productAmount++;
        _amountText.text = $"+{_productAmount}";
        CalculateTotalProductionTime();
    }

    private void CalculateTotalProductionTime()
    {
        _totalProductionTime = BaseProductionTime + 0.1f * _productAmount;
        _waitingSlider.maxValue = _totalProductionTime;
    }

    private void Product()
    {
        _waitingSlider.value = _waitingSlider.maxValue;
        _productionButton.interactable = false;
        _waitingSlider.gameObject.SetActive(true);
        StartCoroutine(AddResource());
    }

    private void Update()
    {
        if (_productionButton.interactable) return;
        _waitingSlider.value -= Time.deltaTime;
        _fillImage.color =
            _gameManager.productionGradientColor.Evaluate(_waitingSlider.value / _waitingSlider.maxValue);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator AddResource()
    {
        yield return new WaitForSeconds(_totalProductionTime);
        ResourceBank.ChangeResource(currentGameResource, _productAmount);
        _productionButton.interactable = true;
        _waitingSlider.gameObject.SetActive(false);
    }
}