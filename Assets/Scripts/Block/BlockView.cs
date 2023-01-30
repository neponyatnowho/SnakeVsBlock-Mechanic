using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Block))]
public class BlockView : MonoBehaviour
{
    [SerializeField] private TMP_Text _viewText;

    private Block _block;

    private void OnEnable()
    {
        _block = GetComponent<Block>();
        _block.FillingUpdated += OnFillingUpdated;
    }

    private void OnDisable()
    {
        _block.FillingUpdated -= OnFillingUpdated;

    }

    private void OnFillingUpdated(int leftToFill)
    {
        _viewText.text = leftToFill.ToString();
    }

}
