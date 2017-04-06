# Architektur
## Architektur Diagramm 
![](img/Architektur.png)

Die Architektur der TrafficSimulation ist in 4 Komponenten aufgeteilt.

### Simulation

### UI

### Traffic Light Control

### Logging

Die Komponenten werden über WCF miteinander kommunizieren.
Logging ist in eine eigenständige Komponente ausgelagert, da mindestens zwei Prozesse Log Meldungen produzieren werden und das nicht über verschiedenste Logfiles verteilt werden soll.