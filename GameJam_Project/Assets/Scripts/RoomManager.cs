using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public Sprite spriteDoor;
    public Transform wall;
    public List<Transform> walls = new List<Transform>();
    public Transform door;

    // 場景的最高點上方 5 下方 -5
    private float sceneHight = 4.5f;

    private void Start()
    {
        CreateWall();
    }

    /// <summary>
    /// 建立牆壁
    /// </summary>
    private void CreateWall()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 pos = new Vector3(8.5f * (j == 0 ? -1 : 1), sceneHight - i, 0);
                walls.Add(Instantiate(wall, pos, Quaternion.identity));
            }
        }

        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 16; i++)
            {
                Vector3 pos = new Vector3(-7.5f + i, j == 0 ? sceneHight : -sceneHight, 0);
                walls.Add(Instantiate(wall, pos, Quaternion.identity));
            }
        }

        ChooseDoor();
    }

    /// <summary>
    /// 選擇一扇門
    /// </summary>
    private void ChooseDoor()
    {
        int ran = 0;
        while (ran == 0 || ran == 9 || ran == 10 || ran == 19)
        {
            ran = Random.Range(0, walls.Count);
            Debug.Log("隨機：" + ran);
        }

        door = walls[ran];
        SpriteRenderer srDoor = door.GetComponent<SpriteRenderer>();
        srDoor.sprite = spriteDoor;
        srDoor.color = new Color(srDoor.color.r, srDoor.color.g, srDoor.color.b, 0.5f);
        srDoor.GetComponent<BoxCollider2D>().size = Vector2.one * 0.6f;
        srDoor.tag = "門";
    }
}
