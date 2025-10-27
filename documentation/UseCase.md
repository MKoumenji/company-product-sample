# Analyse des UseCase.puml

## **Ziele des Systems**
1. **Sicherstellung der vollständigen Bestellverarbeitung**:
    - Der Hauptprozess UC1 startet die gesamte Logik zur Bestellungsabholung.
2. **Fehlerhandling und Warnungsberichterstattung**:
    - Durch UC7 werden Probleme identifiziert und gemeldet.
3. **Protokollierung aller Aktivitäten**:
    - UC6 garantiert eine komplette Dokumentation aller Vorgänge, um Transparenz und eine Grundlage für Fehleranalysen zu schaffen.

Das **Use Case Diagramm "UseCase.puml"**
skizziert die verschiedenen Funktionalitäten der Anwendung.

---

## **Hauptakteur**
- **Handler** (als Application Actor):
    - Stellt eine Softwareanwendung dar, die als primärer Akteur die Bestellverarbeitungslogik startet und koordiniert.

---

## **Hauptprozesse**
1. **UC1: Retrieve Orders**
    - Holt Bestellungen aus einer externen Quelle oder Datenbank in das System.
    - Dieser Prozess ist der zentrale Startpunkt im System.

2. **UC2: Review Orders**
    - Überprüft und validiert die abgeholten Bestellungen auf Vollständigkeit und Korrektheit.
    - Wird obligatorisch nach UC1 ausgeführt (includes-Beziehung).

3. **UC3: Dispatch Pending Orders**
    - Verarbeitet und versendet ausstehende Bestellungen.
    - Wird nur bei Bedarf ausgeführt, wenn ausstehende Bestellungen verfügbar sind (extends-Beziehung von UC2).

4. **UC4: Validate Dispatched Orders**
    - Validiert erfolgreich versendete Bestellungen.
    - Wird obligatorisch nach UC3 ausgeführt (includes-Beziehung).

5. **UC5: Restart the OrderHandler**
    - Ermöglicht das Neustarten des OrderHandlers bei Problemen oder Systemstörungen.
    - Wird ausgeführt, wenn versendete Bestellungen im Order Handler fehlen (extends-Beziehung von UC4).

---

## **Zentrale Dienste**
6. **UC6: Log Behavior**
    - Protokolliert das Verhalten des Systems und aller Prozesse:
        - Verarbeitungsschritte (Abruf, Überprüfung, Versendung, Validierung).
        - Systemereignisse wie Neustarts.
    - Wird von allen Hauptprozessen obligatorisch eingebunden (includes-Beziehung).

7. **UC7: Report Warning**
    - Meldet Warnungen und kritische Situationen, insbesondere beim Neustart des OrderHandlers.
    - Wird vom Restart-Prozess (UC5) obligatorisch ausgeführt (includes-Beziehung).

---

## **Verknüpfungen zwischen den Use Cases**

### **Obligatorische Abhängigkeiten (includes)**:
- **UC1 → UC2**: Nach dem Abrufen der Bestellungen müssen diese überprüft werden.
- **UC1 → UC6**: Protokollierung der Bestellungsabholung.
- **UC2 → UC6**: Protokollierung der Bestellungsüberprüfung.
- **UC3 → UC4**: Nach dem Versenden müssen die Bestellungen validiert werden.
- **UC3 → UC6**: Protokollierung des Versendeprozesses.
- **UC4 → UC6**: Protokollierung der Validierung.
- **UC5 → UC6**: Protokollierung von Neustarts.
- **UC5 → UC7**: Warnungsmeldung bei Neustarts.

### **Optionale Erweiterungen (extends)**:
- **UC2 → UC3**: Versendung wird nur ausgelöst, wenn ausstehende Bestellungen vorhanden sind.
- **UC4 → UC5**: Neustart wird nur bei fehlenden Bestellungen im Handler ausgelöst.

---

## **Beschreibung der Struktur**
Das Diagramm ist horizontal organisiert und zeigt:
- Den **Handler als Softwareakteur**, der die Hauptprozesse initiiert.
- Eine **sequenzielle Verarbeitungskette** (UC1 → UC2 → UC3 → UC4 → UC5).
- **Bedingte Ausführung** durch extends-Beziehungen für optionale Schritte.
- **Zentrale Protokollierung** (UC6) als Querschnittsfunktion für alle Prozesse.
- **Spezifische Warnungsberichterstattung** (UC7) für kritische Situationen.

---
