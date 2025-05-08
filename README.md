# Express Voitures 🚗

Prototype d'une application web de gestion et de vente de voitures d'occasion, développée avec ASP.NET Core MVC v.9.0.  
Ce projet répond à des spécifications fonctionnelles axées sur la simplicité, l'accessibilité (WCAG), la sécurité et une interface sobre.

---

## Fonctionnalités principales

- 🔐 Authentification avec ASP.NET Identity (accès admin uniquement)
- 📋 Ajout, édition et suppression logique de véhicules
- 📸 Upload d'images
- 🟢 Badge de disponibilité automatique
- 🧼 Interface conforme à la maquette
- ♿ Accessibilité vérifiée avec Lighthouse avec un score de 100

---

## Stack technique

- ASP.NET Core MVC (C#) v.9.0
- Entity Framework Core v.9.0.4
- Bootstrap 5
- Razor
- Razor Pages pour Identity
- SQL Server LocalDB

---

## Lancer le projet en local

1. Cloner le dépôt
2. Paramétrer la connexion SSMS avec pour nom de serveur : (localdb)\mssqllocaldb
3. Ouvrir Visual Studio et ouvrir le gestionnaire de packages
4. Add-Migration Initialisation puis Update-Database
5. Lancer l'application via IIS Express
6. Se connecter en Admin avec ces identifiants :
   Nom : admin@expressvoitures.fr
   Mdp : Admin123!

