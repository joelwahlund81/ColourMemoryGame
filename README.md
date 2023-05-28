# ColourMemoryGame

Approx time to get code into current state: **3 hours**<br />
Time spent sporadically during weekend so hard to really tell for sure.

- Prepping: 50 min (gathering requirements).
- Creating project: 2 hours 10 min. 
  - Implementing backend: 1 hour 30 min.
  - Implementing frontend: 30 min.

Used Postman as a external tester against API, collection is added in the repo.

## Requirements
Decision was made to separate business logic into backend layer, and open it through a API.
The technique is known, and getting a working backend state could (theoretically be made into a Nuget package later on).

Start-up:
When a new game is played, the backend is called, which delivers an object with game info

Frontend needs to include:
- Scoreboard
- 4x4 patterns (16 squares in total)
- A total of 8 colors mixed (backend keeps the colors)
- New game button

Frontend functionality:
- Turn over one card at a time, get the color from the backend via Id
- When two cards are selected:
  - Add locked "state" so no card can be clicked
  - Check for matching color, call backend
  - If the cards match
    - Find the matching cards and "remove them"
  - If not match
    - Turn the cards over
  - Check results that all cards are clicked, if they are:
    - Share results
    - Display "New game" button.

Backend functionality:
- Start new game
- Get a game
- Check for color on card via ID
- Check for match between two cards
  - If match, those cards can no longer be used and are set as inactive
  - If no match, set score to minus two, and return
- Checks if all cards are selected, then the game is locked and the game is over

## Results

Backend:
Functionality operational. Minor glitches might still be present.

Frontend
Non working. Found examples of grid system and being able to click to flip:
* https://stackblitz.com/edit/react-card-grid?file=index.js
* https://codepen.io/mondal10/pen/WNNEvjV

Got stuck on CORS and estimated due to lack of time the decision was made to get a at least working backend that served as a POC.

Additional ideas:
V2:
- Unit tests
- Better error messages to client (based on logging)
- Hide color on frontend models (no cheating)
- Backend can save multiple games
  - Change to SQL 
- Separate entities and communicationsmodels

V3
- At refresh, keep session and latest played game is received
- Clock that showes time
- Statistics for missed pairings
- "Surrender" button

V4
- Save users and their games
- Ask for a shuffle during game, for extra points
  - More points for each non flipped card?
- Have difficulty settings for increased points
	- Time
	- Amount of grids

V5
- Highscore 

## Own verdict
Rating: 6/10
Satisfaction: Moderate
Mood: Stressed and joyful
