Hello! To advance through the dialogue just press the "D" key
Welcome to our RPG Dialogue & Relationship Management Demo! 
To test this, just advance through the dialogue. 
Different choices have different effects on the relationship meter. 
To select a choice, just press one of the buttons.
Try it out now!

 * You: That sounds kinda cool.
    -> positiveChoice_1
 * You: That sounds kinda stupid.
    -> negativeChoice_1

=== positiveChoice_1 ===
Thank you, we appreciate that!
 -> explanation
 
 === positiveChoice_2 ===
Apology accepted!
 -> explanation

  === positiveChoice_3 ===
No problem!
 -> finish
 
 === negativeChoice_1 ===
That's a mean thing to say, but you only said that to test the demo, right?
    *You: Yup, sorry. 
    -> positiveChoice_2
    *You: Nope, I did it on purpose to be mean. I'm a terrible, terrible person.
    -> negativeChoice_2

=== negativeChoice_2 ===
:(
-> explanation

=== negativeChoice_3 ===
For example, that was a choice with negative value!
-> finish

=== explanation ===
Each choice you make has a certain negative or positive value.
You can see your relationship meter below - red means bad, green means good!
    *You: Makes sense, thanks for explaining. 
    -> positiveChoice_3
    *You: I could tell that without any explanations. 
    -> negativeChoice_3
    

 === finish ===
Thank you for going through our RPG Dialogue & Relationship Management Demo!
-> END
