# Projektübersicht:
Dieses Anwendung implementiert eine Lösung, die dazu dient, einen Service zu überwachen.
Sie prüft in einem vorgegebenen Zeitintervall, ob alle Aufträge vom Service verarbeitet wurden.
Sollten Aufträge nicht verarbeitet worden sein, werden diese nach einem vorgegebnen Zeitintervall erneut zum Service gesendet.
Die Anwendung ist auch in der Lage herauszufinden, ob der Service läuft oder gestoppt ist.
Sollte der Service nicht aktiv sein, so wird dieser gestartet.


### Voraussetzungen
- .NET 9.0 Runtime muss auf dem Zielserver installiert sein
- Zugriff auf den Datenbankserver 
- IIS muss installiert und konfiguriert sein, um die OrderHandler-Website zu hosten

### Bereitstellungsschritte
1. **Build des Projekts**
   - Das Projekt soll mit dem Production-Profil veröffentlicht werden

2. **Konfigurationsanpassungen**
   - Sicherstellen, dass die `appsettings.Production.json` die korrekten Produktionseinstellungen enthält
   - In der Produktionsumgebung wird der Servicename auf "Production: Service" gesetzt

3. **Windows-Service-Installation**
   - Da es sich um einen Windows-Service handelt, wird die Anwendung als Windows-Dienst installiert
   -  Das sc-Kommando kann hierfür verwendet werden:
      
     `sc create OrderImporter binPath="C:\path\to\gh.services.orderhandler.importer.worker.exe"
      sc description OrderImporter "Überwacht und steuert den OrderHandler-Service"
      sc config OrderImporter start=auto`

4. **IIS-Konfiguration**
   - Die Anwendung interagiert mit dem Service auf einem IIS-Server
   - Sicher stellen, dass der IIS-Benutzer die notwendigen Rechte hat

5. **Netzwerkzugriff**
   - Der Service benötigt Zugriff auf:
      - Die Datenbank (ip)
      - Den OrderHandler-Service (IP)
      - Den SMTP-Server 

6. **Protokollierung**
   - Zugriff auf den Protokollordner einrichten
   - Sicher stellen, dass der Seq-Server erreichbar ist


# Projektarchitektur & Struktur

## Projektübersicht

Das **Company.Product** Projekt implementiert eine Clean Architecture basierte Lösung für die Verarbeitung
von nicht verarbeiteten Kundenaufträgen.

## Projektstruktur
### Root Level
- **Company.Product.sln** - Solution File mit allen Projektreferenzen
- **.gitignore** - Git Ignore Regeln für .NET Projekte
- **src/** - Source Code Verzeichnis
- **test/** - Test Projekte
- **documentation/** - Projektdokumentation


### Source Code (src/)
#### Company.Product.Presentation
- **Company.Product.Presentation.csproj** - Presentation Projekt Konfiguration
- **Program.cs** - Anwendungs-Einstiegspunkt und DI Container Setup
- **Worker.cs** - Background Service Implementation (ExecuteAsync Endlosschleife)
- **appsettings.Development.json** - Entwicklungsumgebung Konfiguration
- **appsettings.Staging.json** - Staging-Umgebung Konfiguration
- **appsettings.Production.json** - Produktionsumgebung Konfiguration


#### Company.Product.Application
- **Company.Product.Application.csproj** - Application Projekt Konfiguration
- **DependencyInjection.cs** - Service Registrierung für Application Layer
- **DTOs/** - Data Transfer Objects für Layer-Kommunikation
- **Pipeline/** - Pipeline Pattern Implementation (IOperation<T>)
- **Services/** - Application Services (ProcessWorkflowHandler, OperationProcessing)
- **UseCases/** - Individuelle Geschäfts-Use Cases
- **Contracts/** - Application Interfaces und Commands



####  Company.Product.Domain
- **Company.Product.Domain.csproj** - Domain Projekt Konfiguration
- **Entities/** - Geschäftsentitäten (RequestOrder, Customer, DeliveryAddress, InvoiceAddress)
- **Contracts/** - Domain Interfaces und Abstraktionen
- **ValueObjects/** - Unveränderliche Value Objects
- **DomainServices/** - Domain-spezifische Services und Geschäftslogik


#### Company.Product.Infrastructure
- **Company.Product.Infrastructure.csproj** - Infrastructure Projekt Konfiguration
- **DependencyInjection.cs** - Service Registrierung für Infrastructure Layer
- **Services/** - Infrastructure Services (Externe API Integration)
- **Repository/** - Datenzugriff Implementation (Repository Pattern)



##  Test Projekte (test/)

###  Company.Product.Domain.Tests - Domain Layer Tests
- **Domain services Unit Tests** - Tests für Geschäftslogik

###  Company.Product.Application.Tests - Application Layer Tests
- **Integration Tests** - End-to-End Use Case Tests
- **für Use Cases Tests** - Tests für einezelne Use Cases

###  Company.Product.Infrastructure.Tests - Infrastructure Layer Tests
- **Repository Implementation Tests** - Tests für Repository-Implementation
- **Database Integration Tests** - Datenbank-Integrationstests
- **External API Integration Tests** - Tests für externe API-Integration
- **Data Access Layer Tests** - Tests für Datenzugriffsschicht


##  Dokumentation (documentation/)

###  Markdown Dokumentation
- **readme.md** - Hauptprojekt-Dokumentation und Getting Started
- **UseCase.md** - Beschreibung- und Analyse der Use Cases

###  PlantUML Diagramme & Bilder
- **UseCase.puml** - Use Case Diagramm Definition
- **UseCases.png** - Use Case Diagramm als Bild
- **Sequence.puml** - Sequenz-Diagramm für Workflow-Ablauf
- **Sequence.png** - Sequenz-Diagramm als Bild
- **Component.puml** - Komponentenarchitektur-Diagramm
- **Component.png** - Komponentenarchitektur als Bild



#  Design 
## **Presentation-Layer**

**Verantwortlichkeiten:**
- Startet als Background Service
- Initiiert Workflow-Verarbeitung
- Konfiguriert Dependency Injection

Die `Company.Product.Presentation`-Schicht ist als Background Service implementiert.  
Beim Start des Programms bündelt diese Schicht alle Komponenten, die über Dependency Injection (DI) registriert sind, zusammen.  
Anschließend wird die Methode `ExecuteAsync` aus der Klasse `Worker` aufgerufen, welche eine Endlosschleife initiiert.  
Innerhalb dieser Schleife wird wiederholt ein Befehl an die `Company.Product.Application`-Schicht gesendet,  
welcher die Prozesskette startet. Um die Entkopplung zwischen den beiden Schichten zu fördern, wurde das **Mediator-Pattern** eingesetzt.  
Das Mediator-Pattern hilft, die direkte Kommunikation zwischen der Präsentationsschicht und der Anwendungslogik zu vermeiden,  
und sorgt so für eine saubere Trennung der Verantwortlichkeiten.

---

## **Application-Layer**

**Verantwortlichkeiten:**
- Orchestriert dieAusführung der Anwedungsfälle
- Verwaltet Transaktionen und Error Handling
- Transformiert Daten zwischen Layern

Die `Company.Product.Application`-Schicht definiert und orchestriert die Ausführung der Anwendungsfälle („Use Cases“).  
Die Klasse `ProcessWorkflowHandler` empfängt den Befehl aus der Präsentationsschicht und startet die Prozesskette, indem sie die Methode `OperationProcessing.ExecuteAsync()` aufruft.  
Die Prozessverarbeitung erfolgt in mehreren Schritten, die jeweils durch eigene Anwendungsfälle repräsentiert werden.  
Jeder Anwendungsfall ist für eine spezifische Aufgabe innerhalb der Prozessverarbeitungskette verantwortlich und kapselt die jeweilige Geschäftslogik.  
Während der Ausführung der Prozesskette können abhängig vom Ergebnis eines Schrittes weitere Anwendungsfälle aufgerufen werden oder nicht.

Um die Flexibilität und Erweiterbarkeit der Prozesskette zu gewährleisten, wurde das **Pipeline-Pattern** gewählt.  
Das **Pipeline-Pattern** ermöglicht es, dass die Prozesskette dynamisch angepasst werden kann, indem die Ausführung der Anwendungsfälle kontrolliert werden kann.  
Die Methode `OperationProcessing.ExecuteAsync()` implementiert den Aufruf und die Ausführung der Pipeline.
Um die Entkopplung der Prozessverarbeitung von den konkreten Instanzen zu fördern, wird das **Factory-Pattern** für die Erzeugung der Pipeline verwendet.

In der Klasse `OrderProcessingPipelineFactory` wird die Pipeline instanziiert und anschließend die einzelnen auszuführenden Schritte in der gewünschten Reihenfolge hinzugefügt.  
Die Klasse wird dann per **Dependency Injection (DI)** in die `OperationProcessing`-Klasse injiziert, wodurch die Flexibilität und Testbarkeit der Anwendung erhöht wird.

**Pipeline-Pattern**

Die Klasse `Pipeline` ist eine generische Klasse, die das Interface `IOperation` implementiert.  
Sie enthält eine generische Liste zum Speichern der auszuführenden Schritte sowie eine Methode `Register`,  
mit der ein Element zur Liste hinzugefügt werden kann. Zudem implementiert sie die Methode `Invoke` von `IOperation`,  
die die Schritte in der Liste nacheinander ausführt.  
Als Argument für die Methode `Invoke` wird ein Kontextobjekt `KontextData` übergeben,  
das die notwendigen Daten für die Ausführung der Anwendungsfälle enthält.

Um die Anwendungsfälle in der Pipeline zu registrieren, werden Klassen implementiert, die das Interface `IOperation` umsetzen.  
Eine `Operation`-Klasse repräsentiert einen einzelnen Anwendungsfall innerhalb der Prozessverarbeitungskette.  
Sie überschreibt die Methode `Invoke`, die für die Ausführung des jeweiligen Anwendungsfalls zuständig ist.

Innerhalb der `Invoke`-Methode einer `Operation`-Klasse wird das übergebene Kontextobjekt geprüft, um zu entscheiden,  
ob der aktuelle Anwendungsfall ausgeführt werden soll und ob die Ausführung des nächsten Anwendungsfalls erfolgen soll.  
Falls der vorherige Schritt erfolgreich abgeschlossen wurde, wird der aktuelle Schritt in der Pipeline ausgeführt.  
An dieser Stelle findet auch das **Exception-Handling** für den jeweiligen Schritt statt,  
um Fehler zu behandeln und den Prozess je nach Schwere des Fehlers fortzusetzen oder anzuhalten.  
Diese Fehlerbehandlung stellt sicher, dass die Pipeline auch bei unvorhergesehenen Fehlern robust bleibt und die Kontrolle über den Prozess erhalten bleibt.

---

## **Domain-Layer**
**Verantwortlichkeiten:**
- Definiert Geschäftsentitäten
- Implementiert Geschäftsregeln
- Stellt Domain Services bereit
- Keine externen Abhängigkeiten

Die Entscheidung, ob ein Anwendungsfall ausgeführt wird, basiert auf den im **Domain-Layer** implementierten Geschäftsregeln.  
Die Geschäftslogik im Domain-Layer definiert, welche Bedingungen erfüllt sein müssen, damit ein Anwendungsfall weiterverarbeitet werden kann.
Diese Schicht hat keine Abhängigkeiten zu den anderen Schichten, stellt aber Schnittstellen `Contracts` bereit, die vom **Application-Layer** implementiert werden

---
## **Infrastructure-Layer**
**Verantwortlichkeiten:**
- Datenbankzugriff über Repository Pattern
- Integration mit externen APIs
- Implementiert Infrastructure Contracts
- SQL Server Datenpersistierung

implementiert die technischen Aspekte der Anwendung und stellt die Verbindung zwischen der Geschäftslogik und den externen Systemen her.
Diese Schicht ist verantwortlich für die konkrete Umsetzung aller technischen Operationen, die von den höheren Schichten benötigt werden,
ohne dass diese die technischen Details kennen müssen.

---

##  **Design Prinzipien**

### **Clean Architecture**
- **Domain**: Keine Abhängigkeiten, nur Business Logic
- **Application**: Abhängig von Domain, Use Cases Orchestration
- **Infrastructure**: Abhängig von Application, Data Access & External APIs
- **Presentation**: Abhängig von Application, Background Service Host

**SOLID-Prinzipien**

- **Single Responsibility Principle (SRP)**:
    - Use Cases und Services haben klare Aufgaben
    - Jede Schicht hat eigene Verantwortlichkeiten
- **Open/Closed Principle (OCP)**
    - `Pipeline` Klasse kann neue Operationen registrieren ohne Modifikation
    - `IOperation<T>` Interface ermöglicht das Hinzufügen neuer Pipeline-Schritte ohne Änderung bestehender Codes
    - Zusätzliche Repository-Implementierungen über Domain Contracts möglich

- **Liskov Substitution Principle (LSP)**
    - Ersetzbarkeit von konkreten Klassen durch ihre Abstraktionen Alle Pipeline-Operationen sind austauschbar über `IOperation<T>`
    - Repository-Implementierungen folgen den Domain Contracts ohne Verhalten zu brechen
    - Mock-Implementierungen in Tests (`RetrieveProcessedOrdersMock`, `SecLoggerMock`) ersetzen echte Services

- **Interface Segregation Principle (ISP)**
    - Keine "Fat Interfaces"
    - Spezifische, fokussierte Interfaces:
        - `IOrderValidationService` - Nur Validierungsmethoden (`HasNewOrders`, `HasPendingOrders`)
        - `IOrderAddAttributService` - Nur eine Methode für Attribut-Hinzufügung
        - `IOperation<T>` - Minimales Interface für Pipeline-Operationen
        - `IReviewOrders` - Spezifisches Interface nur für Order Review Logic

- **Dependency Inversion Principle (DIP)**
    - Domain Layer als Zentrum:
        - Domain Layer definiert Contract für Application Layer
        - Infrastructure Layer abhängig nur von Application Abstractions
        - Dependency Injection Implementation

### **Design Patterns**
- **Pipeline Pattern**: Für Use Case Orchestrierung und modulare Verarbeitung
- **Mediator Pattern**: Für Schicht-Entkopplung und lose Kopplung
- **Repository Pattern**: Für abstrakten Datenzugriff
- **Factory-Pattern** Für eine zentralisierte Instaziirung.
- **Background Service Pattern**: Für kontinuierliche Verarbeitung

##  **Testing Strategy**
### **Unit Tests**
- **Domain Tests**: Geschäftslogik und Entitäten
- **Application Tests**: Use Cases und Pipeline Logic

### **Integration Tests**
- **ProcessWorkflowHandlerTests**: End-to-End Workflow Testing
- **Database Integration**: Repository und SQL Operations
- **API Integration**: External Service Communication
