# Plan de test


| Scope | Catégorie                       | Niveau      | Temporalité                 | Rationnelle                                          |
| ----- | ------------------------------- | ----------- | --------------------------- | ---------------------------------------------------- |
| *     | Fonctionnel -> Exactitude       | Acceptation | Test-First + Defect testing |                                                      |
| *     | Fonctionnel -> Complétude       | Manuel      | Test-Last                   | Run d'un outil de mutation testing                   |
| *     | Performance -> Temps de réponse | Intégration | Test-Last                   | Intégration car E2E impossible sans le vrai hardware |
| *     | Fiabilité -> Robustesse         | Intégration | Test-Last                   | Monkey testing et fault-tolerance                    |
| *     | Maintenabilité                  |             |                             | Analyse générale manuelle régulière                  |

