# Dokumentation

## Änderungshistorie

| Autor | Änderung | Datum | Version |
| --- | --- | --- | --- |
| Reimar | Team Projekt erstellt Architekturdiagramm eingefügt| 06.04.2017 | 1.0 |
| Reimar | WCF Demo hinzugefügt, Nice to know erstellt | 08.04.2017 | 1.1 |
| Reimar | WCF Demo erweitert, Projekt aufgeräumt | 18.04.2017 | 1.2 |
## Inhaltsverzeichnis
### [Tools und Settings](/doc/tools.md)
### [Nice to know](/doc/nicetoknow.md)
### [Architektur](/doc/architecture.md)
### [Design](/doc/design.md)


## History erweitert
### V1.2
Ich habe das WCF Demo Project aus dem src Ordner verschoben, hat ja eigentlich nichts mit der finalen Lösung zu tun.
Das WCF Demo ist nun dahingeghend erweitert, dass ein Objekt mit einem Unterobjekt gmeinsam serialisiert wird und über den Kanal geschickt.
Wie es aussieht ist das nicht über Interfaces möglich. Daher werden wir in den Contracts auch konkrete Klassen haben. 
Das dürfen nur Objekte sein, die ausschließlich für den Transport verwendet werden. Das bedeutet nur Properties, keine Methoden!! 

Das ganze logging hab ich nun auch entfernt weil wir dafür [Sentinel](https://sentinel.codeplex.com/) verwenden.
Die NLog.config ist nun aus dem UI.Application Projekt verschoben und als Solution Item eingebunden. 
Das hat den Vorteil, dass wir nicht für jedes Projekt in dem wir logging brauchen, eine eigene Konfiguration erstellen müssen.

Codedokoumentation
Ich werde einstellen dass wir eine Warnung bekommen, wenn öffentliche Klassen nicht dokumentiert sind. 
Dadurch können wir zum Schluss automatisiert mit [Sandcastle](https://github.com/EWSoftware/SHFB) den gesamten Code im MSDN Style dokumentieren lassen.