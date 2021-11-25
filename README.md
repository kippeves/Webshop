# Webshop Readme

## För att kunna köra programmet:

# 1. Ändra pathen för JSON-filerna:

Sökvägarna som skall ändras ligger i konstruktorn för ***Webshop.Data.DataSource_JSON***. 
Sökvägarna ligger i Dictionary:t _PATHS på rad 18.

Filerna ligger i mappen json under projektet ***Webshop.Data***, d.v.s. samma projekt som klassen ligger i. I Solution Explorer ligger de precis ovanför. 

Filerna:
```
1. Receipts.json
2. Orders.json 
3. Carts.json
```
kan tömmas om du vill se hur systemet hanterar tomt material. Om du gör det så måste du dock göra en tom JSON-array, då det inte finns någon kontroll mot tomma filer.

# 2. Funktionalitet

För att logga in är det bara att välja en användare i dropdown-menyn uppe till höger. Sen är det bara att lägga till hur många produkter du vill.
Det finns ingen adresshantering, du väljer bara kort, och skriver in lösenord. Slutligen så ser du kvittot om du väljer Kvitton i användarmenyn.

