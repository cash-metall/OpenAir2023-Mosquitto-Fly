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

[Component(PropertyGuid = "171732237b9515da9d5c5d1b4416a70e10e19ec6")]
public class spawner : Component
{
	public vec3 box = new vec3(50,50,5);
	public int count = 10;


	public List<bonus> bonuses = new List<bonus>();

	private List<bonus> spawned = new List<bonus>();

	private void generateRandom()
	{
        int index = Game.GetRandomInt(0, bonuses.Count);
        Node n = bonuses[index].node.Clone();
        spawned.Add(n.GetComponent<bonus>());
        n.WorldPosition = node.ToWorld(new vec3(Game.GetRandomFloat(-box.x / 2, box.x / 2), Game.GetRandomFloat(-box.y / 2, box.y / 2), Game.GetRandomFloat(-box.z / 2, box.z / 2)));
        n.Enabled = true;
	}

	private void Init()
	{
		// write here code to be called on component initialization
		
		foreach (var bonus in bonuses) 
		{
			bonus.node.Enabled = false; 
		}


		for (int i = 0; i < count; i++)
		{
			generateRandom();
        }

	}
	
	private void Update()
	{
		Visualizer.RenderBox(box, node.WorldTransform, vec4.BLUE);

		foreach (var bonus in spawned)
		{
			if (!bonus.node)
				spawned.Remove(bonus);
		}

        for (int i = spawned.Count; i < count; i++)
        {
            generateRandom();
        }


        // write here code to be called before updating each render frame

    }
}