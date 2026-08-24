### Quadtrees (C# & Raylib Simulation)
A 2D spatial partitioning index that recursively divides a dimensional coordinate space into specialized regional quadrants.

*   **Core Concepts:** Recursive Subdivision (NW, NE, SW, SE), Node Capacity Limits, Fast Spatial Boundary Range Queries.
*   **Time Complexity:** $O(\log N)$ for localized spatial lookups, degrading to linear $O(N)$ inside highly skewed, unbalanced clusters.
*   **Real-World Use Cases:** Video game collision detection engines, spatial database indexing, image compression, proximity calculations.

Watch explanation video here: https://youtu.be/3KM3pvHs0Pk?si=GTzFIKQNV00ONFh_
