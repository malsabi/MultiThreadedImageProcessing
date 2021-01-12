# Multi Threaded Image Processing 

### About
Used for Remote Desktop streaming purposes to acheive 60 FPS and above by sending the changed regions (delta's) that can be represented in a blocks, the block size can be determined by the user.

### Algorithm
1. Split the Screen into Upper and Lower by deviding the height by 2.
2. Process the Upper and Lower parallel by adding them into a thread pool and wait for them to be finished to sum the results.
3. The Upper process, contains two different phases for scanning.
   - Scan row by row and add the changed row into the list.
   - Scan each row by a specific block size and add the block into the final list that will be returned back to the main caller.

### Recommended Block Sizes
| Block Size  | Pixel Size (bytes) |   Pixel Format  |  Internet Speed |
| ----------- | -------------      | -------------   |  -------------  |
| 120         | 4                  |      RGBA       |     120 Mbps +  |
| 60          | 4                  |      RGBA       |     80  Mbps    |
| 30          | 4                  |      RGBA       |     60  Mbps    |
| 120         | 3                  |      RGB        |     100 MBps    |
| 60          | 3                  |      RGB        |     60  Mbps    |
| 30          | 3                  |      RGB        |     50  Mbps    |

Maximum block size can be up to 200, any more than that it would be redundant.
