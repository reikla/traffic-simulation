# Architektur
## Beschreibung
die Aufgabenstellung:
Implementierung einer verteilten komponentenbasierten Verkehrssimulation.
Die Sumilation unterstützt:
- sdf
- sf
## Architektur Diagramm 
![](img/Architektur.png)

Die Architektur der TrafficSimulation ist in 4 Komponenten aufgeteilt.

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