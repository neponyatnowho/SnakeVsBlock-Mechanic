using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;


[RequireComponent(typeof(TailGenerator))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _tailSize;


    private List<Segment> _tail;
    private TailGenerator _tailGenerator;

    public List<Segment> Tail => _tail;
    public event UnityAction<int> TailSegmentsChanged;

    private void OnEnable()
    {
        _head.BlockCollided += OnBlockColided;
        _head.BonusCollected += OnBonusCollected;
    }

    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockColided;
        _head.BonusCollected -= OnBonusCollected;
    }
    private void Awake()
    {
        _tailGenerator = GetComponent<TailGenerator>();
        _tail = _tailGenerator.Generate(_tailSize);
    }

    private void Start()
    {
        TailSegmentsChanged?.Invoke(_tail.Count + 1);
    }
    private void OnBlockColided()
    {
        if(_tail.Count > 0)
        {
            Segment deletedSegment = _tail[0];
            _tail.Remove(deletedSegment);
            Destroy(deletedSegment.gameObject);
            TailSegmentsChanged?.Invoke(_tail.Count + 1);
        }
        else
        {
            Destroy(gameObject);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void OnBonusCollected(int bonusSize)
    {
        Vibration.Vibrate(100);
        _tail.AddRange(_tailGenerator.Generate(bonusSize));
        TailSegmentsChanged?.Invoke(_tail.Count + 1);
    }
}
