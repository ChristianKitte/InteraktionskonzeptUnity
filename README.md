![image](https://user-images.githubusercontent.com/32162305/150810942-99672aac-99af-47ea-849b-ba263fae0c3f.png)

---

**Augmented and Virtual Reality (AVR)**

**Dozent: Prof. Dr. Thies Pfeiffer**

**University of Applied Sciences Emden/Leer, Faculty of Technology, Department of Electrical Engineering and Informatics**

**Studiengang Medieninformatik Online MA, Wintersemester 2021/22**

---

# Studienarbeit Virtual Reality: Remote Steuerung

Im Wintersemester 2021/22 wurde die Entwicklung eines Interaktionskonzeptes zum Thema Remote Steuerung sowie dessen prototypische Umsetzung als Studienarbeit erstellt.

Ziel war hierbei, innerhalb einer Virtuellen Umgebung (VR) einzelne Objekte einer Gruppe über einen Zeitraum hinweg permanent zu selektieren und neu zu positionieren. Selektion, Bewegung und das Ablegen sollten hierbei auch aus der Ferne (Remote) präzise und ohne direkte Sicht auf das ausgewählte Objekt möglich sein.

Die Umsetzung erfolgte auf Basis von Unity unter Verwendung eines Oculus Headsets und ist hier verfügbar. Als Referenz wurde zudem eine naive Problemlösung mit Standardkomponenten und Fähigkeiten umgesetzt. Eine Zusammenfassung der Arbeit ist als [wissenschaftliches Poster](https://github.com/ChristianKitte/InteraktionskonzeptUnity/blob/main/Doc/Semesterarbeit.pdf) verfügbar.

Zusätzlich befinden sich in den Ordner [Bin](https://github.com/ChristianKitte/InteraktionskonzeptUnity/tree/main/Bin) die kompilierte Lösung beider Versionen als Android Anwendung (.APK) für die Oculus. In dem Ordner [Streaming](https://github.com/ChristianKitte/InteraktionskonzeptUnity/tree/main/Streaming) befinden sich zwei herunterskalierte Videos, welche den Einsatz beider Lösungen anhand einer beispielhaften Aufgabenstellung in Aktion zeigen. Videos in voller Auflösung finden sich auf YouTube.

- YouTube Video [Steuerung aus der Ferne - einfache Umsetzung](https://youtu.be/TofhwbJO1fI)
- YouTube Video [Steuerung aus der Ferne - angepasste Umsetzung](https://youtu.be/Wzkt-cg_A-U)

Während für die Steuerung der Bewegung in beiden, für das Greifen nur in der einfachen Lösung auf allgemeine Interaktionskonzepte zurückgegriffen wurde, ist das Mapping für die Objektsteuerung des linken Controllers in der angepassten Umsetzung komplett neu erstellt worden:

| Beabsichtigte Aktion | Interface | Interaktion | Ergebnis |
| --- | --- | --- | --- |
| Auswahl eines Objektes | Trigger | Pressen | Aktivierung des Rays |
| Selektion Beginn | Trigger | Klick | m.Ray: Selektieren |
| Selektion Ende | Trigger | Klick | o.Ray: Release |
| Bewegung auf der Z-Achse* | Thumbstick | Vor/Zurück | Vor-/Rückwärts |
| Bewegung auf der X-Achse* | Thumbstick | Links/Rechts | Links/Rechts |
| Bewegung auf der Y-Achse* | Thumbstick + Grip | Vor/Zurück | Hoch/Runter |
| Drehung um Y-Achse* | Thumbstick + Grip | Links/Rechts | Links-/Rechtsdrehung |

*) gemeint ist hier die globale Achse

