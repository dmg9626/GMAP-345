using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSingleton : MonoBehaviour
{
    public static HelperSingleton instance;

	void Awake ()
    {
		if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}

    public Dictionary<Vector2, float> angleByVelocity = new Dictionary<Vector2, float>()
    {
        { (new Vector2(0f, 1f)).normalized, 0f },
        { (new Vector2(-1f, 1f)).normalized, 45f },
        { (new Vector2(-1f, 0f)).normalized, 90f },
        { (new Vector2(-1f, -1f)).normalized, 135f },
        { (new Vector2(0f, -1f)).normalized, 180f },
        { (new Vector2(1f, -1f)).normalized, 225f },
        { (new Vector2(1f, 0f)).normalized, 270f },
        { (new Vector2(1f, 1f)).normalized, 315f },
    };
}
