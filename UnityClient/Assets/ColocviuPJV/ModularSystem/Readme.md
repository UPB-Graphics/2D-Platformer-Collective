# Serialization model

### Saving Data
Data we want to save -> serialize it -> write it out to a file

### Loading Data
Load file -> populate in-game data with data from file


## Diagrama proiect
![diagram](Art/UML-PJV-Save-System.png)


### Overview utilizare 

Fiecare entitate, Player, Enemies, Pickups, etc. va avea o clasa asociata ce va tine datele ce trebuiesc persistate. Ex. pt. Enemy, vom avea o clasa EnemyData.

Vom avea o clasa ce agrega toate datele pe care le vrem persistate. 
Ex: GameData - va contine toate instantele de EnemyData

Vom avea o clasa ce va trece prin fiecare entitate, va extrage EntityData si va popula GameData. 
Apoi va serializa GameData si va salva datele rezultate intr-un fisier, in PlayerPrefs, etc. 
In diagrama, numele clasei este GamePersister. 
GamePersister va avea 2 metode: Save si Load
Pt a permite mai multe salvari, Save si Load vor primi un parametru: save_number. 

Cand metoda Save este rulata, se va trece prin fiecare entitate, se vor lua datele si se va popula GameData. 
Dupa care GameData va fi serializat si scris intr-un fisier. 

Cand metoda Load este rulata, se va deschide fisierul, datele vor fi de-serializate, se va trece prin fiecare entitate si datele entitatii vor fi populate.


---
Astfel, avem un sistem de Save Game / Load Game ce necesita o clasa de date pt fiecare entitate dar care poate fi organizat relativ usor.  