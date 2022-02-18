Am implementat un Combat system de baza.

In script-ul PlayerCombat, am implementat o functie simpla de atac, in care
utilizatorul ataca folosind mouse-ul. In cazul in care sunt inamici in zona,
acestia primesc damage. Jucatorul poate fi lovit si poate muri. Damage-ul
aplicat inamicului se calculeaza in functie de puterea atacului, puterea
player-ului, modificatorului de atac, daca e cazul, si nivelul de defensiva
al inamicului, precum si de puterea armei jucatorului.

In script-ul EnemyCombat, am implementat functionalitati similare cu
player-ul, dar am trecut stats-urile in acelasi script, gandindu-ma ca
inamicul nu are stats-uri atasate de gameObject.

In script-ul WeaponStats, am implementat stats-urile posibile pe care le poate
avea o arma, si anume un attackPower, attackRange, attackRate sau enchantLevel.