using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class InputData : MonoBehaviour
{
    
    [SerializeField] private InputField inputName;
    [SerializeField] private InputField inputImg;
    [SerializeField] private InputField inputAtk;
    [SerializeField] private InputField inputDefense;
    [SerializeField] private InputField inputLevel;
    [SerializeField] private InputField inputAbilityValue;
    [SerializeField] private InputField inputDescription;

    [SerializeField] private Text txtName;
    [SerializeField] private Text txtImg;
    [SerializeField] private Text txtAtk;
    [SerializeField] private Text txtDefense;
    [SerializeField] private Text txtLevel;
    [SerializeField] private Text txtAbilityValue;
    [SerializeField] private Text txtDescription;




    Dictionary<string, string> itemInformation = new Dictionary<string, string>();

    public void InputName()
    {
        itemInformation.Remove("Name");
        itemInformation.Add("Name", inputName.text);
        txtName.text = inputName.text;
    }

    public void InputImage()
    {
        itemInformation.Remove("Img");
        itemInformation.Add("Img", inputImg.text);
        txtImg.text = inputImg.text;
    }
    public void InputAttack()
    {
        itemInformation.Remove("Atk");
        itemInformation.Add("Atk", inputAtk.text);
        txtAtk.text = "공격력 : " +inputAtk.text;
    }
    public void InputDefense()
    {
        itemInformation.Remove("Defense");
        itemInformation.Add("Defense", inputDefense.text);
        txtDefense.text = "방어력 : " + inputDefense.text;    
    }
    public void InputRestrictionLevel()
    {
        itemInformation.Remove("Level");
        itemInformation.Add("Level", inputLevel.text);
        txtLevel.text = "착용 레벨 : " + inputLevel.text;
    }    
    public void InputAbilityValue()
    {
        itemInformation.Remove("AbilityValue");
        itemInformation.Add("AbilityValue", inputAbilityValue.text);
        txtAbilityValue.text = "능력치 : " + inputAbilityValue.text;
    }    
    public void InputDescription()
    {
        itemInformation.Remove("Description");
        itemInformation.Add("Description", inputDescription.text);
        txtDescription.text = inputDescription.text;
    }


}
