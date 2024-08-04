using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    //
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    //
    [SerializeField] private Transform pressToRebindTransform;

    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        moveUpButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Up));
        moveDownButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Down));
        moveLeftButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Left));
        moveRightButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Move_Right));
        interactButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Interact));
        interactAltButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Interact_Alternate));
        pauseButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Pause));
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();

        Hide();
        HidePressToRebindKey();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + (int)(SoundManager.Instance.Volume * 10f);
        musicText.text = "Music: " + (int)(MusicManager.Instance.Volume * 10f);

        moveUpText.text = GameInput.Instance.BindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.BindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.BindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.BindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.BindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.BindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.Instance.BindingText(GameInput.Binding.Pause);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindTransform.gameObject.SetActive(false);
    }

    public void ShowPressToRebindTransform()
    {
        pressToRebindTransform.gameObject.SetActive(true);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindTransform();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

}
