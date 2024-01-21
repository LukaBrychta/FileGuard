Program po spuštění detekuje změny v adresáři.

Na vstupu přijme cestu k adresáři. Na základě této cesty si vytvoří textový soubor, který v názvu obsahuje i cestu k adresáři. To umožňuje analyzovat i více adresářů a mít pro každý adresář vlastní textový soubor, do kterého se ukládájí informace o souborech (cesta k souboru, číslo verze, čas poslední změny). Po úspěšném uložení dat se pro přehlednost cesta k textovém souboru vypíše.

Při dalším spuštění se porovnávají změny v adresáři a uložené informace v textovém souboru. A na základě toho program vyhodnotí soubor jako: nový, smazaný nebo upravený (podle změny času poslední úpravy).

Program je tvořen především třídou Database, která vyhodnocuje a pracuje se soubory a třídou itemFile, která reprezentuje informace o souboru.

Nevýhodou je, že pokud je soubor v adresáři přejmenován nebo přesunut, program to vyhodnotí tak, že byl jeden soubor smazán a jeden nově přidán.


![FileGuard-2](https://github.com/LukaBrychta/FileGuard/assets/134295729/98366e31-bb00-4402-ba2f-34e6f77495b1)
![FileGuard-1](https://github.com/LukaBrychta/FileGuard/assets/134295729/1bb29e39-d81c-4900-9010-e14649e5628d)
