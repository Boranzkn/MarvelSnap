using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostToggles : MonoBehaviour
{
    [SerializeField] private Toggle costToggle01;
    [SerializeField] private Toggle costToggle2;
    [SerializeField] private Toggle costToggle3;
    [SerializeField] private Toggle costToggle4;
    [SerializeField] private Toggle costToggle5;
    [SerializeField] private Toggle costToggle6;

    public List<int> GetSelectedCostTogglesIndex()
    {
        var selectedCosts = new List<int>();

        if (costToggle01.isOn)
        {
            selectedCosts.Add(0);
            selectedCosts.Add(1);
        }

        if (costToggle2.isOn)
        {
            selectedCosts.Add(2);
        }

        if (costToggle3.isOn)
        {
            selectedCosts.Add(3);
        }

        if (costToggle4.isOn)
        {
            selectedCosts.Add(4);
        }

        if (costToggle5.isOn)
        {
            selectedCosts.Add(5);
        }

        if (costToggle6.isOn)
        {
            selectedCosts.Add(6);
            selectedCosts.Add(7);
            selectedCosts.Add(8);
            selectedCosts.Add(9);
            selectedCosts.Add(10);
        }

        return selectedCosts;
    }
}
