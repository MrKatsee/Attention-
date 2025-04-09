using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WinApiTest : MonoBehaviour
{
    [SerializeField]
    private Button _onToggleButton;
    [SerializeField]
    private Button _selectButton;
    [SerializeField]
    private Dropdown _selectDropdown;


    private void Start()
    {
        _selectButton.onClick.AddListener(getList);
    }

    private void getList()
    {
        WinApiController.Instance.GetWindowList();
        setDropdownOptions(WinApiController.Instance.WindowList);
    }

    private void setDropdownOptions(List<WindowData> list)
    {
        _selectDropdown.ClearOptions();
        _selectDropdown.AddOptions(list.Select(s => s.title).ToList());

    }

     
}
