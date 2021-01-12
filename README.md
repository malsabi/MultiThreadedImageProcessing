# Multi Threaded Image Processing 

### About
Used for streaming purposes to acheive 60 FPS and above by sending the changed regions (delta's) that can be represented in a block, the block size can be determined by the user.

### Recommended Block Sizes
| Block Size  | Pixel Size (bytes) |   Pixel Format  |
| ----------- | -------------      | -------------   |
| 120         | 4                  |      RGBA       |
| 60          | 4                  | RGBA            |
