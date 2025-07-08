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

[Component(PropertyGuid = "6d1d551695de4b9e271c256f91f0be2b6def3df4")]
public class life_time : Component
{
	public float lifeTime = 5.0f;
	public float lifeTimeSpread = 1.0f;
	private void Init()
	{
		lifeTime += Game.GetRandomFloat(-lifeTimeSpread / 2, lifeTimeSpread/2);
	}
	
	private void Update()
	{
		lifeTime -= Game.IFps;
		if (lifeTime <= 0)
			node.DeleteLater();
		
	}
}