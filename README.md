# Project345

<!-----

Yay, no errors, warnings, or alerts!

Conversion time: 0.654 seconds.


Using this Markdown file:

1. Paste this output into your source file.
2. See the notes and action items below regarding this conversion run.
3. Check the rendered output (headings, lists, code blocks, tables) for proper
   formatting and use a linkchecker before you publish this page.

Conversion notes:

* Docs to Markdown version 1.0β33
* Thu Apr 21 2022 11:00:38 GMT-0700 (PDT)
* Source doc: Introducció a Unity: Sessió 3
----->


**<span style="text-decoration:underline;">Guió - Resumen Sessió 3</span>**

<span style="text-decoration:underline;">PART 1</span>



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
