using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsScript : MonoBehaviour
{
    public Dropdown resolutionDropdown, qualityDropdown;
    Resolution[] resolutions;
    string[] qualites;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int resolutionActuelIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionActuelIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionActuelIndex;
        resolutionDropdown.RefreshShownValue();

        qualites = QualitySettings.names;
        qualityDropdown.ClearOptions();

        List<string> qualitesList = new List<string>(qualites);
        int qualiteActuelIndex = 0;

        for(int i = 0; i < qualitesList.Count; i++)
        {
            if(i == QualitySettings.GetQualityLevel())
            {
                qualiteActuelIndex = i;
            }
        }
      
        qualityDropdown.AddOptions(qualitesList);
        qualityDropdown.value = qualiteActuelIndex;
        qualityDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetPleinEcran(bool estEnPleinEcran)
    {
        Screen.fullScreen = estEnPleinEcran;
    }

    public void SetQualite(int qualiteIndex)
    {
        QualitySettings.SetQualityLevel(qualiteIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
