### Andrew's Monotone Chain Algorithm
An efficient, elegant method for computing the **Convex Hull** (minimum bounding envelope) of a distinct 2D point cloud cloud.

*   **Core Concepts:** Coordinate Sorting, Split-Hull Processing (Upper Chain & Lower Chain processing), Orientation Turn Checks via 2D cross-products.
*   **Time Complexity:** $O(N \log N)$ average/worst-case performance driven strictly by initial coordinate sorting.
*   **Real-World Use Cases:** Collision hitbox optimization in game engines, computer vision contouring (`cv2.convexHull`), cluster outline extraction.

Watch explanation video here: https://youtu.be/-JyEDBKW2yE?si=p99pHDWku5dOT0Vh
