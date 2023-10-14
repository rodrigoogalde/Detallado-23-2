using RawDeal.Cards;
using RawDeal.SuperStarsCards;
using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;

namespace RawDeal;

public class Player
{
    private readonly View _view;
    public SuperCard? SuperStarCardInfo;
    public SuperStar SuperStar;
    private readonly List<Card>? _cardsInArsenal;
    private List<Card> _cardsInHand;
    private List<Card> _cardsInRingside;
    private List<Card> _cardsInRingArea;
    
    private readonly string? _pathDeck;
    private int _fortitude;
    
    private bool _hasHeel;
    private bool _hasFace;

    private const int _maxDeckSize = 60;

    public Player(string pathDeck, View view)
    {
        _pathDeck = pathDeck;
        _cardsInArsenal = new List<Card>();
        _cardsInHand = new List<Card>();
        _cardsInRingside = new List<Card>();
        _cardsInRingArea = new List<Card>();
        _view = view;
        
        ReadDeck();
    }

    private void ReadDeck()
    {
        var deckLines  = File.ReadAllLines(_pathDeck!);
        SetSuperCard(deckLines);
        AddCardsFromTxtToDeck(deckLines.Skip(1).ToArray());
        IsDeckSizeCorrect(); 
    }
    
    private void SetSuperCard(IReadOnlyList<string> deckLines)
    {
        var superName = deckLines[0].Replace(" (Superstar Card)", "");
        SuperStarCardInfo = new SuperCard(superName);
        SetSuperStar();
    }

    private void SetSuperStar()
    {
        if (SuperStarCardInfo!.Name == "THE UNDERTAKER") { SuperStar = new Theundertaker(SuperStarCardInfo, this, _view); }
        else if (SuperStarCardInfo.Name == "STONE COLD STEVE AUSTIN") { SuperStar = new Stonecold(SuperStarCardInfo, this, _view); }
        else if (SuperStarCardInfo.Name == "CHRIS JERICHO") { SuperStar = new Chrisjericho(SuperStarCardInfo, this, _view); }
        else if (SuperStarCardInfo.Name == "HHH") { SuperStar = new Hhh(SuperStarCardInfo, this, _view); }
        else if (SuperStarCardInfo.Name == "THE ROCK") { SuperStar = new Therock(SuperStarCardInfo, this, _view); }
        else if (SuperStarCardInfo.Name == "KANE") { SuperStar = new Kane(SuperStarCardInfo, this, _view); }
        else if (SuperStarCardInfo.Name == "MANKIND") { SuperStar = new Mankind(SuperStarCardInfo, this, _view); }
        
    }
    
    private void AddCardsFromTxtToDeck(IReadOnlyList<string> deckLines)
    {
        foreach (var line in deckLines)
        {
            AddCardToDeck(new Card(line));
        }
    }
    
    private void AddCardToDeck(Card card)
    {
        card.CheckIfHaveAnotherLogo(SuperStarCardInfo!);
        CheckIfHaveValidDeckWhenYouAddCard(card);
        _cardsInArsenal!.Add(card);
    }
    
    private void CheckIfHaveValidDeckWhenYouAddCard(Card cardToAdd)
    {
        var numberOfRepeatedCards = _cardsInArsenal!.Count(cardInDeck => cardInDeck.Title == cardToAdd.Title);
        IsInvalidUniqueCard(cardToAdd, numberOfRepeatedCards);
        IsInvalidNumberOfRepeatedCards(numberOfRepeatedCards, cardToAdd);
        IsInvalidFaceAndHeelCombination(cardToAdd);
        IsDeckSizeExceeded();
        IsInvalidLogo(cardToAdd);
    }

    public void DrawCard()
    {
        Card card = _cardsInArsenal!.Last();
        _cardsInHand.Add(card);
        _cardsInArsenal!.Remove(card);
    }

    public void DrawFirstHand()
    {
        var cardsToDraw = SuperStarCardInfo!.HandSize;
        for (var i = 0; i < cardsToDraw; i++)
        {
            DrawCard();
        }
    }

    public List<string> ChooseWhichMazeOfCardsTransformToStringFormat(CardSet cardSet)
    {
        return cardSet switch
        {
            CardSet.Hand => TransformListOfCardsIntoStringFormat(_cardsInHand),
            CardSet.Arsenal => TransformListOfCardsIntoStringFormat(_cardsInArsenal!),
            CardSet.RingsidePile => TransformListOfCardsIntoStringFormat(_cardsInRingside),
            CardSet.RingArea => TransformListOfCardsIntoStringFormat(_cardsInRingArea),
            _ => new List<string>()
        };
    }

    private List<string> TransformListOfCardsIntoStringFormat(List<Card> cards) =>
        (cards.Count > 0)
            ? cards.Select(card => Formatter.CardToString(new FormaterCardInfo(card))).ToList()
            : new List<string>();

    public List<string> TransformPlayableCardsIntoHandInStringFormat()
    {
        return _cardsInHand.Where(IsPlayableCard).Select(card => Formatter.PlayToString(new FormaterPlayableCardInfo(card, card.Types![0].ToUpper()))).ToList();
    }
    
    public Card DiscardCardPlayableFromHandToRingside(int indexCardToDiscard)
    {
        List<string> playableCardsInHand = TransformPlayableCardsIntoHandInStringFormat();
        Card cardToDiscard = FindCardsInHandFromPlayableCardInfo(playableCardsInHand, indexCardToDiscard);
        _view.SayThatPlayerIsTryingToPlayThisCard(SuperStarCardInfo!.Name, playableCardsInHand[indexCardToDiscard]);
        MoveCardFromHandToRingArea(cardToDiscard);
        return cardToDiscard;
    }
    
    private Card FindCardsInHandFromPlayableCardInfo(List<string> playableCardsInHand, int indexCardToDiscard) 
    {
        Card cardToDiscard = _cardsInHand.Find(card => Formatter.PlayToString(new FormaterPlayableCardInfo(card, card.Types![0].ToUpper())) == playableCardsInHand[indexCardToDiscard])!;
        return CheckIfTheCardYouAreLookingForIsInTheFirstPositionOrAnother(cardToDiscard, indexCardToDiscard);
    }

    private Card CheckIfTheCardYouAreLookingForIsInTheFirstPositionOrAnother(Card cardToDiscard, int indexCardToDiscard)
    {
        var index = 0;
        foreach (var card in _cardsInHand)
        {
            if (IsPlayableCard(card) && cardToDiscard.Title == card.Title && index == indexCardToDiscard) cardToDiscard = card;
            if (IsPlayableCard(card)) index++; 
        }
        return cardToDiscard;
    }
    
    private void MoveCardFromHandToRingArea(Card cardToDiscard)
    {
        _cardsInHand.Remove(cardToDiscard);
        _cardsInRingArea.Add(cardToDiscard);
        _fortitude += int.Parse(cardToDiscard.Damage!);
    }

    public bool TakeDamage(int damage)
    {
        int i;
        for (i = 0; i < damage && _cardsInArsenal!.Count > 0; i++)
        {
            MoveCardFromArsenalToRingSide(i + 1, damage);
        }
        return _cardsInArsenal!.Count == 0 && i != damage;
    }
    
    private void MoveCardFromArsenalToRingSide(int currentDamage, int totalDamage)
    {
        Card card = _cardsInArsenal!.Last();
        _view.ShowCardOverturnByTakingDamage(Formatter.CardToString(new FormaterCardInfo(card)), currentDamage, totalDamage);
        _cardsInArsenal!.Remove(card);
        _cardsInRingside.Add(card);
    }

    public void MoveCardFromRingsideToArsenal(int index)
    {
        Card cardToRecover = _cardsInRingside[index];
        _cardsInRingside.Remove(cardToRecover);
        _cardsInArsenal!.Insert(0, cardToRecover);
    }
    
    public void DiscardCardFromHandToRingside(int index)
    {
        Card cardToDiscard = _cardsInHand[index];
        _cardsInHand.Remove(cardToDiscard);
        _cardsInRingside.Add(cardToDiscard);
    }
    
    public void MoveCardFromRingsideToHand(int index)
    {
        Card cardToRecover = _cardsInRingside[index];
        _cardsInRingside.Remove(cardToRecover);
        _cardsInHand.Add(cardToRecover);
    }

    public void MoveTopeCardFromArsenalToHand()
    {
        Card card = _cardsInArsenal!.Last();
        _cardsInArsenal!.Remove(card);
        _cardsInHand.Add(card);
    }
    
    public void MoveCardFromHandToArsenalBottom(int index)
    {
        Card card = _cardsInHand[index];
        _cardsInHand.Remove(card);
        _cardsInArsenal!.Insert(0, card);
    }
    
    private static void IsInvalidUniqueCard(Card card, int numberOfRepeatedCards)
    {
        int numberOfRepeatedCardsAllowed = 0;
        if (card.Subtypes!.Contains("Unique") && numberOfRepeatedCards > numberOfRepeatedCardsAllowed)
            throw new InvalidDeckException();
    }
    
    private static void IsInvalidNumberOfRepeatedCards(int numberOfRepeatedCards, Card card)
    {
        int numberOfRepeatedCardsAllowed = 3;
        if (!card.Subtypes!.Contains("SetUp") && numberOfRepeatedCards >= numberOfRepeatedCardsAllowed)
        {
            throw new InvalidDeckException();
        }
    }
    
    private void IsInvalidFaceAndHeelCombination(Card card)
    {
        if ((_hasFace && card.Subtypes!.Contains("Heel")) || (_hasHeel && card.Subtypes!.Contains("Face")))
            throw new InvalidDeckException();
        if (card.Subtypes!.Contains("Heel"))  _hasHeel = true; 
        if (card.Subtypes.Contains("Face"))  _hasFace = true; 
    }
    
    private void IsDeckSizeExceeded()
    {
        if (_cardsInArsenal!.Count >= _maxDeckSize)
            throw new InvalidDeckException();
    }
    
    private static void IsInvalidLogo(Card card)
    {
        if (card.HasAnotherLogo) throw new InvalidDeckException();
    }
    
    private void IsDeckSizeCorrect()
    {
        if (_cardsInArsenal!.Count != _maxDeckSize) throw new InvalidDeckException();
    }

    private bool IsPlayableCard(Card card)
    {
        return (card.Types!.Contains("Maneuver") || card.Types.Contains("Action")) 
               && _fortitude >= long.Parse(card.Fortitude) ;
    }
    public PlayerInfo GetPlayerInfo()
    {
        return new PlayerInfo(SuperStarCardInfo!.Name, _fortitude, _cardsInHand.Count, _cardsInArsenal!.Count);
    }
}