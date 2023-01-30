using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeHead : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenDestroying;
    [SerializeField] private ParticleSystem _deadSegmentParticle;


    public event UnityAction BlockCollided;
    public event UnityAction<int> BonusCollected;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block))
            StartCoroutine(OnBlockHit(block, _secondsBetweenDestroying));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block))
            StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bonus bonus))
        {
            BonusCollected?.Invoke(bonus.Collect());
        }    
    }

    private IEnumerator OnBlockHit(Block block, float wait)
    {
        while(true)
        {
            block.Fill();
            BlockCollided?.Invoke();
            Instantiate(_deadSegmentParticle, transform);
            Vibration.Vibrate(50);
            yield return new WaitForSeconds(wait);
        }
    }
}
