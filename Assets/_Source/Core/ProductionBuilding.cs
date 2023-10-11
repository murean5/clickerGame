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
    private Button _productionButton;
    private GameManager _gameManager;
    private Image _fillImage;
    private Slider _waitingSlider;

    private void Awake()
    {
        if (Camera.main != null) _gameManager = Camera.main.GetComponent<GameManager>();
        _productionButton = GetComponent<Button>();
        _productionButton.onClick.AddListener(Product);
        _waitingSlider = _productionButton.transform.Find("WaitingSlider").GetComponent<Slider>();
        _fillImage = _waitingSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
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
        if (!_productionButton.interactable)
        {
            _waitingSlider.value -= Time.deltaTime;
            _fillImage.color =
                _gameManager.productionGradientColor.Evaluate(_waitingSlider.value / _waitingSlider.maxValue);
        }
    }

    private IEnumerator AddResource()
    {
        yield return new WaitForSeconds(BaseProductionTime);
        ResourceBank.ChangeResource(currentGameResource, 1);
        _productionButton.interactable = true;
        _waitingSlider.gameObject.SetActive(false);
    }
}