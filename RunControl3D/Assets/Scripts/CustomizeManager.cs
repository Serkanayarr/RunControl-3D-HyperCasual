using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Seko;
using TMPro;
using UnityEngine.SceneManagement;


public class CustomizeManager : MonoBehaviour
{
    public Text PuanText;
    public GameObject[] OperationPanels;
    public GameObject OperationCanvas;
    public GameObject[] GeneralPanels;
    public Button[] OperationButtons;
    public TextMeshProUGUI BuyingText;
    public GameObject ChooseText;
    [Header("CAPS")]
    public GameObject[] Caps;
    public Button[] CapsButtons;
    public TextMeshProUGUI CapText;
    [Header("STICKS")]
    public GameObject[] Sticks;
    public Button[] SticksButtons;
    public TextMeshProUGUI StickText;
    [Header("MATERIALS")]
    public Material[] Materials;
    public Button[] MaterialsButtons;
    public TextMeshProUGUI MaterialText;
    public SkinnedMeshRenderer _Renderer;
    public AudioSource[] Sounds;


    int CapIndex = -1;
    int StickIndex = -1;
    int MaterialIndex = -1;
    int ActiveOperationPanelIndex;

    MemoryManagement _MemoryManagement = new MemoryManagement();
    DataManagement _DataManagement = new DataManagement();
    public Animator _SavedAnimator;

    [Header("GENERAL DATAS")]
    public List<ItemDatas> _ItemDatas = new List<ItemDatas>();
    public List<LanguageDatasMainObject> _LanguageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> _LanguageReadDatas = new List<LanguageDatasMainObject>();
    public TextMeshProUGUI[] TextObjects;

    string SatinAlmaText;
    string ItemText;

    void Start()
    {

        PuanText.text = _MemoryManagement.ReadData_int("Point").ToString();

        _DataManagement.Load();//verileri okuyoruz
        _ItemDatas = _DataManagement.TransferList();//okumuþ olduðumuz verileri listemize aktarýyoruz*/

        ControlTheSituation(0, true);
        ControlTheSituation(1, true);
        ControlTheSituation(2, true);

        foreach (var item in Sounds)
        {
            item.volume = _MemoryManagement.ReadData_float("MenuFX");
        }
        //_MemoryManagement.SaveData_string("Language", "TR");
        _DataManagement.LanguageLoad();
        _LanguageReadDatas = _DataManagement.TransferLanguageList();
        _LanguageDatasMainObject.Add(_LanguageReadDatas[1]);
        LanguagePreferManagement();

    }
    public void LanguagePreferManagement()
    {
        if (_MemoryManagement.ReadData_string("Language") == "TR")
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = _LanguageDatasMainObject[0].languageDatas_TR[i].Text;
            }
            SatinAlmaText = _LanguageDatasMainObject[0].languageDatas_TR[4].Text;
            ItemText = _LanguageDatasMainObject[0].languageDatas_TR[3].Text;
        }
        else
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = _LanguageDatasMainObject[0].languageDatas_EN[i].Text;
            }
            SatinAlmaText = _LanguageDatasMainObject[0].languageDatas_EN[4].Text;
            ItemText = _LanguageDatasMainObject[0].languageDatas_EN[3].Text;
        }
    }
    
    public void ControlTheSituation(int Part, bool Operation = false) 
    {
        if (Part == 0)
        {
            #region
            if (_MemoryManagement.ReadData_int("ActiveCap") == -1) /* eðer cap ýndeximiz -1' e eþitse yani elimizde þapka yoksa there is no cap yazýsýný yazdýrýyoruz*/
            {
                foreach (var cap in Caps)
                {
                    cap.SetActive(false);
                }

                
                OperationButtons[0].interactable = false;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[1].interactable = false;

                if (!Operation)
                {
                    CapIndex = -1;
                    CapText.text = ItemText;
                }
            }
            else
            {
                foreach(var cap in Caps)
                {
                    cap.SetActive(false);
                }
                CapIndex = _MemoryManagement.ReadData_int("ActiveCap");
                Caps[CapIndex].SetActive(true);
                CapText.text = _ItemDatas[CapIndex].ItemName;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = true;

            }
            #endregion
        }
        if(Part == 1)
        {
            #region
            if (_MemoryManagement.ReadData_int("ActiveStick") == -1) /* eðer cap ýndeximiz -1' e eþitse yani elimizde þapka yoksa there is no cap yazýsýný yazdýrýyoruz*/
            {
                foreach (var stick in Sticks)
                {
                    stick.SetActive(false);
                }

                OperationButtons[0].interactable = false;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[1].interactable = false;

                if (!Operation)
                {
                    StickIndex = -1;
                    StickText.text = ItemText;
                }
            }
            else
            {
                foreach (var stick in Sticks)
                {
                    stick.SetActive(false);
                }
                StickIndex = _MemoryManagement.ReadData_int("ActiveStick");
                Sticks[StickIndex].SetActive(true);
                StickText.text = _ItemDatas[StickIndex + 3].ItemName;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = true;
            }
            #endregion
        }
        else
        {
            if(_MemoryManagement.ReadData_int("ActiveTheme") == 0)
            {
                OperationButtons[0].interactable = false;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[1].interactable = false;
            }
            if (_MemoryManagement.ReadData_int("ActiveTheme") == -1) /* eðer cap ýndeximiz -1' e eþitse yani elimizde þapka yoksa there is no cap yazýsýný yazdýrýyoruz*/
            {
                MaterialIndex = _MemoryManagement.ReadData_int("ActiveTheme" );
                if (!Operation)
                {
                    TextObjects[4].text = SatinAlmaText;
                    MaterialText.text = ItemText;
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = false;
                }
                else 
                {
                    MaterialIndex = _MemoryManagement.ReadData_int("ActiveTheme");
                    Material[] mats = _Renderer.materials;
                    mats[0] = Materials[MaterialIndex];
                    _Renderer.materials = mats;
                    TextObjects[4].text = SatinAlmaText;
                }
            }
            else
            {
                MaterialIndex = _MemoryManagement.ReadData_int("ActiveTheme");
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;

                MaterialText.text = _ItemDatas[MaterialIndex + 6].ItemName;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[0].interactable = false;
                OperationButtons[1].interactable = true;
            }
        }
    }
    public void Buy() 
    {
        Sounds[1].Play();
        if (ActiveOperationPanelIndex != -1)
        {
            switch (ActiveOperationPanelIndex)
            {
                case 0:
                    BuyResult(CapIndex);
                    break;
                case 1:
                    int index = StickIndex + 3;
                    BuyResult(index);
                    break;
                case 2:
                    int index2 = MaterialIndex + 3;
                    BuyResult(index2);
                    break;
            }
        }
        
    }
    public void Save()
    {
        Sounds[2].Play();
        if (ActiveOperationPanelIndex != -1)
        {
            switch (ActiveOperationPanelIndex)
            {
                case 0:
                    SaveResult("ActiveCap", CapIndex);
                    break;
                case 1:
                    SaveResult("ActiveStick", StickIndex);
                    break;
                case 2:
                    SaveResult("ActiveTheme", MaterialIndex);
                    break;
            }
        }
    }
    public void CapDirectionButtons(string operation) 
    {
        Sounds[0].Play();
        if (operation == "Forward") 
        {
            if(CapIndex == -1) // eðer ileriye bastýðýmda þapka indexi -1 ise 
            {
                CapIndex = 0;
                Caps[CapIndex].SetActive(true);
                CapText.text = _ItemDatas[CapIndex].ItemName;

                if (!_ItemDatas[CapIndex].BuyingSituation)
                {
                    TextObjects[4].text = _ItemDatas[CapIndex].Point + " - BUY";
                    OperationButtons[1].interactable = false;
                    if(_MemoryManagement.ReadData_int("Point") < _ItemDatas[CapIndex].Point)
                    {
                        OperationButtons[0].interactable = false;
                    }
                    else
                    {
                        OperationButtons[0].interactable = true;
                    }
                }
                else 
                {
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            else // ileri yönde gidiceðim için bulunduðumuz þapkayý deaktif edip bir sonraki þapkayý aktif edicek.
            {
                Caps[CapIndex].SetActive(false);
                CapIndex++;
                Caps[CapIndex].SetActive(true);
                CapText.text = _ItemDatas[CapIndex].ItemName;

                if (!_ItemDatas[CapIndex].BuyingSituation)
                {
                    TextObjects[4].text = _ItemDatas[CapIndex].Point + " - " + SatinAlmaText;
                    OperationButtons[1].interactable = false;
                    if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[CapIndex].Point)
                    {
                        OperationButtons[0].interactable = false;
                    }
                    else
                    {
                        OperationButtons[0].interactable = true;
                    }
                }
                else
                {
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            //-------------------------------------------------------------------------------------------------------------

            if(CapIndex == Caps.Length - 1) 
                CapsButtons[1].interactable = false;

            else 
                CapsButtons[1].interactable = true;
            
            if (CapIndex != -1)
                CapsButtons[0].interactable = true;
        }
        else
        {
            if(CapIndex != -1) 
            {
                Caps[CapIndex].SetActive(false);
                CapIndex--;
                if(CapIndex != -1) 
                {
                    Caps[CapIndex].SetActive(true);
                    CapsButtons[0].interactable = true;
                    CapText.text = _ItemDatas[CapIndex].ItemName;

                    if (!_ItemDatas[CapIndex].BuyingSituation)
                    {
                        TextObjects[4].text = _ItemDatas[CapIndex].Point + " - " + SatinAlmaText;
                        OperationButtons[1].interactable = false;
                        if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[CapIndex].Point)
                        {
                            OperationButtons[0].interactable = false;
                        }
                        else
                        {
                            OperationButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        TextObjects[4].text = SatinAlmaText;
                        OperationButtons[0].interactable = false;
                        OperationButtons[1].interactable = true;
                    }
                }
                else 
                {
                    CapsButtons[0].interactable = false;
                    CapText.text = ItemText;
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                }

            }
            else 
            {
                CapsButtons[0].interactable = false;
                CapText.text = ItemText;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[0].interactable = false;
            }

            //-------------------------------------------------------------------------------------------------------------

            if (CapIndex != Caps.Length - 1 ) //yani eðer ben sona gelip geri gittiysem öne gitme butonunu tekrardan aktifleþtir
                CapsButtons[1].interactable= true;
        }
    }
    public void StickDirectionButtons(string operation)
    {
        Sounds[0].Play();
        if (operation == "Forward")
        {
            if (StickIndex == -1) // eðer ileriye bastýðýmda þapka indexi -1 ise 
            {
                StickIndex = 0;
                Sticks[StickIndex].SetActive(true);
                StickText.text = _ItemDatas[StickIndex + 3].ItemName;

                if (!_ItemDatas[StickIndex + 3].BuyingSituation)
                {
                    TextObjects[4].text = _ItemDatas[StickIndex + 3].Point + " - " + SatinAlmaText;
                    OperationButtons[1].interactable = false;
                    if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[StickIndex + 3].Point)
                    {
                        OperationButtons[0].interactable = false;
                    }
                    else
                    {
                        OperationButtons[0].interactable = true;
                    }
                }
                else
                {
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            else // ileri yönde gidiceðim için bulunduðumuz þapkayý deaktif edip bir sonraki þapkayý aktif edicek.
            {
                Sticks[StickIndex].SetActive(false);
                StickIndex++;
                Sticks[StickIndex].SetActive(true);
                StickText.text = _ItemDatas[StickIndex + 3].ItemName;//item datasta 0ýncý index cap olduðu için þapkalarýnýn bitiþinden baþlamak için +3 diyoruz

                if (!_ItemDatas[StickIndex + 3].BuyingSituation)
                {
                    TextObjects[4].text = _ItemDatas[StickIndex + 3].Point + " - " + SatinAlmaText;
                    OperationButtons[1].interactable = false;
                    if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[StickIndex + 3].Point)
                    {
                        OperationButtons[0].interactable = false;
                    }
                    else
                    {
                        OperationButtons[0].interactable = true;
                    }
                }
                else
                {
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            //-------------------------------------------------------------------------------------------------------------

            if (StickIndex == Sticks.Length - 1)
                SticksButtons[1].interactable = false;

            else
                SticksButtons[1].interactable = true;

            if (StickIndex != -1)
                SticksButtons[0].interactable = true;
        }
        else
        {
            if (StickIndex != -1)
            {
                Sticks[StickIndex].SetActive(false);
                StickIndex--;
                if (StickIndex != -1)
                {
                    Sticks[StickIndex].SetActive(true);
                    SticksButtons[0].interactable = true;
                    StickText.text = _ItemDatas[StickIndex + 3].ItemName;

                    if (!_ItemDatas[StickIndex + 3].BuyingSituation)
                    {
                        TextObjects[4].text = _ItemDatas[StickIndex + 3].Point + " - " + SatinAlmaText;
                        OperationButtons[1].interactable = false;
                        if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[StickIndex + 3].Point)
                        {
                            OperationButtons[0].interactable = false;
                        }
                        else
                        {
                            OperationButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        TextObjects[4].text = SatinAlmaText;
                        OperationButtons[0].interactable = false;
                        OperationButtons[1].interactable = true;
                    }
                }
                else
                {
                    SticksButtons[0].interactable = false;
                    StickText.text = ItemText;
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                }

            }
            else
            {
                SticksButtons[0].interactable = false;
                StickText.text = ItemText;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[0].interactable = false;
            }

            //-------------------------------------------------------------------------------------------------------------

            if (StickIndex != Sticks.Length - 1) //yani eðer ben sona gelip geri gittiysem öne gitme butonunu tekrardan aktifleþtir
                SticksButtons[1].interactable = true;
        }
    }
    public void MaterialDirectionButtons(string operation)
    {
        Sounds[0].Play();
        if (operation == "Forward")
        {
            if (MaterialIndex == -1) // eðer ileriye bastýðýmda þapka indexi -1 ise 
            {
                //MaterialIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;

                MaterialText.text = _ItemDatas[MaterialIndex + 6].ItemName;

                if (!_ItemDatas[MaterialIndex + 6].BuyingSituation)
                {
                    TextObjects[4].text = _ItemDatas[MaterialIndex + 6].Point + " - " + SatinAlmaText;
                    OperationButtons[1].interactable = false;
                    if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[MaterialIndex + 6].Point)
                    {
                        OperationButtons[0].interactable = false;
                    }
                    else
                    {
                        OperationButtons[0].interactable = true;
                    }
                }
                else
                {
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            else // ileri yönde gidiceðim için bulunduðumuz þapkayý deaktif edip bir sonraki þapkayý aktif edicek.
            {
                MaterialIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex ];
                _Renderer.materials = mats;

                MaterialText.text = _ItemDatas[MaterialIndex + 6].ItemName;

                if (!_ItemDatas[MaterialIndex + 6].BuyingSituation)
                {
                    TextObjects[4].text = _ItemDatas[MaterialIndex + 6].Point + " - " + SatinAlmaText;
                    OperationButtons[1].interactable = false;
                    if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[MaterialIndex + 6].Point)
                    {
                        OperationButtons[0].interactable = false;
                    }
                    else
                    {
                        OperationButtons[0].interactable = true;
                    }
                }
                else
                {
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                    OperationButtons[1].interactable = true;
                }
            }
            //-------------------------------------------------------------------------------------------------------------

            if (MaterialIndex == Materials.Length - 1)
                MaterialsButtons[1].interactable = false;

            else
                MaterialsButtons[1].interactable = true;

            if (MaterialIndex != -1)
                MaterialsButtons[0].interactable = true;
        }
        else
        {
            if (MaterialIndex != -1)
            {
                MaterialIndex--;
                if (MaterialIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Materials[MaterialIndex];
                    _Renderer.materials = mats;


                    MaterialsButtons[0].interactable = true;
                    MaterialText.text = _ItemDatas[MaterialIndex + 6].ItemName;

                    if (!_ItemDatas[MaterialIndex + 6].BuyingSituation)
                    {
                        TextObjects[4].text = _ItemDatas[MaterialIndex + 6].Point + " - " + SatinAlmaText;
                        OperationButtons[1].interactable = false;
                        if (_MemoryManagement.ReadData_int("Point") < _ItemDatas[MaterialIndex + 6].Point)
                        {
                            OperationButtons[0].interactable = false;
                        }
                        else
                        {
                            OperationButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        TextObjects[4].text = SatinAlmaText;
                        OperationButtons[0].interactable = false;
                        OperationButtons[1].interactable = true;
                    }
                }
                else
                {
                    MaterialsButtons[0].interactable = false;
                    MaterialText.text = ItemText;
                    TextObjects[4].text = SatinAlmaText;
                    OperationButtons[0].interactable = false;
                }

            }
            else
            {
                MaterialsButtons[0].interactable = false;
                MaterialText.text = ItemText;
                TextObjects[4].text = SatinAlmaText;
                OperationButtons[0].interactable = false;
            }

            //-------------------------------------------------------------------------------------------------------------

            if (MaterialIndex != Materials.Length - 1) //yani eðer ben sona gelip geri gittiysem öne gitme butonunu tekrardan aktifleþtir
                MaterialsButtons[1].interactable = true;
        }
    }
    public void OpenOperationPanel(int Index) 
    {
        Sounds[0].Play();
        ControlTheSituation(Index);
        GeneralPanels[0].SetActive(true);
        ActiveOperationPanelIndex = Index;
        OperationPanels[Index].SetActive(true);
        GeneralPanels[1].SetActive(true);
        GeneralPanels[2].SetActive(true);
        OperationCanvas.SetActive(false);    
        ChooseText.SetActive(false);
    }
    public void GoBack() 
    {
        Sounds[0].Play();
        GeneralPanels[0].SetActive(false);
        OperationCanvas.SetActive(true);
        GeneralPanels[1].SetActive(false);
        GeneralPanels[2].SetActive(false);
        OperationPanels[ActiveOperationPanelIndex].SetActive(false);
        ControlTheSituation(ActiveOperationPanelIndex,true);
        ChooseText.SetActive(true);
    }
    public void BackToMainMenu()
    {
        Sounds[0].Play();
        _DataManagement.Save(_ItemDatas);
        SceneManager.LoadScene(0);
    }

    //--------------------------------------------------------------------

    void BuyResult(int index)
    {
        _ItemDatas[index].BuyingSituation = true;
        _MemoryManagement.SaveData_int("Point", _MemoryManagement.ReadData_int("Point") - _ItemDatas[index].Point);
        TextObjects[4].text = "Purchased";
        OperationButtons[0].interactable = false;
        OperationButtons[1].interactable = true;
        PuanText.text = _MemoryManagement.ReadData_int("Point").ToString();
    }
    void SaveResult(string key, int index)
    {
        _MemoryManagement.SaveData_int(key, index);
        OperationButtons[1].interactable = false;
        if (!_SavedAnimator.GetBool("ok"))
            _SavedAnimator.SetBool("ok", true);
    }
}
