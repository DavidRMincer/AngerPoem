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
                                textDuration;

    private Color               _transparent = new Color(0f, 0f, 0f, 0f),
                                _black = new Color(0f, 0f, 0f, 255f),
                                _white = new Color(255f, 255f, 255f, 255f);

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

        yield return new WaitForSeconds(1f);

        foreach (Character_Script item in characters)
        {
            item.SetRBType(RigidbodyType2D.Dynamic);
        }
    }

    private IEnumerator FadeTo(Color colour)
    {
        float   counter = 0;
        Color   prevWhiteOutColour = whiteOutImg.color,
                prevTextColour = text.color;

        do
        {
            yield return new WaitForSeconds(Time.deltaTime);
            counter += Time.deltaTime;
            counter = counter > fadeDuration ? fadeDuration : counter;

            whiteOutImg.color = Color.Lerp(prevWhiteOutColour, colour, counter / fadeDuration);
            text.color = Color.Lerp(prevTextColour, colour, counter / fadeDuration);

        } while (counter < fadeDuration);

        yield return null;
    }
}
