using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;
    [SerializeField] private Color[] _colors;

    private SpriteRenderer _spriteRenderer;
    private int _destroyPrice;
    private int _filling;

    private int LeftToFill => _destroyPrice - _filling;

    public event UnityAction<int> FillingUpdated;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        int chance = Random.Range(0, 4);
        SetCollor(_colors[Random.Range(0, _colors.Length)]);

        if (chance > 1)
            _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);
        else
          _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y / 2);

        FillingUpdated?.Invoke(LeftToFill);
    }

    public void Fill()
    {
        _filling++;
        FillingUpdated?.Invoke(LeftToFill);
        if (_filling == _destroyPrice)
            Destroy(gameObject);
    }
    private void SetCollor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
