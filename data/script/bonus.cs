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

[Component(PropertyGuid = "a87233cc05d3adf78d25a3f4edb734e2c5129f53")]
public class bonus : Component
{
	public Node lookAtNode;
	public Component effectComponent;
	public float radius = 1f;
	private fly player;
	private dvec3 node_pos;
	private ShapeSphere sphere;

	private bool need_to_destoroy = false;

	private void Init()
	{
		node_pos = node.WorldPosition;
		sphere = new ShapeSphere();
		sphere.Radius = radius;
		sphere.Position = node_pos;
		player = ComponentSystem.FindComponentInWorld<fly>();
		// write here code to be called on component initialization
		sphere.CollisionMask = 3;

	}
	
	private void Update()
	{
		if (need_to_destoroy)
		{
			node.DeleteLater();
			return;
		}

		Visualizer.RenderSphere(radius, MathLib.Translate(node.WorldPosition), vec4.BLUE);

		if (lookAtNode)
		{
			dvec3 look_at_pos = lookAtNode.WorldPosition;
			look_at_pos.z = node_pos.z;
			node.WorldTransform = MathLib.SetTo(node_pos, look_at_pos, vec3.UP, MathLib.AXIS.Y);
		}

		List<ShapeContact> contacts	= new List<ShapeContact>();
		sphere.GetCollision(contacts);
		if (contacts.Count > 0)
		{
			foreach (ShapeContact contact in contacts)
			{
				if (contact.Object == player.node)
				{
					effectComponent.Enabled = true;
					need_to_destoroy = true;
				}
			}
		}
	}
}