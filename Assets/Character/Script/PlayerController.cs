using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource backSoundSource;

    private AudioSource audioSource;
    private bool isDead = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("Enemy"))
        {
            Die();
        }
        if (collision.CompareTag("Finish"))
        {
            Finish();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Game Over");
        _animator.SetBool("isDead", true);

        _rb.linearVelocity = Vector2.zero;
        StartCoroutine(FadeOutMusic(1.5f));
        audioSource.PlayOneShot(gameOverSound);     

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if(script != this)
            {
                script.enabled = false;
            }
        }

        float delay = 4f;
        Invoke(nameof(ShowGameOverPanel), delay);
    }

    private void ShowGameOverPanel()
    {
        gameManager.GameOver();
    }

    private void Finish()
    {
        Time.timeScale = 0f;       
        StartCoroutine(FadeOutMusic(1.5f));
        gameManager.Finish();
        audioSource.PlayOneShot(finishSound);

        _animator.SetBool("isWalk", false);
        _animator.SetBool("isJump", false);
    }

    private IEnumerator FadeOutMusic(float fadeTime)
    {
        float startVolume = backSoundSource.volume;

        while (backSoundSource.volume > 0)
        {
            backSoundSource.volume -= startVolume * Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }

        backSoundSource.Stop();
        backSoundSource.volume = startVolume; // reset volume
    }
}
