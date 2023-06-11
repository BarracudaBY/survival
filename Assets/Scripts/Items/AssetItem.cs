using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName ="Item")]
public class AssetItem : ScriptableObject, IItems
{
    public string Name => _name;

    public Sprite UIIcon => _uiIcon;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;
}
