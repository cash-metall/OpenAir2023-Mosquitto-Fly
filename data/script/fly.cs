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

[Component(PropertyGuid = "520339a0358e9b3d6eeac84864125353fd298617")]
public class fly : Component
{
	public float forward_force = 2f;
	public float up_force = 2f;
	public float torque_force = 2.0f;
	public float anti_gravity = 9.1f;


    private void Init()
	{
		// write here code to be called on component initialization
		
	}
	
	private void Update()
	{
		// write here code to be called before updating each render frame
		
	}

	private void UpdatePhysics() 
	{
		var body = node.ObjectBodyRigid;

		vec3 dir = vec3.UP * anti_gravity;

		if (Input.IsKeyPressed(Input.KEY.W))
			 dir += node.GetWorldDirection(MathLib.AXIS.Y) * forward_force;
		if (Input.IsKeyPressed(Input.KEY.S))
			dir += node.GetWorldDirection(MathLib.AXIS.Y) * -forward_force;


		if (Input.IsKeyPressed(Input.KEY.SPACE))
			dir += node.GetWorldDirection(MathLib.AXIS.Z) * up_force;
		if (Input.IsKeyPressed(Input.KEY.ANY_CTRL))
			dir += node.GetWorldDirection(MathLib.AXIS.Z) * -up_force;


		body.AddWorldForce(node.ToWorld(vec3.UP * 0.5f), dir);
		Visualizer.RenderVector(node.ToWorld(vec3.UP * 0.5f), node.ToWorld(vec3.UP * 0.5f) + MathLib.Normalize(dir), vec4.BLUE, 0.25f, false, 0.02f, true);
		Visualizer.RenderVector(node.WorldPosition, node.WorldPosition + body.LinearVelocity, vec4.GREEN, 0.25f, false, 0.02f, true);

		if (Input.IsKeyPressed(Input.KEY.A))
			body.AddTorque(vec3.UP * torque_force);
		if (Input.IsKeyPressed(Input.KEY.D))
			body.AddTorque(vec3.UP * -torque_force);
    }
}