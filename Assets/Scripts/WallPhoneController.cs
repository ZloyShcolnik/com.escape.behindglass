using UnityEngine;
using UnityEngine.UI;

public class WallPhoneController : MonoBehaviour
{
    private int id;
    private bool isReady;

    private GameObject canvas;
    private AudioSource source;

    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        id = 0;
        isReady = false;

        canvas = GetComponentInChildren<Canvas>().gameObject;
        var btn = canvas.GetComponentInChildren<Button>();
        canvas.SetActive(false);

        source = GetComponent<AudioSource>();

        btn.onClick.AddListener(() =>
        {
            isReady = false;

            var clip = id < audioClips.Length ? audioClips[id] : audioClips[audioClips.Length - 1];
            var duration = clip.length;

            PhoneDialogueSfx.Instant(clip);

            canvas.SetActive(false);
            source.Stop();

            id++;
            Invoke(nameof(TellMyPhome), duration + Random.Range(65.0f, 120.0f));
        });

        Invoke(nameof(TellMyPhome), Random.Range(75.0f, 120.0f));
    }

    private void TellMyPhome()
    {
        source.Play();
        isReady = true;
    }

    private void EnableCanvas()
    {
        canvas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !isReady)
        {
            return;
        }

        canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || !isReady)
        {
            return;
        }

        canvas.SetActive(false);
    }
}
