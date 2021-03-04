Ebben a mappában lévő file-okban az <b>1D cutting</b> optimization, vagy másképpen <b>1D vágás</b> optimalizálás témakörével foglalkozom. A témáról bővebben <a href="https://www.bferi.hu/download.php#cutter">itt található</a> leírás.<br>
A cutter.c és a cutter.cs file-okban lévő kódok az ismétléses permutáció algoritmusát használják a darabok megkeverésére. Itt látszik,
hogy ez a módszer biztos megtalálja az optimális megoldások egyikét, de csak nagyon kevés darabra használható.<br>
A cutterrnd.cs file-ban lévő kód a véletlen számokat használja. Kevés darabszám esetén ez is megtalálja az optimális megoldást, de közepes és nagy darabszámok esetén is talán elfogadható "megoldást" ad.
A saját programomba azért beépítettem egy ennél általában hatékonyabb megoldást is. De fontos kihangsúlyozni, hogy biztosra természetesen nem lehet menni.
Nyilván mindegyik "véletlen" szám generálása módjának különböző jellemzői vannak. Tehát az én programom sem képes mindenre. De a középső start gomb azért elég hatékonynak tűnik.
Azonban azt javaslom, hogy a felső és a középső start gombot is használjuk, mert van ahol a felső start gomb jobb megoldást ad. Bár a programom megengedi a párhuzamos futtatást,
én mégis a program kétszeri indítását szoktam használni, és ezt is javaslom.
