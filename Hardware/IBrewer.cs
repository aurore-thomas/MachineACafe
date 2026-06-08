namespace Hardware;

public interface IBrewer
{
    /// <summary>
    /// Demande à la machine de faire couler un café.
    /// Si aucune dose d'eau n'était préchargée dans le bouilleur, la machine tentera d'en charger une.
    /// </summary>
    /// <returns>True si aucun problème, False si défaillance.</returns>
    bool MakeACoffee();

    /// <summary>
    /// Tire une dose d'eau depuis le réservoir vers le bouilleur.
    /// </summary>
    /// <returns>
    /// True si une dose a été tirée avec succès.
    /// False si le bouilleur contenait déjà une dose d'eau.
    /// False si aucune dose complète n'a pu être tirée.
    /// </returns>
    bool TryPullWater();

    /// <summary>
    /// Ajoute une dose de lait au mélange.
    /// Il est conseillé d'ajouter le lait en premier, sauf sur le cappuccino.
    /// </summary>
    /// <returns>True si aucun problème, False si défaillance.</returns>
    bool PourMilk();

    /// <summary>
    /// Ajoute une dose d'eau au mélange. Il est conseillé d'ajouter l'eau en dernier.
    /// Si aucune dose d'eau n'était dans le bouilleur, la machine tentera d'en charger une.
    /// </summary>
    /// <returns>True si aucun problème, False si défaillance.</returns>
    bool PourWater();

    /// <summary>
    /// Ajoute une dose de sucre au mélange. Il est conseillé d'ajouter le sucre en premier.
    /// </summary>
    /// <returns>True si aucun problème, False si défaillance.</returns>
    bool PourSugar();

    /// <summary>
    /// Ajoute une dose de chocolat au mélange.
    /// Il est conseillé d'ajouter le chocolat après le sucre mais avant les autres ingrédients.
    /// </summary>
    /// <returns>True si aucun problème, False si défaillance.</returns>
    bool PourChocolate();
}