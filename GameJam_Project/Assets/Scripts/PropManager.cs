using UnityEngine;

public class PropManager : MonoBehaviour
{
    public Transform prop;
    public Vector2 posX = new Vector2(-7, 7);
    public Vector2 posY = new Vector2(-3, 3);

    private void Start()
    {
        RandomProp();
    }

    /// <summary>
    /// 隨機產生道具
    /// </summary>
    private void RandomProp()
    {
        for (int i = 0; i < GameManager.instance.propCount; i++)
        {
            Vector2 pos = new Vector2(Random.Range(posX.x, posX.y), Random.Range(posY.x, posY.y));
            Instantiate(prop, pos, Quaternion.identity);
        }
        
    }
}
