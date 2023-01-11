
function nyelvCsere()
    {  
        if (document.getElementById("nyelv").value =="angol") 
        {
        document.getElementById("cim").innerHTML="Labyrinth Game"
        document.getElementById("elsoBlokk").innerHTML="When you start the game program, the game asks for the path to the map file. If a previous game is saved, the user has the option to continue or start a new game. When starting a new game, the user can choose whether to play with an overlaid map or a time line, and can specify the time available. The objective of the game is to get to all the treasure rooms and then exit through one of the exits. To make it more difficult, you can play in exploration mode, in which case only the corridors you have already explored are visible. The game will display the name and size of the corridor, the time elapsed or remaining, the number of steps taken and the number of rooms explored, and where the player can move. Using the number keys, the player can start a new game, save their current position, switch between English and Hungarian, or quit.";
        document.getElementById("jatek").innerHTML="(Game program)"
        document.getElementById("jatekpro").innerHTML="Game program"
        document.getElementById("palya").innerHTML="(Map editing)"
        document.getElementById("palyaszer").innerHTML="Map editing"
        document.getElementById("csapatcim").innerHTML="Team members:"
        document.getElementById("web").innerHTML="(Web page)"
        document.getElementById("masodikBlokk").innerHTML="In the main menu of the track editor, you can choose between three options: create track, load track, exit the program. Then you can specify the size of the matrix. In this menu we have the following options: back to menu, empty track, delete field, save track, place character on track"
        }
        else if(document.getElementById("nyelv").value=="magyar")
        {
        document.getElementById("cim").innerHTML="Labirintus játék"
        document.getElementById("elsoBlokk").innerHTML="A játékprogram indításakor a játék bekéri a térkép fájl elérési útját. Ha egy előző játék el van mentve, a felhasználónak lehetősége van folytatni, vagy új játékot indítani. Új játék indításakor a felhasználó kiválaszthatja, hogy szeretne-e fedett térképpel vagy időkorláttal játszani, valamint megadhatja rendelkezésre álló időt.A játék célja eljutni az összes kincsesterembe (’█’), majd kijutni az egyik kijáraton. Nehezítésként lehet felfedező módban játszani, ekkor csak a már korábban bejárt folyosók láthatóak.játék kiírja a pálya nevét és méretét, az eltelt vagy hátralévő időt, a megtett lépések és felfedezett termek számát, valamint hogy merre léphet a játékos. A számbillentyűk használatával a játékos indíthat új játékot, elmentheti a jelenlegi állását, válthat angol és magyar között, vagy kiléphet.";
        document.getElementById("jatek").innerHTML="Játék Program"
        document.getElementById("jatekpro").innerHTML="Játék Program"
        document.getElementById("palya").innerHTML="(Pályaszerkesztés)"
        document.getElementById("palyaszer").innerHTML="Pályaszerkesztés"
        document.getElementById("csapatcim").innerHTML="Csapat tagjai:"
        document.getElementById("web").innerHTML="(Weboldal)"
        document.getElementById("masodikBlokk").innerHTML="A pályaszerkesztő program főmenüjében három lehetőség közül választhatunk ezek a pálya létrehozása, pálya betöltése, kilépés a programból. Ezután megadjuk a mátrix méretét. Ebben a menüben a következő választási lehetőségeink vannak: vissza a menübe, üres pálya, mező törlése, pálya mentése, karakter elhelyezése a pályán"
        }

    
    }