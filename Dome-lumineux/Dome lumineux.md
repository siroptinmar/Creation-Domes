# **Dôme Lumière**

[Quelle expérience utilisateur ?](#quelle-expérience-utilisateur)

[Les Besoins](#les-besoins)

[La Faisabilité](#la-faisabilité)

[La Réplicabilité](#la-réplicabilité)

## ***Quelle expérience utilisateur***

Cette installation permet d’initier le public aux jeux de lumière et à l'interactivité numérique. Dans ce dôme, le public est donc défié à relever le plus haut niveau du jeu en retenant le plus de combinaisons de couleurs possibles (comme le célèbre jeu [Simon Says](https://freesimon.org/)).  
   
Nous retrouvons le même dispositif technique que celui présent dans le dôme musical, le corps du public est conducteur de l'électricité. Un courant très faible est envoyé à deux bornes et le corps sert à la fois d'interrupteur et de déclencheur d'événements, il déclenche la lumière qu’il a touché comme indiqué (soit : bleu, rouge, jaune ou vert).

## ***Les Besoins***

Pour délimiter les espaces d’interactivité des participants, nous avons utilisé des chutes de bois de formes rondes qui appartenaient à un de nos précédents projets artistiques. Sur chacun des ronds, est collé du ruban adhésif en aluminium (matière conductible)pour que le corps de l’utilisateur puisse interagir.  

### Matériels

- 1 Carte [Playtron](https://shop.playtronica.com/products/playtron)  - Playtronica
- 1 Node Dmx ([ENTTEC](https://www.enttec.com/product/dmx-usb-interfaces/dmx-usb-pro-professional-1u-usb-to-dmx512-converter/) par exemple)
- Quelques éclairages Dmx RGB
- 1 ou 2 [enceintes](https://www.prozic.com/www2/info_promo_CR4-X_Monitoring-Mackie.html) reliée à l'ordinateur (avec [ça](https://www.prozic.com/www2/info_promo_HBA-3SC2-0090_jack-35-stereo-vers-cinch-RCA.html) par exemple)
- 1 ordinateur

### Logiciels
- [CHATAIGNE](https://benjamin.kuperberg.fr/chataigne/fr)
- [Unity3D](https://unity.com/fr)
- les [Scripts](Assets/Script)

## ***La Faisabilité - Prérequis***  

![img](img/Capture%20d’écran%202024-12-03%20à%2014.42.00.png)

Le projet a été développé en C# sur [Unity3D](https://unity.com/fr), afin d'avoir l'affichage des scores et top score, sur un écran. 
Le logiciel [CHATAIGNE](https://benjamin.kuperberg.fr/chataigne/fr) nous sert ensuite à connecter ensemble, la [playtron](https://shop.playtronica.com/products/playtron) en midi, les lumières en DMX, et le moteur de jeu dans Unity3D.

Dépendances du projet dans Unity :
>Tweening engine for Unity3d - leanTween - The MIT License (MIT)
Copyright (c) 2017 Russell Savage - Dented Pixel

>MIDI - DryWetMIDI.Nativeless 7.0.0 by Melanchall

>OSC - ExtOSC Copyright (c) 2018-2020 Vladimir Sigalkin

## ***La Réplicabilité***

Le coeur de connection est Chataigne **SimonSays.noisette**, il serait possible de faire évoluer vers une version embarqué sur Raspberry (donc en python), pour remplacer le Game Manager gérée par Unity.  

![img](img/Capture%20d’écran%202024-12-03%20à%2014.56.49.png)
