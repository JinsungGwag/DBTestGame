using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speedY;

    private void Update()
    {
        transform.Translate(0f, -_speedY, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene("Result");
        }
        else if (collision.transform.CompareTag("Ground"))
        {
            if(ScoreSystem.Instance != null)
                ScoreSystem.Instance.RaiseScore();
            Destroy(gameObject);
        }
    }
}
