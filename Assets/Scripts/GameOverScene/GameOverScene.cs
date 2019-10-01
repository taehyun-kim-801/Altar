using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            yield return new WaitForEndOfFrame();
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.4f * 1 / 60 * fadeInTime);
        }

        gameOverText.SetActive(true);

        yield return new WaitForSecondsRealtime(0.7f);

        caughtMonsterText.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);

        caughtMonsterNum.GetComponent<Text>().text = GameManager.Instance.caughtMonsterCount.ToString();
        caughtMonsterNum.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);

        timeText.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);

        timeNum.GetComponent<Text>().text = TimeToString((int)(endTime - GameManager.Instance.startTime));
        timeNum.SetActive(true);
        yield return new WaitForSecondsRealtime(0.7f);

        touchToContinueText.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButton(0));
        GameManager.Instance.SetStartUI();
        Destroy(gameObject);

    }

    private string TimeToString(int time)
    {
        return string.Format("{0:D2}:{1:D2}", time / 60, time % 60);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}