# Components 
- Input Parser
  - File Reader
  - line 1 - Top Right Surface location. will define the dimentions of the surface grid
  - line 2 - Mower Start position & Orientation
  - line 3 - Mower movement. 
  - repetition of line 2 & 3 for each subsequent mower on the surface.

- Out Put Parse
  - each line is the final mower position and orientation. MOST be in the same order as input.
  - out put type is not specified. File? Console? 

- Surface Grid / Map
  - maintain a single array of the grid for perfornace instead of a 2d array
- Cartesian coordinates
  - supprot IComaprrer ?? 
- Orientation
- Movement
 - rotation
 - forward
 - within surfcae
 - collision detection
- Mower

# Parallel 
- Input parses. Could read / generate mawers in parallel
- mower manager.
  - get next move for all mowers
  - Collision evaluation
  - execute mower move