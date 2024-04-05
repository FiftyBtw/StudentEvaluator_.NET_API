# Web API StudentEvaluator


# CI / CD
<div align="center">

![Build Status](https://codefirst.iut.uca.fr/api/badges/benjamin.paczkowski/sae_2a_entity_framework/status.svg)

</div>

<div align="center">

![Quality Gate Status](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=alert_status&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Bugs](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=bugs&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Code Smells](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=code_smells&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Coverage](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=coverage&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Duplicated Lines (%)](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=duplicated_lines_density&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Lines of Code](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=ncloc&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Maintainability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=sqale_rating&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Reliability Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=reliability_rating&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Security Rating](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=security_rating&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Technical Debt](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=sqale_index&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)
![Vulnerabilities](https://codefirst.iut.uca.fr/sonar/api/project_badges/measure?project=Student_Evaluator-API&metric=vulnerabilities&token=8e5c53e153f855619cf79fd2b63f4f81faac9fac)

</div>

# Introduction

StudentEvaluator_API_EF est une API RESTful conçue pour simplifier la communication entre le site web et l'application Android du projet StudentEvaluator. Son objectif principal est de faciliter la gestion des évaluations, des modèles, des utilisateurs et d'autres données essentielles de manière efficace...

# Jalon 1

## Diagramme de paquetage 

```mermaid
  flowchart TB
    EF_ConsoleTests -.-> EF_StubbedContextLib -.-> EF_DbContextLib -.-> EF_Entities;

    Model2Entities --> EF_DbContextLib;
    Model2Entities -.-> Client_Model;
    Model2Entities -.-> Shared;

    Entities2Dto --> EF_DbContextLib;
    Entities2Dto -.-> API_Dto;
    Entities2Dto -.-> Shared;

    Dto2Model -.-> Shared;
    Dto2Model -.-> API_Dto;
    Dto2Model -.-> Client_Model;
    
    Shared -.-> EF_Entities
    Shared -.-> API_Dto
    Shared -.-> Client_Model

    API_EF -.-> EventLogs;
    API_EF -.-> Entities2Dto;
    
    UnitTests -.-> Entities2Dto;
    UnitTests -.-> EF_DbContextLib;
    UnitTests -.-> Model2Entities;
    UnitTests -.-> Dto2Model;
```

## Ce que l'on a fait pour le premier jalon
### Fonctionnalités :

- Liaison Modèle <-> DTO
- Liaison Entités <-> DTO
- Liaison Modèle <-> Entités
- Journalisation (API et EF)
- Injection de service
- Injection / indépendance du fournisseur
- Pagination & Filtrage (Exemple : GetEvaluationByTeacherId)
- Requêtes CRUD sur des données simples et complexes (API et EF)
- Utilisation des relations One-to-One et One-to-Many
- Mappage (Entités <-> DTO)
- Authentification
- Intégration continue (CI)
- Liaison API <-> EF
- Code partiellement documenté

### Tests:

- Tests unitaires - EF (SQLite en mémoire)
- Tests fonctionnels - EF (Sans données simulées)
- Tests unitaires - API sur les contrôleurs (Moq)
- Tests fonctionnels - API (consommation des requêtes)
- Tests unitaires sur les différents "Data Manager" faisant le lien entre DTO, Entités et Modèle
- Tests unitaires sur le Modèle
- Tests unitaires sur les "Translator" permettant de traduire Entités en Dto, etc...

# Ce qu'il nous reste à faire pour le deuxième jalon

### Fonctionnalités :
- Unit of Work
- Repository
- Utilisation dans le projet

### Tests :
- Tests sur les nouveautés

# Informations complémentaires :
- Il est impossible de modifier un Groupe.
- Il est impossible de rajouter des critères à la création d'un Template.

# Jalon 2

## Diagramme de paquetage

```mermaid
  flowchart TB
    EF_ConsoleTests -.-> EF_StubbedContextLib -.-> EF_DbContextLib -.-> EF_Entities;

    Model2Entities --> EF_DbContextLib;
    Model2Entities -.-> Client_Model;
    Model2Entities -.-> Shared;

    Entities2Dto --> EF_DbContextLib;
    Entities2Dto -.-> API_Dto;
    Entities2Dto -.-> Shared;
    

    Dto2Model -.-> Shared;
    Dto2Model -.-> API_Dto;
    Dto2Model -.-> Client_Model;
    
    Shared -.-> EF_Entities
    Shared -.-> API_Dto
    Shared -.-> Client_Model

    API_EF -.-> EventLogs;
    API_EF -.-> Entities2Dto;
    
    UnitTests -.-> Entities2Dto;
    UnitTests -.-> EF_DbContextLib;
    UnitTests -.-> Model2Entities;
    UnitTests -.-> Dto2Model;

    UnitOfWork -.-> EF_DbContextLib;
    UnitOfWork -.-> Repository;
    Entities2Dto -.-> UnitOfWork;

```

## Ce que l'on a fait pour le premier jalon
### Fonctionnalités :
- Unit of Work
- Repository
- Utilisation dans le projet
- Déploiement de l'API sur CodeFirst à cette adresse : https://codefirst.iut.uca.fr/containers/benjaminpaczkowski-studentevaluator-api

# Informations complémentaires :
- Il est impossible de modifier un Groupe.
- Il est impossible de rajouter des critères à la création d'un Template.
- La majorité des requêtes sur les endpoints de l'API nécessitent une authentification. 
- Seule Register et Login ne nécessitent pas d'authentification.
- Les tests  Entity Framework utilisent une base de données SQLite en mémoire sans le Stub.
- Le Stub n'est plus utilisé car beaucoup de nos entités nécessitent l'id de nos users pour être créées et le Stub & IdentityUser ne permet pas de fixer un ID. Nos données Stubbées sont alors crées avec la classe SeedData qui permet de les créer et les ajouter.