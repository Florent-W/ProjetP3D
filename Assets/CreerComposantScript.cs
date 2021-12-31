using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreerComposantScript : MonoBehaviour
{
    /// <summary>
    /// Cette méthode permet de créer et de récupérer un GameObject Texte
    /// </summary>
    /// <returns>Récupère un GameObject de texte</returns>
    public GameObject CreateText(Transform canvas_transform, string nameGameObject, float width, float height, float x, float y, string text_to_print, int font_size, int min_font_size, int max_size_font, Color text_color)
    {
        GameObject UItextGO = new GameObject(nameGameObject);
        UItextGO.transform.SetParent(canvas_transform);

        RectTransform transform = UItextGO.AddComponent<RectTransform>();
        transform.sizeDelta = new Vector2(width, height);
        transform.anchoredPosition = new Vector2(x, y);
        transform.localScale = new Vector3(1, 1, 1);

        Text text = UItextGO.AddComponent<Text>();
        text.text = text_to_print;

        if (min_font_size > 0 || max_size_font > 0)
        {
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = min_font_size;
            text.resizeTextMaxSize = max_size_font;
        }
        else
        {
            text.fontSize = font_size;
        }

        text.color = text_color;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        return UItextGO;
    }

    public GameObject CreateText3D(Transform canvas_transform, string nameGameObject, float width, float height, float x, float y, string text_to_print, int font_size, Color text_color, GameObject cameraCombatGameObject)
    {
        foreach (Transform t in canvas_transform)
        {
            if (t.gameObject.name == "NbDegats")
            {
                Destroy(t.gameObject);
                break;
            }
        }

        GameObject UItextGO = new GameObject(nameGameObject);
        UItextGO.transform.SetParent(canvas_transform);

        RectTransform transform = UItextGO.AddComponent<RectTransform>();
        transform.sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector3(x, y + 2, 0);
        transform.localScale = new Vector3(-1, 1, 1);

        TextMesh text = UItextGO.AddComponent<TextMesh>();
        text.text = text_to_print;
        text.anchor = TextAnchor.UpperCenter;
        text.characterSize = 0.5f;

        text.fontSize = font_size;

        text.color = text_color;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        UI3DScript uiscript = UItextGO.AddComponent<UI3DScript>();
        uiscript.cameraCombat = cameraCombatGameObject;

        return UItextGO;
    }

    public GameObject CreateBarre3D(Transform canvas_transform, string nameGameObject, float width, float height, float x, float y, GameObject barreVieGameObject, GameObject cameraGameObject)
    {
        GameObject UIBarreGO = Instantiate(barreVieGameObject);
        UIBarreGO.name = nameGameObject;
        UIBarreGO.transform.SetParent(canvas_transform);

        RectTransform transform = UIBarreGO.GetComponent<RectTransform>();
        transform.sizeDelta = new Vector2(width, height);
        transform.localPosition = new Vector3(x, y + 2, 0);
        transform.localScale = new Vector3(-1, 1, 1);

        UI3DScript uiscript = UIBarreGO.AddComponent<UI3DScript>();
        uiscript.cameraCombat = cameraGameObject;

        return UIBarreGO;
    }

    /// <summary>
    /// Cette méthode permet de créer et de récupérer un GameObject de Bouton
    /// </summary>
    /// <returns>Récupère un GameObject de bouton</returns>
    public GameObject CreateButton(Transform canvas_transform, string nameGameObject, int bouton_width, int bouton_height, string text_to_print, int font_size)
    {
        DefaultControls.Resources uiResources = new DefaultControls.Resources();

        GameObject uiButtonGameObject = DefaultControls.CreateButton(uiResources);
        uiButtonGameObject.name = nameGameObject;
        uiButtonGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(bouton_width, bouton_height);
        Text textUiButton = uiButtonGameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        textUiButton.text = text_to_print;
        textUiButton.fontSize = font_size;
        uiButtonGameObject.transform.SetParent(canvas_transform.transform, false);

        return uiButtonGameObject;
    }

    public GameObject CreateButton(Transform canvas_transform, string nameGameObject, int bouton_width, int bouton_height, string text_to_print, int font_size, int x, int y)
    {
        DefaultControls.Resources uiResources = new DefaultControls.Resources();

        GameObject uiButtonGameObject = DefaultControls.CreateButton(uiResources);
        uiButtonGameObject.name = nameGameObject;
        uiButtonGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(bouton_width, bouton_height);
        uiButtonGameObject.transform.localPosition = new Vector2(x, y);
        Text textUiButton = uiButtonGameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        textUiButton.text = text_to_print;
        textUiButton.fontSize = font_size;
        uiButtonGameObject.transform.SetParent(canvas_transform.transform, false);

        return uiButtonGameObject;
    }
}
