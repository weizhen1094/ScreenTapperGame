using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonModel
{
    public int MaxBtns
    {
        get => maxOfBtns;
        set => maxOfBtns = value;
    }
    int maxOfBtns = 3;

    public int NumOfBtns
    {
        get => numOfBtns;
        set => numOfBtns = value;
    }
    int numOfBtns = 3;

    public List<GameObject> Buttons
    {
        get => buttons;
        set => buttons = value;
    }
    List<GameObject> buttons = new List<GameObject>();
}

public class ButtonController : MonoBehaviour
{
    public static ButtonController INSTANCE = null;

    ButtonModel buttonModel;

    [SerializeField] GameObject btnParent;

    void Awake()
    {
        // singleton
        if (!INSTANCE)
            INSTANCE = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        buttonModel = new ButtonModel();
        InitButtons();
    }

    public void InitButtons()
    {
        for (int i = 0; i < buttonModel.MaxBtns; ++i)
        {
            GameObject btn = Instantiate(btnParent.transform.GetChild(0).gameObject);
            btn.GetComponent<RectTransform>().SetParent(btnParent.transform);
            btn.SetActive(false);
            buttonModel.Buttons.Add(btn);
        }
    }

    public void SpawnButtons()
    {
        buttonModel.NumOfBtns = Random.Range(1, buttonModel.MaxBtns + 1);
        for (int i = 0; i < buttonModel.NumOfBtns; ++i)
        {
            GameObject btn = btnParent.transform.GetChild(i).gameObject;
            btn.SetActive(true);
            RectTransform rt = btn.GetComponent<RectTransform>();
            float randX = Random.Range(0 + rt.rect.width * 0.5f, Screen.width - rt.rect.width * 0.5f);
            float randY = Random.Range(0 + rt.rect.height * 0.5f, Screen.height - rt.rect.height * 0.5f - 200);
            rt.anchoredPosition = new Vector3(randX, randY, 0);
            btn.GetComponent<Button>().onClick.AddListener(() => OnBtnClicked(btn));
            StartCoroutine(UIAnimator.INSTANCE.ScaleUI(btn.GetComponent<RectTransform>(), Vector3.zero, Vector3.one,  1.5f));
        }
    }

    public void DespawnButtons()
    {
        foreach (GameObject go in buttonModel.Buttons)
        {
            if (go.activeInHierarchy)
                go.SetActive(false);
        }
    }

    public bool GetAreAllBtnsClicked()
    {
        foreach (GameObject go in buttonModel.Buttons)
        {
            if (go.activeInHierarchy == true)
                return false;
        }
        return true;
    }

    void OnBtnClicked(GameObject _btn)
    {
       _btn.SetActive(false);
    }
}
