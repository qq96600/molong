using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class panel_smallWorld : Panel_Base
{
    private panel_plant _plant;
    public override void Hide()
    {
        base.Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        _plant=Find<panel_plant>("small_World/plantPlanting");
    }

    public override void Show()
    {
        base.Show();
        _plant.Show();
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
