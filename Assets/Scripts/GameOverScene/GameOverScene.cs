using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameOverScene : MonoBehaviour
{
    public Image image;
    public GameObject gameOverText;
    public GameObject caughtMonsterText;
    public GameObject caughtMonsterNum;
    public GameObject timeText;
    public GameObject timeNum;
    public GameObject touchToContinueText;

    private readonly float fadeInTime = 0.7f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        float endTime = Time.time;
        image = GetComponentInChildren<Image>();
        while(image.color.a<=0.4f)
        {
            yield return null;
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.4f * 1 / 60 * fadeInTime);
        }

        gameOverText.SetActive(true);
        float animationStartTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - animationStartTime < 0.7f)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
                break;
        }

        caughtMonsterText.SetActive(true);
        animationStartTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - animationStartTime < 0.7f)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
                break;
        }

        caughtMonsterNum.GetComponent<Text>().text = GameManager.Instance.caughtMonsterCount.ToString();
        caughtMonsterNum.SetActive(true);
        animationStartTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - animationStartTime < 0.7f)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
                break;
        }

        timeText.SetActive(true);
        animationStartTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - animationStartTime < 0.7f)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
                break;
        }

        timeNum.GetComponent<Text>().text = TimeToString((int)(endTime - GameManager.Instance.startTime));
        timeNum.SetActive(true);
        animationStartTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - animationStartTime < 0.7f)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
                break;
        }

        touchToContinueText.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        Destroy(gameObject);

        GameManager.Instance.GameReset();
    }

    private string TimeToString(int time)
    {
        return string.Format("{0:D2}:{1:D2}", time / 60, time % 60);
    }
}