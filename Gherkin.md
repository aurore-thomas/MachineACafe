# Spécifications d'Acceptation (Gherkin) - Machine à Café

## Scénario : Aucune Action
* **Étant donné** une machine à café
* **Quand** aucune action n'est réalisée
* **Alors** aucune invocation du Brewer ou de la ChangeMachine n'est effectuée

## Scénario : Cas Nominal
* **Étant donné** une machine à café
* **Quand** on insère une somme supérieure ou égale au prix d'un café
* **Alors** 1 café est servi
* **Et** l'argent est encaissé

## Scénario : Cas Brewer Défaillant
* **Étant donné** une machine à café ayant un brewer défaillant
* **Quand** on insère une somme supérieure ou égale au prix d'un café
* **Alors** l'argent est restitué

## Scénario : Pas Assez Argent
* **Étant donné** une machine à café
* **Quand** on insère 1 pièce qui vaut moins que le prix d'un café
* **Alors** aucun café n'est servi
* **Et** l'argent inséré n'est pas collecté ni rendu (le monnayeur attend d'autres pièces)

## Scénario : Cas 1 Café 2 Pièces
* **Étant donné** une machine à café
* **Quand** on insère 2 pièces de 20 centimes
* **Alors** MakeACoffee est appelé 1 fois sur le hardware
* **Et** CollectStoredMoney est appelé 1 fois sur le hardware
* **Et** FlushStoredMoney n'est pas appelé

## Scénario : Cas 1 Café 4 Pièces
* **Étant donné** une machine à café
* **Quand** on insère 4 pièces de 10 centimes
* **Alors** un café est servi
* **Et** l'argent est encaissé

## Scénario : Cas 2 Cafés Plusieurs Pièces
* **Étant donné** une machine à café
* **Quand** on insère 4 pièces de 20 centimes
* **Alors** 2 cafés sont servis
* **Et** l'argent est encaissé 2 fois

## Scénario : Cas 2 Cafés Avec 50 Cts
* **Étant donné** une machine à café
* **Quand** on insère 2 pièces de 50 centimes
* **Alors** 2 cafés sont servis
* **Et** l'argent est encaissé 2 fois

## Scénario : Cas Pas Assez Argent Avec 5 Pièces
* **Étant donné** une machine à café
* **Quand** on insère 4 pièces de 5 centimes
* **Alors** aucun café n'est servi
* **Et** l'argent est restitué

## Scénario : Cas Deux Pièces Insuffisantes
* **Étant donné** une machine à café
* **Quand** on insère deux pièces insuffisantes pour le prix d'un café (20 et 10 centimes)
* **Alors** aucun café n'est servi
* **Et** l'argent est en attente (la machine attend d'autres pièces)

## Scénario : Cas Deux Pièces Monnaie En Trop
* **Étant donné** une machine à café
* **Quand** on insère une pièce de 20 centimes et une pièce de 50 centimes
* **Alors** un café est servi
* **Et** l'argent est encaissé

## Scénario : Cas 1 Café 2 Pièces Avec Rendu
* **Étant donné** une machine à café
* **Quand** on insère 2 pièces dont la somme est supérieure au prix d'un café
* **Alors** 1 café est servi
* **Et** l'argent est encaissé et le surplus est rendu

## Scénario : Cas 1 Café 4 Pièces Avec Rendu
* **Étant donné** une machine à café
* **Quand** on insère 4 pièces dont la somme est supérieure au prix d'un café
* **Alors** 1 café est servi
* **Et** l'argent est encaissé et le surplus est rendu

## Scénario : Cas Monnayeur Vide
* **Étant donné** une machine à café avec un monnayeur vide (pas de monnaie pour rendre la différence)
* **Quand** on insère une somme supérieure ou égale au prix d'un café
* **Alors** 1 café est servi
* **Et** l'argent est encaissé mais le surplus n'est pas rendu

## Scénario : Cas Monnayeur Vide Avec Deux Pièces
* **Étant donné** une machine à café avec un monnayeur vide (pas de monnaie pour rendre la différence)
* **Quand** on insère 2 pièces chacune suffisante pour le prix d'un café
* **Alors** 2 cafés sont servis
* **Et** l'argent est encaissé 2 fois mais le surplus n'est pas rendu

## Scénario : Cas Rendu Monnaie Impossible Rend Zéro Et Sert Café
* **Étant donné** une machine à café avec uniquement des pièces de 5 centimes
* **Quand** on insère 50 centimes (10 centimes à rendre)
* **Alors** 1 café est servi
* **Et** l'argent est encaissé et la monnaie possible est rendue (5 centimes)

## Scénario : Cas Rendu Monnaie Adaptée Utilise Plusieurs Petites Pièces
* **Étant donné** une machine à café avec uniquement des pièces de 5 centimes
* **Quand** on insère 50 centimes (10 centimes à rendre)
* **Alors** 1 café est servi
* **Et** l'argent est encaissé et le surplus est rendu (2 pièces de 5 centimes)