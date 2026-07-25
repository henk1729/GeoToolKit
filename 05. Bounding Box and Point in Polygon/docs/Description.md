### Introduction
How do we determine if a delivery partner is inside a city, an area or a colony?

### Description
1. Bounding Box (bbox):
  * A bounding box of a polygon (representing a city/ an area) is the smallest rectangle, with its sides parallel to the axes, that contains the entire polygon.
  * If a point lies outside the bbox, it definitely lies outside the polygon thus eliminating the effort to do complex math.

2. Ray Casting Algorithm:
  * The algorithm says that when an infinite ray is casted horizontally from the point under consideration to its right and made to intersect the polygon under consideration, then the location of the point with resepct to the polygon can be determined by the parity of the number of intersections.
  * If the parity is even, the point lies outside else it lis inside.
  * If the ray grazes the polygon edge, is collinear with an edge or just hits a point, then it is not counted as an intersection.

### Final Flow
1. Calculate bbox.
2. Check if the point is outside. If yes, stop, else continue.
3. Implement the ray casting algorithm.
4. Count the no. of intersections. If they are even, the point is outside, else inside.
