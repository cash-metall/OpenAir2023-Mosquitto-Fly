/* Copyright (C) 2005-2023, UNIGINE. All rights reserved.
*
* This file is a part of the UNIGINE 2 SDK.
*
* Your use and / or redistribution of this software in source and / or
* binary form, with or without modification, is subject to: (i) your
* ongoing acceptance of and compliance with the terms and conditions of
* the UNIGINE License Agreement; and (ii) your inclusion of this notice
* in any version of this software that you use or redistribute.
* A copy of the UNIGINE License Agreement is available by contacting
* UNIGINE. at http://unigine.com/
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "d4c4b043af6a75153e89f73b3bfb13e1b9034e66")]
public class game : Component
{
	public float health = 0.5f;
	public float consumption = 0.01f;


	private WidgetCanvas canvas;
	private int text_id;
	private int poly_id;
	private WidgetDialogMessage dialogMessage;


    private void Init()
	{
		Visualizer.Enabled = true;
		// write here code to be called on component initialization

		Gui gui = Gui.GetCurrent();
		canvas = new WidgetCanvas(gui);
		gui.AddChild(canvas, Gui.ALIGN_OVERLAP | Gui.ALIGN_EXPAND);

		text_id = canvas.AddText();
		canvas.SetTextSize(text_id, 42);
		canvas.SetTextColor(text_id, vec4.WHITE);
		canvas.SetTextOutline(text_id, 1);
		canvas.SetTextPosition(text_id, new vec2(50, 50));

		poly_id = canvas.AddPolygon();
		canvas.AddPolygonPoint(poly_id, new vec3(0, 0, 0));
		canvas.AddPolygonPoint(poly_id, new vec3(1, 0, 0));
		canvas.AddPolygonPoint(poly_id, new vec3(1, 1, 0));
		canvas.AddPolygonPoint(poly_id, new vec3(0, 1, 0));
		canvas.SetPolygonColor(poly_id, vec4.RED);

		canvas.SetPolygonTransform(poly_id, MathLib.Translate(40, 40, 0) * MathLib.Scale(800, 70, 0));

	}

	private void Update()
	{
		int w = WindowManager.MainWindow.ClientSize.x - 100;

		canvas.SetPolygonTransform(poly_id, MathLib.Translate(40, 40, 0) * MathLib.Scale(w * health, 70, 0));

		canvas.SetTextText(text_id, string.Format("Health: {0}", (int)(health * 100)));



		if (health < 0 && !dialogMessage)
		{
			Game.Scale = 0;
			dialogMessage = new WidgetDialogMessage();
			Gui.GetCurrent().AddChild(dialogMessage, Gui.ALIGN_CENTER);
			dialogMessage.MessageText = "WASTED!";
			dialogMessage.Text = "test123";
			dialogMessage.OkText = "Reload";
			dialogMessage.CancelText = "Quit";
			dialogMessage.GetOkButton().AddCallback(Gui.CALLBACK_INDEX.CLICKED, () =>
			{
				Unigine.Console.Run("world_reload");
			});
			dialogMessage.GetCancelButton().AddCallback(Gui.CALLBACK_INDEX.CLICKED, () =>
			{
				Unigine.Engine.Quit();
			});
		}
		else
		{
			health -= consumption * Game.IFps;
		}

	}
	private void Shutdown()
	{
		if (dialogMessage)
			dialogMessage.DeleteLater();
		canvas.DeleteLater();
		canvas = null;
	}
}