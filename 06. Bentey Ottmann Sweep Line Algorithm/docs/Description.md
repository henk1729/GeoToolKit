### Bentley-Ottmann Sweep Line Algorithm
A sweep-line paradigm designed to solve the **Line Segment Intersection Problem** by avoiding naive $O(N^2)$ brute-force comparisons.

*   **Core Concepts:** Event Queue (Sorted Priority Queue), Status Structure (Balanced BST tracking active intersecting segments).
*   **Time Complexity:** $O((N + K) \log N)$ where $N$ is the number of segments and $K$ is the number of intersections.
*   **Real-World Use Cases:** Geographic Information Systems (GIS), CAD layer validation, path crossing checks.

Watch explanation video here: https://youtu.be/hRl5FZgN2Mk?si=sGEfp8ZsXLibtem7
