Einführung und Ziele {#section-introduction-and-goals}
====================

Aufgabenstellung {#_aufgabenstellung}
----------------
**Inhalt.**

Beim Projekt "Traffic Sumulation" handelt es sich um ein FH-Projekt im Zuge der Lehrveranstaltng  Softwarearchitektur.
Die wesentliche Aufgabenstellung ist Entwurf, Dokumentation und Implementierung einer verteilten komponentenbasierten Verkehrsimulation gemäß des [Anforderungsdokuments](Anforderungsdokument_Verkehrsimulation.pdf).

**Die Simulation sollte wie folgt unterstützen:**
- Mikroskopische Simulation der Fahrzeuge (PKW und LKW) und Ampelanlagen
- Zusammenhängendes Verkehrs-/Straßennetz
- Geregelte und ungeregelte Kreuzungen
- Verhalten der Verkehrsteilnehmer sollte weitgehend parametrisierbar sein (variabel/zufällig/individuell)
- Verhalten der Lichtsteueranlage parametrisierbar Regelung über eigenen Prozess
- Anzahl der über Einfahrtsstraßen in das System zufahrenden Fahrzeuge regelbar
- Grafische Darstellung und einfache Benutzerschnittstelle Konfiguration-GUI nicht unbedingt notwendig

**Motivation.**

Die Motivation aus Sicht der Teammitgliedern besteht nicht nur in Fertigstellung des Projets, sondern auch in:
- praktische Umsetzung eines Projektes mit Hilfe von agilen Projktmanagementtechniken
- Erweiterung der Know-How in der .NET Umgebung
- weitere Erfahrungen in der komponentenbasierten Entwicklung sammeln

**Form.**

Die Endlösung könnte in etwa, wie [hier](http://volkhin.com/RoadTrafficSimulator/) aussehen.

Qualitätsziele {#_qualit_tsziele}
--------------

- agile Softwareentwicklungsprozess einhalten mit folgenden Zielen:

    - Abbau der Bürokratie und durch die stärkere Berücksichtigung der menschlichen Aspekte effizienter zu gestalten
    - reine Entwurfsphase auf ein Mindestmaß zu reduzieren
    - im Entwicklungsprozess so früh wie möglich zu aus-führbarer Software zu gelangen
    - in regelmäßigen, kurzen Abständen dem Kunden zur gemeinsamen Abstimmung die Ergebnise für weitere Abstimmung vorzeigen

- laufende Dokumentationsanpassung:

    Die Doku soll nicht erst am Ende es Projekts sondern laufend erfolgen. Das Ziel ist dabei permanent gültige Dokumente zu besitzen.

- komponentenbasierte Entwicklung (mehrere loigsche Komponente und kein Monolit)
- Erweiterbarkeit der Komponenten vorsehen

    In der Hinsicht auf mögliche Veränderungen oder ERweiterungen müssen die Kompnenten zu einem gewissen Teil eine einfache Anpssung ohne großen Aufwand "vertragen"


Stakeholder {#_stakeholder}
-----------

Rolle | Kontakt | Erwartungshaltung 
------- | ---------------- | ----------:
Roland Graf  | roland.graf@fh-salzburg.ac.at | Fertigstellung des Projekts
Eduard Hirsch  | eduard.hirsch@fh-salzburg.ac.at | Fertigstellung des Projekts
      
Randbedingungen {#section-architecture-constraints}
===============

Kontextabgrenzung {#section-system-scope-and-context}
=================

Fachlicher Kontext {#_fachlicher_kontext}
------------------

**&lt;Diagramm und/oder Tabelle&gt;**

**&lt;optional: Erläuterung der externen fachlichen Schnittstellen&gt;**

Technischer Kontext {#_technischer_kontext}
-------------------

**&lt;Diagramm oder Tabelle&gt;**

**&lt;optional: Erläuterung der externen technischen
Schnittstellen&gt;**

**&lt;Mapping fachliche auf technische Schnittstellen&gt;**

Lösungsstrategie {#section-solution-strategy}
================

Bausteinsicht {#section-building-block-view}
=============

Whitebox Gesamtsystem {#_whitebox_gesamtsystem}
---------------------

***&lt;Übersichtsdiagramm&gt;***

Begründung

:   *&lt;Erläuternder Text&gt;*

Enthaltene Bausteine

:   *&lt;Beschreibung der enhaltenen Bausteine (Blackboxen)&gt;*

Wichtige Schnittstellen

:   *&lt;Beschreibung wichtiger Schnittstellen&gt;*

### &lt;Name Blackbox 1&gt; {#__name_blackbox_1}

*&lt;Zweck/Verantwortung&gt;*

*&lt;Schnittstelle(n)&gt;*

*&lt;(Optional) Qualitäts-/Leistungsmerkmale&gt;*

*&lt;(Optional) Ablageort/Datei(en)&gt;*

*&lt;(Optional) Erfüllte Anforderungen&gt;*

*&lt;(optional) Offene Punkte/Probleme/Risiken&gt;*

### &lt;Name Blackbox 2&gt; {#__name_blackbox_2}

*&lt;Blackbox-Template&gt;*

### &lt;Name Blackbox n&gt; {#__name_blackbox_n}

*&lt;Blackbox-Template&gt;*

### &lt;Name Schnittstelle 1&gt; {#__name_schnittstelle_1}

…

### &lt;Name Schnittstelle m&gt; {#__name_schnittstelle_m}

Ebene 2 {#_ebene_2}
-------

### Whitebox *&lt;Baustein 1&gt;* {#_whitebox_emphasis_baustein_1_emphasis}

*&lt;Whitebox-Template&gt;*

### Whitebox *&lt;Baustein 2&gt;* {#_whitebox_emphasis_baustein_2_emphasis}

*&lt;Whitebox-Template&gt;*

…

### Whitebox *&lt;Baustein m&gt;* {#_whitebox_emphasis_baustein_m_emphasis}

*&lt;Whitebox-Template&gt;*

Ebene 3 {#_ebene_3}
-------

### Whitebox &lt;\_Baustein x.1\_&gt; {#_whitebox_baustein_x_1}

*&lt;Whitebox-Template&gt;*

### Whitebox &lt;\_Baustein x.2\_&gt; {#_whitebox_baustein_x_2}

*&lt;Whitebox-Template&gt;*

### Whitebox &lt;\_Baustein y.1\_&gt; {#_whitebox_baustein_y_1}

*&lt;Whitebox-Template&gt;*

Laufzeitsicht {#section-runtime-view}
=============

*&lt;Bezeichnung Laufzeitszenario 1&gt;* {#__emphasis_bezeichnung_laufzeitszenario_1_emphasis}
----------------------------------------

-   &lt;hier Laufzeitdiagramm oder Ablaufbeschreibung einfügen&gt;

-   &lt;hier Besonderheiten bei dem Zusammenspiel der Bausteine in
    diesem Szenario erläutern&gt;

*&lt;Bezeichnung Laufzeitszenario 2&gt;* {#__emphasis_bezeichnung_laufzeitszenario_2_emphasis}
----------------------------------------

…

*&lt;Bezeichnung Laufzeitszenario n&gt;* {#__emphasis_bezeichnung_laufzeitszenario_n_emphasis}
----------------------------------------

…

Verteilungssicht {#section-deployment-view}
================

Infrastruktur Ebene 1 {#_infrastruktur_ebene_1}
---------------------

***&lt;Übersichtsdiagramm&gt;***

Begründung

:   *&lt;Erläuternder Text&gt;*

Qualitäts- und/oder Leistungsmerkmale

:   *&lt;Erläuternder Text&gt;*

Zuordnung von Bausteinen zu Infrastruktur

:   *&lt;Beschreibung der Zuordnung&gt;*

Infrastruktur Ebene 2 {#_infrastruktur_ebene_2}
---------------------

### *&lt;Infrastrukturelement 1&gt;* {#__emphasis_infrastrukturelement_1_emphasis}

*&lt;Diagramm + Erläuterungen&gt;*

### *&lt;Infrastrukturelement 2&gt;* {#__emphasis_infrastrukturelement_2_emphasis}

*&lt;Diagramm + Erläuterungen&gt;*

…

### *&lt;Infrastrukturelement n&gt;* {#__emphasis_infrastrukturelement_n_emphasis}

*&lt;Diagramm + Erläuterungen&gt;*

Querschnittliche Konzepte {#section-concepts}
=========================

*&lt;Konzept 1&gt;* {#__emphasis_konzept_1_emphasis}
-------------------

*&lt;Erklärung&gt;*

*&lt;Konzept 2&gt;* {#__emphasis_konzept_2_emphasis}
-------------------

*&lt;Erklärung&gt;*

…

*&lt;Konzept n&gt;* {#__emphasis_konzept_n_emphasis}
-------------------

*&lt;Erklärung&gt;*

Entwurfsentscheidungen {#section-design-decisions}
======================

Qualitätsanforderungen {#section-quality-scenarios}
======================

Qualitätsbaum {#_qualit_tsbaum}
-------------

Qualitätsszenarien {#_qualit_tsszenarien}
------------------

Risiken und technische Schulden {#section-technical-risks}
===============================

Glossar {#section-glossary}
=======

+-----------------------+-----------------------------------------------+
| Begriff               | Definition                                    |
+=======================+===============================================+
| *&lt;Begriff-1&gt;*   | *&lt;Definition-1&gt;*                        |
+-----------------------+-----------------------------------------------+
| *&lt;Begriff-2*       | *&lt;Definition-2&gt;*                        |
+-----------------------+-----------------------------------------------+


