#pragma once

#include "vector2.h"
#include "list.h"

// Collision Polygon
class Polygon
{
	public:
		List* Points;
		List* Edges;
		Vector2* Centre;

		Polygon();
		void Compute();

		bool HitTest(Vector2* Point);
		bool DoesCollide(Vector2* Velocity, Polygon* CheckWith);
		Polygon* Translate( Vector2* Offset, int FlipX, float Rotation );
		Polygon* Translate( float OffsetX, float OffsetY, int FlipX, float Rotation );

		void Project( Vector2* axis, float* min, float* max );
		float IntervalDistance(float minA, float maxA, float minB, float maxB);
};
