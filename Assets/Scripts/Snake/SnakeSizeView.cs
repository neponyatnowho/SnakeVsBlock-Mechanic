using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeSizeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _sizeViewText;
    private Snake _snake;

    private void OnEnable()
    {
        _snake = GetComponent<Snake>();
        _snake.TailSegmentsChanged += ChangeTailSegmentsView;
    }

    private void OnDisable()
    {
        _snake.TailSegmentsChanged -= ChangeTailSegmentsView;
    }

    private void ChangeTailSegmentsView(int count)
    {
        _sizeViewText.text = count.ToString();
    }

}
