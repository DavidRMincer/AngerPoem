using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Script : MonoBehaviour
{
    public Image                whiteOutImg;
    public TextMeshProUGUI      text;
    public Character_Script[]   characters;
    public float                fadeDuration,
                                textDuration,
                                postRageWait;
    public string               finalText;

    private Color               _transparent = new Color(0f, 0f, 0f, 0f),
                                _black = new Color(0f, 0f, 0f, 1f),
                                _white = new Color(1f, 1f, 1f, 1f);

    private void Start()
    {
        StartCoroutine(StartingRoutine());
    }

    private IEnumerator StartingRoutine()
    {
        foreach (Character_Script item in characters)
        {
            item.SetRBType(RigidbodyType2D.Static);
        }

        yield return new WaitForSeconds(textDuration);

        StartCoroutine(FadeTo(_transparent));
        StartCoroutine(FadeTextTo(_transparent));

        yield return new WaitForSeconds(fadeDuration + 1f);

        foreach (Character_Script item in characters)
        {
            item.SetRBType(RigidbodyType2D.Dynamic);
        }
    }

    public IEnumerator EndingRoutine()
    {
        yield return new WaitForSeconds(postRageWait);

        text.text = finalText;

        StartCoroutine(FadeTo(_white));
        yield return new WaitForSeconds(fadeDuration + 1f);

        StartCoroutine(FadeTextTo(_black));
        yield return new WaitForSeconds(fadeDuration + textDuration);

        yield return null;
    }

    private IEnumerator FadeTo(Color colour)
    {
        float   counter = 0;
        Color   prevWhiteOutColour = whiteOutImg.color;

        do
        {
            Debug.Log(whiteOutImg.color);
            yield return new WaitForSeconds(Time.deltaTime);
            counter += Time.deltaTime;
            counter = counter > fadeDuration ? fadeDuration : counter;

            whiteOutImg.color = Color.Lerp(prevWhiteOutColour, colour, counter / fadeDuration);

        } while (counter < fadeDuration);

        yield return null;
    }

    private IEnumerator FadeTextTo(Color colour)
    {
        float counter = 0;
        Color prevTextColour = text.color;

        do
        {
            yield return new WaitForSeconds(Time.deltaTime);
            counter += Time.deltaTime;
            counter = counter > fadeDuration ? fadeDuration : counter;
            
            text.color = Color.Lerp(prevTextColour, colour, counter / fadeDuration);

        } while (counter < fadeDuration);

        yield return null;
    }
}
