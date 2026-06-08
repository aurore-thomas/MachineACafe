namespace Hardware;

public interface IChangeMachine
{
    /// <summary>
    /// Enregistre un callback, qui sera appelé lors de l'insertion d'une pièce reconnue valide.
    /// Attention : si le monnayeur est physiquement plein (plus de 5 pièces), cette méthode n'est plus invoquée.
    /// Il est de la responsabilité du logiciel de surveiller cela.
    /// </summary>
    /// <param name="callback">Callback prenant en paramètre la valeur de la pièce détectée.</param>
    void RegisterMoneyInsertedCallback(Action<CoinCode> callback);

    /// <summary>
    /// Vide le monnayeur et rend l'argent.
    /// </summary>
    void FlushStoredMoney();

    /// <summary>
    /// Vide le monnayeur et encaisse l'argent.
    /// </summary>
    void CollectStoredMoney();

    /// <summary>
    /// Fait tomber une pièce depuis le stock vers la trappe à monnaie.
    /// </summary>
    /// <param name="coinCode">Code de la pièce à rendre.</param>
    /// <returns>True si la pièce était disponible, False sinon.</returns>
    bool DropCashback(CoinCode coinCode);
}