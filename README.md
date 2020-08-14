# Sudoku Game
## [Install Game](https://drive.google.com/file/d/1LzHC1rFJpgXe01hrTVUF-LNOknqQcwjK/view?usp=sharing) (Windows x86 only)
## Tools and Languages Used

<img align="left" alt="TheSameerAhmed | LinkedIn" width="22px" src="https://cdn.jsdelivr.net/npm/simple-icons@3.4.1/icons/csharp.svg">
<img align="left" alt="TheSameerAhmed | LinkedIn" width="22px" src="https://cdn.jsdelivr.net/npm/simple-icons@3.4.1/icons/unity.svg">
<img align="left" alt="TheSameerAhmed | LinkedIn" width="22px" src="https://cdn.jsdelivr.net/npm/simple-icons@3.4.1/icons/visualstudio.svg"> <br>

## Features

- 30 puzzle sudoku game with 3 [difficulty levels](/Sudoku/Assets/Scripts/DifficultyLevel.cs) (Easy, Medium and Hard).
- Includes a [timer](/Sudoku/Assets/Scripts/Timer.cs) and a [best timings panel](/Sudoku/Assets/Scripts/BestTimes.cs) to keep track of the fastest time taken to solve a given puzzle.
- Solution of each puzzle is derived dynamically using a [backtracking algorithm.](/Sudoku/Assets/Scripts/PuzzleManager.cs)
- A [.txt parser](/Sudoku/Assets/Scripts/DownloadSudokuPuzzles.cs) that inputs given puzzles into the model of this game from a .txt file of sudoku puzzles.
- [User control](/Sudoku/Assets/Scripts/InputController.cs) can be handled by both the keyboard and mouse.
- Includes [Game Audio](/Sudoku/Assets/Scripts/AudioManager.cs) and [Animation](/Sudoku/Assets/Scripts/AnimationManager.cs) to denote events such as after completion of puzzle.

## User Interface

- A dark color theme right across the game, with neon green text.
- Selected box is highlighted with grey to denote the respective box in the puzzle.
- Basic [main menu](/Sudoku/Assets/Scripts/EnterGame.cs) that allows the user to get into the game, read instructions of a 9x9 sudoku puzzle or quit the game.

## Game Play

- To start completing the puzzle, simply select an empty box in the puzzle and enter a number between 1-9.
- Numbers entered can be deleted with the delete, backspace or escape keys on the keyboard.
- Individual or all puzzles can be [reset](/Sudoku/Assets/Scripts/NextAndPrevious.cs) to the original puzzles.

## ScreenShots from the game
<details>
  <summary>Click to view images </summary>
  
Main Menu                  |  Difficulty Level
:-------------------------:|:-------------------------:
![](/Sudoku/GameScreenShots/MainMenu.jpg)  |  ![](/Sudoku/GameScreenShots/DifficultyLevel.jpg)

Game UI                    | Game UI After Puzzle is Solved
:-------------------------:|:-------------------------:
![](/Sudoku/GameScreenShots/GameMode.jpg)  |  ![](/Sudoku/GameScreenShots/GameModeAfterSolving.jpg)

Puzzle Incorrectly Solved  |  Puzzle Solved
:-------------------------:|:-------------------------:
![](/Sudoku/GameScreenShots/NotSolved.jpg)  |  ![](/Sudoku/GameScreenShots/SolvedPuzzle.jpg)

Best Times Recorded        |  Game Paused
:-------------------------:|:-------------------------:
![](/Sudoku/GameScreenShots/BestTimes.jpg)  |  ![](/Sudoku/GameScreenShots/GamePaused.jpg)

</details>

## Acknowledgments

- :musical_note:  Background Music  <a href="https://www.youtube.com/watch?v=U0YSWmmKhDw"><img alt="Youtube Link | Game Music" width="22px" src="https://cdn.jsdelivr.net/npm/simple-icons@3.4.1/icons/youtube.svg"> </a> <br> Youtube Channel: <a href="https://www.youtube.com/channel/UCwgDTjnlwlalaWXooGlnMrQ"> KickTracks </a> <br>

- :musical_note: Sound effects obtained from <a href="https://www.zapsplat.com">Zaplast </a>

- :video_game: Puzzles and Game instructions obtained from <a href="https://www.websudoku.com/"> Web Sudoku </a>


