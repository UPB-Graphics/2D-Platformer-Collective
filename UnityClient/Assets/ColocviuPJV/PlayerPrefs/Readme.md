`PlayerPrefs` is a class that stores Player preferences between game sessions. It can store string, float and integer values into the user’s platform registry.

---
A quick and easy way to implement a save system is to use Unity's built in PlayerPrefs class.
It is recommended for saving preferences/settings such video, audio, etc. However, if you only care about saving a few values, such as max score or a player's position (for example in a game such as Flappy Bird), this system can work fine. 

If more complicated datatypes need to be saved or too many enteties' properties, then a more robust system is needed.  

---
Note: if the example doesn't work as intended, try going to Edit -> Clear All PlayerPrefs

