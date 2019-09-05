using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text textTime;

    private float timer;

    private void Start()
    {
        timer = Random.Range(GameManager.instance.rangeTimer.x, GameManager.instance.rangeTimer.y);
    }

    private void Update()
    {
        Timer();
    }

    /// <summary>
    /// 計時器
    /// </summary>
    private void Timer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) TimeUp();

        textTime.text = "TIME:" + timer.ToString("F2");
    }

    /// <summary>
    /// 時間結束
    /// </summary>
    private void TimeUp()
    {
        timer = 0;

        if (timer == 0)
        {
            GameManager.instance.Lose();
        }
    }
}
