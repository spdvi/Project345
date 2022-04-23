# Project345

<!-----

Yay, no errors, warnings, or alerts!

Conversion time: 1.279 seconds.


Using this Markdown file:

1. Paste this output into your source file.
2. See the notes and action items below regarding this conversion run.
3. Check the rendered output (headings, lists, code blocks, tables) for proper
   formatting and use a linkchecker before you publish this page.

Conversion notes:

* Docs to Markdown version 1.0β33
* Sat Apr 23 2022 05:15:33 GMT-0700 (PDT)
* Source doc: Introducció a Unity: Sessió 3
----->



## Guió - Resumen Sessió 3


### <span style="text-decoration:underline;">PART 1</span>



* Crear nou projecte “Project345”.
* Afegir un plànol de 50x50m a l’origen. Nom “Ground”.
* Importar el paquet [magatzem.unitypackage](https://drive.google.com/file/d/1mzMm_QIG86xc-bJJ9k_PTbMYciTI4maF/view?usp=sharing).
* Afegir un magatzem o 2 a la escena.

FPS Player



* Afegir una capsule a la scene. Donar-li una height de 170 cm
* Afegir-li un component Rigidbody. Constraint rotation X,Y,Z.
* Crear el script PlayerController. Asociarlo al Player.
    * **Obtenir una referencia al rb del player en Start.**
    * Moure el player amb el rb.MovePlayer y vInput, hInput.
    * If moveSpeed > 10 it will pass through colliders.
* Afegir camera dins el Player. Fer-la main. Esborrar la main camera.
* Crear el script MouseLook. Adjuntar-lo a la càmera.
    * Mirar a esquerra i a dreta.
        * xRotation = Input.GetAxis(“Mouse X”);
    * Mirar a dalt i a baix.
        * yRotation = Input.GetAxis(“Mouse Y”);
        * Clamp rotation.
    * Quaternion rotation = Quaternion.Euler(transform.rotation * xRotation * rotateSpeed * Time.deltaTime);
    * rb.MoveRotate(rotation);

Il·luminació



* Directional light.
* Afegir llum al taller.
    * Point light.
    * Tip: Per situar un game object més fàcilment a l’escena, posar-se on el volem posar i, amb el GO seleccionat a la Hierarchy press Ctrl + Shift + F.
* Afegir un frontal al player.
    * Spot light.
    * Exercici(5’): Script per encendre i aturar el frontal del player amb la tecla L.
        * Sol·lució:
            * Crear un nou script i afegir-lo al “Spot Light”.
            * Obtenir una referència al component Light.
            * Activar o desactivar el component. 

Importing assets



* Models
    * [https://docs.unity3d.com/Manual/3D-formats.html](https://docs.unity3d.com/Manual/3D-formats.html)
* Asset store
    * Light switch
        * [https://assetstore.unity.com/packages/3d/props/free-homewares-asset-pack-142878](https://assetstore.unity.com/packages/3d/props/free-homewares-asset-pack-142878)
        * Add to my assets. Purchase
        * In Unity Editor, Open Package Manager Window
            * Packages: My assets
            * Select ‘Free homewares asset pack’.
            * Click Download, then Import.
    * Analitzar la carpeta: Materials, models, prefabs, textures.
* Posar un LightSwitchSingle a l’escena, al costat de la porta.
    * Exercici (10’): Script per encendre i apagar el llum quan el player ‘toqui’ el switch. Rotació del switch.
        * Sol·lució:
            * Afegir un Box Collider al “LightSwitchSingle” 
            * Crear un script i afegir-lo al “LightSwitchSingle”
            * Obtenir referencies a les llums.
            * Obtenir referencia al Transform del “Switch”
            * OnCollisionEnter
                * Activar/desactivar els llums i rotar el switch.


### <span style="text-decoration:underline;">PART 2</span>



* Importar asset workspace Tools
    * [https://assetstore.unity.com/packages/3d/props/industrial/workplace-tools-86242#description](https://assetstore.unity.com/packages/3d/props/industrial/workplace-tools-86242#description)
* Posar una Board a la paret. Escalar-la.
* Posar un martell, una serra, un berbiquí i una destral a la escena i col·locar-les (posició i rotació aprox.) com voldríem que quedin a la board.
* Modificar el nom i colliders de les Tools
    * Botó dret sobre Eina (p.ex. Axe) > Prefab > Unpack.
    * Deshabilitar o esborrar els colliders dels components interns de les eines i posar un box collider als parents.
    * Mesh colliders should not be used unless necessary. Change them.
* Modificar Saw (o tots els GO que no siguin compostos) i posar-lo dins un parent GO.
* Fer prefabs de les eines.
* Posar penjadors per les eines.
    * Crear un Empty GO > Penjadors. Posar-lo al 0,0,0. Fer-lo fill de la board. Tornar a resetejar el transform de Penjadors. Treure-lo de la board. Així fixem la posició al centre de la board. Modificar de nou rotació i escala.
    * Dins penjadors crear un cilindre “Penjador”. Modificar rotació z 90. Escala 0.01, 0.2, 0.2. Situar-lo al mig de la serra aprox.
    * Fer-lo prefab.
    * Posar penjadors per la resta de eines.
* Afegir text als penjadors.
    * Seleccionar el penjador de la serra.
    * Afegir GO > 3D object > Text - TMPro. Import TMP Essentials.
    * Rotar i Posicionar.
    * Hide gizmo icon.
    * Shader > TMPro > Distance Field SSD | Distance Field Overlay.
* Actualitzar el prefab de ‘Penjador’ per afegir el text als altres: Overrides.

Instanciar nous GameObjects en runtime.



* Al inicialitzar-se la escena (o al presionar una tecla), instanciar una eina random dins els límits del magatzem.
* Crear empty GO Spawn Eina. Crear i associar script SpawnEina.
* Afegir Rigidbody als prefabs de les eines per gravetat.
* List&lt;GameObjects>
* Random.Range
* Instantiate al presionar I
    * Primer al 0,5,0 p.ex.
    * Jugar a instanciar a un punt fix (sense gravetat) i amb diferents rotacions.
    * Després a un punt aleatori

Mirilla



* Crear nou objecte UI > canvas. Render mode: Screen space - Camera. Asignar la càmera del player.
* Canviar a mode 2D, enfocar el canvas i centrar-lo.
* Afegir dins un text amb un + en blanc.
* Centrar-lo en el canvas. Anchor center.

Outline



* Download & import Quick Outline.
    * [https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488](https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488)
* Fer còpia de l’script Outline i posar-lo a la nostra carpeta d’scripts. S’ha de moure també la carpeta Resources. Obrir-lo i canviar el color i fer que estigui enabled false per defecte.
* Afegir l’script Outline al prefab del penjador.
* Comprovar a l’editor l’efecte al habilitar-lo i deshabilitar-lo.

 

Ray casting



* Crear dos objectes primitius i situar-los a la escena. Instanciar un ray que vagi de un objecte a l’altre. Dibuixar-lo amb Debug.DrawRay. Entrar en Play mode i mirar a l’escena.
* Els rays es repinten (re-crean) a cada frame. L’hem de posar a l’Update.
* Crear un nou script OutlineObject. Associar-lo al Player (p.ex).
    * Crear un ray que vagi desde la càmera al mig de la pantalla. Camera.main.ScreenPointToRay
    * Physics.Raycast
        * RaycastHit
        * distance
        * LayerMask
    * Si un raig travessa un collider, habilitar el seu Outline.

Agafar, amollar i col·locar eines



* Afegir una mà al player.
* Script ToolHandler.
    * En el Update, si pitjam mouse 0
        * Si no tenim res en la mà
            * Si hi ha una eina Outlined, agafar-la 
        * Si ja tenim algo en la mà
            * Si no hi ha res outlined amollar la eina
            * Si hi ha algo outlined i eixe algo es un penjador (o no es una altra eina), situar-lo al penjador.
        * Necessitem accedir a la variable outlinedObject del script OutlineObject que hi ha al Player.
        * També afegir a les eines el tag “Eina” i als penjadors el tag “Penjador”
        * Rules: Un objecte penjat ja no es pot despenjar.
    * Problema amb la rotació i escala al colocar les eines als penjadors.
        * Fer el penjadors empty gos amb rotació 000 i escala 111 i posar dins els cilindres.
    * Truc per situar les eines als penjadors amb la rotació desitjada:
        * Destroy GO en la mà.
        * Instanciar al penjador un nou GO a partir del prefab.
            * El prefab sempre a la posició 000.
            * Si un asset ve amb una rotació diferent a la que volem, posar-lo dins un empty go amb rotació 000 i rotar el asset dins.
            * Quan s’instancïi ho farà en la posició i rotació per defecte del prefab.

Level Manager



* Quan es posin totes les eines, comprovar si estan ben posades.
* Si ho estan, passar al següent nivell.
* Si no, repetir.


### BONUS

Inspect mode

When left mouse is holded during 1 sec on a tool that is hanging on a penjador, enter inspect mode. Show loader wheel.

In inspect mode, move mouse and right click to move the camera. Left click and move mouse to rotate the tool.

