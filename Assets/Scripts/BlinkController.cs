using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> SpritesToBlink;
    public void Blink(float seconds, Action OnBlinkComplete)
    {
        StartCoroutine(BlinkCoroutine(seconds,OnBlinkComplete));
    }

    private IEnumerator BlinkCoroutine(float seconds,Action OnBlinkComplete)
    {
        float timer = 0f;
        while (true)
        {
            ToggleSprites(false);
            yield return new WaitForSeconds(0.1f);
            timer += 0.2f;
            ToggleSprites(true);
            if (timer >= seconds)
            {
                OnBlinkComplete();
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ToggleSprites(bool enable)
    {
        for (int i = 0; i < SpritesToBlink.Count; i++)
        {
            SpritesToBlink[i].enabled = enable;
        }
    }
}
