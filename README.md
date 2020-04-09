# FaceCompare
Optoelectronica - Server and client demo app
1. Aplicatia are nevoie de Microsoft Visual C++ Redistributable for Visual Studio 2017
2. Se porneste aplicatia server, dupa care aplicatia client (demo)
3. Aplicatia server este o aplicatie de tip console, wcf self hosted
4. Aplicatia client este o aplicatie de tip asp.net care ruleaza din browser
5. Aplicatia client apeleaza serviciile serverului pentru upload si compararea imaginilor.
6. In aplicatia client exista o functie de explorare si incarcare a fisierelor de tip imagine (jpg)
7. Prima imagine care trebuie incarcata (upload) pe server este cea cu care se face compararea (imaginea de baza)
8. Urmatoarele imagini incarcata se pot compara cu imaginea de baza pentru a verifica compatibilitatea intre fetele personajelor din cele 2 imagini incarcate.
9. Incarcarea celei de-a doua imagine (nu cea de baza), are ca efect si compararea/recunoasterea faciala.
10. In cazul in care recunoasterea fetei este pozitiva, aplicatia intoarce un mesaj:
"Face Compare - Sistemul gaseste multe similitudini identificand fata ca apartinand aceleiasi persoane: Small numJitters: 2"
11. In cazul in care recunoasterea fetei personajului nu este pozitiva aplicatia va intoarce mesajul:
"Sistemul gaseste prea putine similitudini identificand fata ca apartinand altei persoane".
12. Aplicatia poate fi folosita si testata la
http://13.93.24.190/FaceRecognition
