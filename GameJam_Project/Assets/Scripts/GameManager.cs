using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region 欄位
    [Header("範圍 - 道具數量")]
    public Vector2 rangePropCount = new Vector2(1, 4);
    [Header("範圍 - 時間限制")]
    public Vector2 rangeTimer = new Vector2(10, 20);

    // 介面
    public Text textScore, textTip, textBest;

    public Transform cameraMain;

    public static GameManager instance;
    public static int passLevel;

    [Header("攝影機晃動時間、震度")]
    public float durationShake = 1.5f;
    public float magnitude = 0.2f;
    [HideInInspector]
    public bool stop;
    [HideInInspector]
    public int propCount = 1;
    private float timeShake;
    #endregion

    private void Awake()
    {
        instance = this;

        Initial();
    }

    private void Update()
    {
        ResetGame();
    }

    /// <summary>
    /// 初始值
    /// </summary>
    private void Initial()
    {
        propCount = (int)Random.Range(rangePropCount.x, rangePropCount.y);
    }

    /// <summary>
    /// 重設遊戲
    /// </summary>
    private void ResetGame()
    {
        if (stop && Input.GetKeyDown(KeyCode.Space))
        {
            passLevel = 0;
            SceneManager.LoadScene("遊戲場景");
        }
    }

    /// <summary>
    /// 開門
    /// </summary>
    public void OpenDoor()
    {
        GetComponent<RoomManager>().door.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<RoomManager>().door.GetComponent<Collider2D>().isTrigger = true;
    }

    /// <summary>
    /// 過關
    /// </summary>
    public void Pass()
    {
        passLevel++;
        SceneManager.LoadScene("遊戲場景");
    }

    /// <summary>
    /// 失敗
    /// </summary>
    public void Lose()
    {
        stop = true;
        textScore.text = "通過關卡：" + passLevel;
        textTip.text = "時間結束，挑戰失敗！" + "\n請按空白鍵重新開始！";

        HighScore();
    }

    /// <summary>
    /// 最佳紀錄
    /// </summary>
    private void HighScore()
    {
        if (passLevel > PlayerPrefs.GetInt("最佳紀錄"))
        {
            PlayerPrefs.SetInt("最佳紀錄", passLevel);
        }
        textBest.text = "最佳紀錄：" + PlayerPrefs.GetInt("最佳紀錄");
    }

    /// <summary>
    /// 晃動攝影機
    /// </summary>
    /// <returns></returns>
    public IEnumerator CameraShake()
    {
        Vector3 originalCamPos = cameraMain.position;
        timeShake = 0;

        while (timeShake < durationShake)
        {
            timeShake += Time.deltaTime;

            float percent = timeShake / durationShake;
            float damper = 1f - Mathf.Clamp(4 * percent - 3f, 0, 1);

            float x = Random.value * 2 - 1;
            float y = Random.value * 2 - 1;

            x *= magnitude * damper;
            y *= magnitude * damper;

            cameraMain.position = new Vector3(x, y, -10);

            yield return null;
        }

        cameraMain.position = originalCamPos;
    }
}
