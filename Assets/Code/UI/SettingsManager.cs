using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
  
  [SerializeField] private Renderer2DData renderData;
  private FullScreenPassRendererFeature renderPass;
  [SerializeField] private Material normalPalette, protanopia, deuteranopia, tritanopia, grayscale;
  [SerializeField] private Slider master, music, sfx;
  [SerializeField] private Toggle toggle;
  public void OnEnable() {
    renderData.TryGetRendererFeature(out renderPass);
    SaveManager.Load(ref SaveManager.currentSave);

    master.value = SaveManager.currentSave.masterVolume;
    music.value = SaveManager.currentSave.musicVolume;
    sfx.value = SaveManager.currentSave.sfxVolume;
    dropdown.value = SaveManager.currentSave.palette switch {
       ColorPalette.PROTANOPIA => 2,
       ColorPalette.DEUTERANOPIA => 1,
       ColorPalette.TRITANOPIA => 3,
       ColorPalette.GRAYSCALE => 4,
      _ => 0
    };
    renderPass.passMaterial = SaveManager.currentSave.palette switch {
      ColorPalette.PROTANOPIA => protanopia,
      ColorPalette.DEUTERANOPIA => deuteranopia,
      ColorPalette.TRITANOPIA => tritanopia,
      ColorPalette.GRAYSCALE => grayscale,
      _ => normalPalette
    };
    toggle.isOn = SaveManager.currentSave.simplistic;
  }

  public void SetMasterVolume(Slider slider) => SaveManager.currentSave.masterVolume = slider.value;
  public void SetMusicVolume(Slider slider) => SaveManager.currentSave.musicVolume = slider.value;
  public void SetSFXVolume(Slider slider) => SaveManager.currentSave.sfxVolume = slider.value;
  
  [SerializeField] private TMP_Dropdown dropdown;
  
  public void SetPalette() {
    SaveManager.currentSave.palette = dropdown.value switch {
      2 => ColorPalette.PROTANOPIA,
      1 => ColorPalette.DEUTERANOPIA,
      3 => ColorPalette.TRITANOPIA,
      4 => ColorPalette.GRAYSCALE,
      _ => ColorPalette.NORMAL
    };
    renderPass.passMaterial = SaveManager.currentSave.palette switch {
      ColorPalette.PROTANOPIA => protanopia,
      ColorPalette.DEUTERANOPIA => deuteranopia,
      ColorPalette.TRITANOPIA => tritanopia,
      ColorPalette.GRAYSCALE => grayscale,
      _ => normalPalette
    };
  }
  public void OnCloseSettings() => SaveManager.Save(SaveManager.currentSave);
  public void Simplistic() => SaveManager.currentSave.simplistic = toggle.isOn;
  
  
}
