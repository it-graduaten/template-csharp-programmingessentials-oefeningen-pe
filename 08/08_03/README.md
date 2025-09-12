## 08.03
*Deze oefening is een uitbreiding op oefening 05.05*

Lees voor 3 pijlen de landingsplaats en geef vervolgens een totaal van de punten weer. Ingevoerde waardes moeten gehele getallen zijn die groter zijn dan 0 en kleiner of gelijk aan 4. Na een correcte invoer, word een nieuw scherm getoond.

- Buiten de roos: 0 punten (landingsplaats 1) 
- Binnenste ring: 20 punten (landingsplaats 2) 
- Buitenste ring: 50 punten (landingsplaats 3) 
- In het hart: 100 punten (landingsplaats 4)

Werk eerst volgende verplichte methodes uit:

`int ReadNumber(int min, int max)`
: Deze methode vraagt de gebruiker om een geheel getal in te geven binnen de bepaalde grenzen. Indien de gebruiker een foutieve waarde (string, lege string, ..) ingeeft, wordt de vraag herhaald. Returnwaarde is het ingegeven getal.

`int CalculateScore(List<int> throws)`
: Deze methode berekent de totaalscore op basis van de worpen die de gebruiker heeft gedaan. Returnwaarde is de totaalscore.

`void ShowScore(int score)`
: Deze methode zonder returnwaarde print de score van de gebruiker. Bekijk de voorbeelden voor de juiste tekst.

| Input aan de console | Verwachte output |
|----------------------|------------------|
| 3<br>3<br>3 | U hebt 150 punten behaald! |
| 6<br>4<br>3<br>2 | U hebt 170 punten behaald! |