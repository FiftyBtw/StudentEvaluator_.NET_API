# Web API StudentEvaluator

# Introduction

StudentEvaluator_API_EF est une API RESTful conçue pour simplifier la communication entre le site web et l'application Android du projet StudentEvaluator. Son objectif principal est de faciliter la gestion des évaluations, des modèles, des utilisateurs et d'autres données essentielles de manière efficace...

# Diagramme ?
# Ce que l'on a fait pour le premier jalon  : 


### Fonctionnalités :

- Lien Modèle <-> DTO
- Lien Entités <-> DTO
- Lien Modèle <-> Entités
- Logger (pas partout ?)
- Injection de service
- Injection / indépendance du fournisseur
- Pagination & Filtrage (Par ID)
- Requêtes CRUD sur des données simples et complexes (API et EF)
- Utilisations des relations One-to-One et One-to-Many
- Mapping (Entités <-> DTO )
- Authentification
- CI
- Lien API <-> EF



### Tests:
- Tests Unitaire - EF (SQLite in memory)
- Tests fonctionnel - EF avec données stubbées
- Tests Unitaire - API sur les controlleurs (Moq)
- Tests fonctionnel - API (consommation des requêtes)
- Test Unitaire sur les différents "Data manager" faisant le lien entre DTO,entités et modèle





