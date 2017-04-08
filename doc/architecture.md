# Architektur
## Beschreibung
die Aufgabenstellung:
Implementierung einer verteilten komponentenbasierten Verkehrssimulation.
Die Sumilation unterstützt:
- Mikroskopische Simulation der Fahrzeuge (PKW und LKW) und Ampelanlagen
- Zusammenhängendes Verkehrs-/Straßennetz
- Geregelte und ungeregelte Kreuzungen
- Verhalten der Verkehrsteilnehmer wird weitgehend parametrisierbar sein (variabel/zufällig/individuell)
- Verhalten der Lichtsteueranlage parametrisierbar Regelung über eigenen Prozess
- Anzahl der über Einfahrtsstraßen in das System zufahrenden Fahrzeuge regelbar
## Architektur Diagramm 
![](img/Architektur.png)

Die Traffic Simulation wird in 4 verschiedene Komponenten aufgeteilt. Diese Komponenten kommunizieren mit der Windows Communication Foundation (WCF) miteinander. WCF wurde ausgewählt da es damit sehr einfach möglich ist Interprozesskommunikation (IPC) zu implementieren, und diese Kommunikation in zukunft sogar auf Webservices umzustellen um zwischen verschiedenen Systemen 

### Simulation
Die Komponenten für die Simulation.

### UI
Visualsierung der Simulation.

### Traffic Light Control
Steuerung für die Ampeln

### Logging
Logging ist in eine eigenständige Komponente ausgelagert, da mindestens zwei Prozesse Log Meldungen produzieren werden und das nicht über verschiedenste Logfiles verteilt werden soll.

## Allgemeines
Die Kommunikation zwischen den Komponenten wird mit WCF durchgeführt.
WCF ist ist eine dienstorientierte Kommunikationsplattform für verteilte Anwendungen in Microsoft Windows. Sie führt viele Netzwerkfunktionen zusammen und stellt sie den Programmierern solcher Anwendungen standardisiert zur Verfügung.

## Zusammenwirken der Komponenten
- Grafisch
- Textuell