using RawDeal.Cards;
using RawDeal.Decks;
using RawDeal.Exceptions;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;
using RawDealView.Formatters;
using FormatterCardInfo = RawDeal.Cards.FormatterCardInfo;

namespace RawDeal;

public class Player
{
    private readonly View _view;
    public SuperStar SuperStar;
    private readonly DeckCollection _cardsInArsenal;
    private DeckCollection _cardsInRingside;
    private DeckCollection _cardsInRingArea;
    private DeckCollection _cardsInHand;
    private CardRepresentationCollection _playeablesCardsInHand;
    private CardRepresentationCollection _reversalCardsInHand;
    private StringCollection _playeablesCardsInHandInStringFormat;
    private StringCollection _reversalCardsInHandInStringFormat;
    private int _indexCardToDiscard;
    private FormatterCardRepresentation _cardPlayedByOpponent;
    
    private readonly string? _pathDeck;
    private int _fortitude;
    
    private bool _hasHeel;
    private bool _hasFace;

    private const int MaxDeckSize = 60;
    private const int EmptyDeck = 0;
    private const string ReversalCardType = "Reversal";

    public Player(string pathDeck, View view)
    {
        _pathDeck = pathDeck;
        _cardsInArsenal = new DeckCollection(new List<Card>());
        _cardsInHand = new DeckCollection(new List<Card>());
        _cardsInRingside = new DeckCollection(new List<Card>());
        _cardsInRingArea = new DeckCollection(new List<Card>());
        _cardPlayedByOpponent = new FormatterCardRepresentation();
        _reversalCardsInHandInStringFormat = new StringCollection(new List<string>());
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
        SetSuperStar(superName);
    }

    private void SetSuperStar(string superName)
    {
        var superStarCardInfo = new SuperCardInfo(superName);
        SuperStar = superStarCardInfo.Name switch
        {
            "THE UNDERTAKER" => new Theundertaker(superStarCardInfo, this, _view),
            "STONE COLD STEVE AUSTIN" => new Stonecold(superStarCardInfo, this, _view),
            "CHRIS JERICHO" => new Chrisjericho(superStarCardInfo, this, _view),
            "HHH" => new Hhh(superStarCardInfo, this, _view),
            "THE ROCK" => new Therock(superStarCardInfo, this, _view),
            "KANE" => new Kane(superStarCardInfo, this, _view),
            "MANKIND" => new Mankind(superStarCardInfo, this, _view),
            _ => SuperStar
        };
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
        card.CheckIfHaveAnotherLogo(SuperStar.SuperCard);
        CheckIfHaveValidDeckWhenYouAddCard(card);
        _cardsInArsenal.Add(card);
    }
    
    private void CheckIfHaveValidDeckWhenYouAddCard(Card cardToAdd)
    {
        var numberOfRepeatedCards = _cardsInArsenal.Count(cardInDeck => cardInDeck.Title == cardToAdd.Title);
        IsInvalidUniqueCard(cardToAdd, numberOfRepeatedCards);
        IsInvalidNumberOfRepeatedCards(numberOfRepeatedCards, cardToAdd);
        IsInvalidFaceAndHeelCombination(cardToAdd);
        IsDeckSizeExceeded();
        IsInvalidLogo(cardToAdd);
    }

    public void DrawCard()
    {
        Card card = _cardsInArsenal.Last();
        _cardsInHand.Add(card);
        _cardsInArsenal.Remove(card);
    }

    public void DrawFirstHand()
    {
        SuperCardInfo superCard = SuperStar.SuperCard;
        int cardsToDraw = superCard.HandSize;
        for (var i = 0; i < cardsToDraw; i++)
        {
            DrawCard();
        }
    }

    public StringCollection ChooseWhichMazeOfCardsTransformToStringFormat(CardSetFull cardSet)
    {
        var cardsInStringFormat = new StringCollection( new List<string>());
        switch (cardSet)
        {
            case CardSetFull.Arsenal:
                cardsInStringFormat = new StringCollection(_cardsInArsenal.TransformListOfCardsIntoStringFormat());
                break;
            case CardSetFull.Hand:
                cardsInStringFormat = new StringCollection(_cardsInHand.TransformListOfCardsIntoStringFormat());
                break;
            case CardSetFull.RingArea or CardSetFull.OpponentsRingArea:
                cardsInStringFormat = new StringCollection(_cardsInRingArea.TransformListOfCardsIntoStringFormat());
                break;
            case CardSetFull.RingsidePile or CardSetFull.OpponentsRingsidePile:
                cardsInStringFormat = new StringCollection(_cardsInRingside.TransformListOfCardsIntoStringFormat());
                break;
        }
        return cardsInStringFormat;
    }
    
    public StringCollection MakeAListOfPlayeableCards()
    {
        CheckWhichCardsArePlayeable();
        return _playeablesCardsInHandInStringFormat;
    }

    private void CheckWhichCardsArePlayeable()
    {
        _playeablesCardsInHand = new CardRepresentationCollection(new List<FormatterCardRepresentation>());
        _playeablesCardsInHandInStringFormat = new StringCollection(new List<string>());
        foreach (var card in _cardsInHand.Where(card => card.IsPlayeableCard(_fortitude)))
        {
            AddAllTypesToPlayeableCardsList(card);
        }
    }

    private void AddAllTypesToPlayeableCardsList(Card card)
    {
        foreach (var type in card.Types!)
        {
            var formaterPlayableCardInfo = Formatter.PlayToString(new FormatterPlayableCardInfo(card, type.ToUpper()));
            _playeablesCardsInHandInStringFormat.Add(formaterPlayableCardInfo);
            _playeablesCardsInHand.Add( new FormatterCardRepresentation
            {
                CardInObjectFormat = card,
                CardInStringFormat = formaterPlayableCardInfo,
                Type = type.ToUpper()
            });
        }
    }

    public List<string> MakeAListOfReversalCardsInStringFormat()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHandInStringFormat.ToList();
    }

    public CardRepresentationCollection MakeAListOfReversalCards()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHand;
    }
    
    private void CheckWhichCardsAreReversal()
    {
        _reversalCardsInHand = new CardRepresentationCollection(new List<FormatterCardRepresentation>());
        _reversalCardsInHandInStringFormat = new StringCollection(new List<string>());
        foreach (Card card in _cardsInHand)
        {
            if (CheckReversalOfTheCardPlayedByTheOpponent(card))
            {
                AddAllTypesToReversalCardsList(card);
            }
        }
    }
    
    private void AddAllTypesToReversalCardsList(Card card)
    {
        foreach (var type in card.Types!)
        {
            var formaterPlayableCardInfo = Formatter.PlayToString(new FormatterPlayableCardInfo(card, type.ToUpper()));
            _reversalCardsInHandInStringFormat.Add(formaterPlayableCardInfo);
            _reversalCardsInHand.Add( new FormatterCardRepresentation
            {
                CardInObjectFormat = card,
                CardInStringFormat = formaterPlayableCardInfo,
                Type = type.ToUpper()
            });
        }
    }
    
    public void PlayCardAsAction(Card cardToPlay)
    {   
        _view.SayThatPlayerMustDiscardThisCard(SuperStar.Name!, cardToPlay.Title);
        DrawCard();
        _view.SayThatPlayerDrawCards(SuperStar.Name!, 1);
        MoveCardFromHandToRingSide(cardToPlay);
    }
    public FormatterCardRepresentation CheckWhichCardWillBePlayed(int indexCardToDiscard)
    {
        _indexCardToDiscard = indexCardToDiscard;
        MakeAListOfPlayeableCards();
        var cardToDiscardInBothFormats = _playeablesCardsInHand[_indexCardToDiscard];
        _view.SayThatPlayerIsTryingToPlayThisCard(SuperStar.Name!, cardToDiscardInBothFormats.CardInStringFormat!);
        return cardToDiscardInBothFormats;
    }
    
    public void MoveCardFromHandToRingArea(Card cardToDiscard)
    {
        _cardsInHand.Remove(cardToDiscard);
        _cardsInRingArea.Add(cardToDiscard);
        _fortitude += int.Parse(cardToDiscard.Damage!);
    }

    public bool TakeDamage(int damage)
    {
        int i;
        for (i = 1; (i - 1) < damage && _cardsInArsenal.Count > EmptyDeck; i++)
        {
            Card card = MoveCardFromArsenalToRingSide();
            _view.ShowCardOverturnByTakingDamage(Formatter.CardToString(new FormatterCardInfo(card)), i, damage);
            TryReversalFromMaze(card, damage - i);
        }
        return _cardsInArsenal.Count == EmptyDeck && (i - 1) != damage;
    }

    private void TryReversalFromMaze(Card card, int remainingDamage)
    {
        if (!CheckReversalOfTheCardPlayedByTheOpponent(card)) return;
        int stunValue = CheckHayManyCardsCanTheOpponentStealFromDeckByHisStunValue(remainingDamage);
        _cardPlayedByOpponent = new FormatterCardRepresentation();
        throw new ReversalFromDeckException(stunValue);
    }
    
    private int CheckHayManyCardsCanTheOpponentStealFromDeckByHisStunValue(int remainingDamage)
    {
        Card card = _cardPlayedByOpponent.CardInObjectFormat!;
        int stunValue = 0;
        if (int.Parse(card.StunValue) > 0 && remainingDamage != 0)
        {
            stunValue = int.Parse(card.StunValue);
        }
        return stunValue;
    }
    
    public void SetTheCardPlayedByOpponent(FormatterCardRepresentation card)
    {
        _cardPlayedByOpponent = card;
    }
    
    private Card MoveCardFromArsenalToRingSide()
    {
        Card card = _cardsInArsenal.Last();
        _cardsInArsenal.Remove(card);
        _cardsInRingside.Add(card);
        return card;
    }
    
    public void MoveCardFromRingsideToArsenal(int index)
    {
        Card cardToRecover = _cardsInRingside[index];
        _cardsInRingside.Remove(cardToRecover);
        _cardsInArsenal.Insert(0, cardToRecover);
    }
    
    public void DiscardCardFromHandToRingside(int index)
    {
        Card cardToDiscard = _cardsInHand[index];
        _cardsInHand.Remove(cardToDiscard);
        _cardsInRingside.Add(cardToDiscard);
    }

    public void CardFromHandToRingside(Card card)
    {
        _cardsInHand.Remove(card);
        _cardsInRingside.Add(card);
    }
    
    public void MoveCardFromRingsideToHand(int index)
    {
        Card cardToRecover = _cardsInRingside[index];
        _cardsInRingside.Remove(cardToRecover);
        _cardsInHand.Add(cardToRecover);
    }
    
    public void MoveTopeCardFromArsenalToHand()
    {
        Card card = _cardsInArsenal.Last();
        _cardsInArsenal.Remove(card);
        _cardsInHand.Add(card);
    }
    
    public void MoveCardFromHandToArsenalBottom(int index)
    {
        Card card = _cardsInHand[index];
        _cardsInHand.Remove(card);
        _cardsInArsenal.Insert(0, card);
    }
    
    private void MoveCardFromHandToRingSide(Card cardToDiscard)
    {
        _cardsInHand.Remove(cardToDiscard);
        _cardsInRingside.Add(cardToDiscard);
    }
    
    private bool CheckReversalOfTheCardPlayedByTheOpponent(Card card)
    {
        bool isReversal = _cardPlayedByOpponent.Type != null;
        if (isReversal)
        {
            isReversal = CheckReversalForAction(card) || CheckReversalForCardType(card);
        }
        return isReversal;
    }

    private bool CheckReversalForAction(Card card)
    {
        return _cardPlayedByOpponent.Type == "ACTION" &&
               card.Subtypes!.Any(subtype =>
                   subtype.Contains("ReversalAction") && _fortitude >= int.Parse(card.Fortitude));
    }

    private bool CheckReversalForCardType(Card card)
    {
        Card cardPlayedByOpponent = _cardPlayedByOpponent.CardInObjectFormat!;
        return (from subtype in card.Subtypes! 
            where card.CanBeUsedAsReversal(_fortitude) && _cardPlayedByOpponent.Type != "ACTION"
            select subtype.Split(ReversalCardType)[1]).Any(typeOfReversal => cardPlayedByOpponent.Subtypes!.Contains(typeOfReversal));
    }

    
    public bool CanReverseTheCardPlayed()
    {
        CheckWhichCardsAreReversal();
        return PlayerHasAllConditionsToPlayReversalFromHand();
    }
    
    private bool PlayerHasAllConditionsToPlayReversalFromHand()
    {
        bool hasConditions = _reversalCardsInHand.Count != EmptyDeck
                            && _reversalCardsInHand.Any(card =>
                            CheckReversalOfTheCardPlayedByTheOpponent(card.CardInObjectFormat!)); 
        return hasConditions;
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
        if (PlayerAlreadyHasFaceOrHeel(card)) throw new InvalidDeckException();
        if (card.Subtypes!.Contains("Heel"))  _hasHeel = true; 
        if (card.Subtypes.Contains("Face"))  _hasFace = true; 
    }
    
    private bool PlayerAlreadyHasFaceOrHeel(Card card)
    {
        return (_hasFace && card.Subtypes!.Contains("Heel")) || (_hasHeel && card.Subtypes!.Contains("Face"));
    }
    
    private void IsDeckSizeExceeded()
    {
        if (_cardsInArsenal.Count >= MaxDeckSize)
            throw new InvalidDeckException();
    }
    
    private static void IsInvalidLogo(Card card)
    {
        if (card.HasAnotherLogo) throw new InvalidDeckException();
    }
    
    private void IsDeckSizeCorrect()
    {
        if (_cardsInArsenal.Count != MaxDeckSize) throw new InvalidDeckException();
    }
    
    public PlayerInfo GetPlayerInfo()
    {
        SuperCardInfo superCardInfo = SuperStar.SuperCard;
        return new PlayerInfo(superCardInfo.Name, _fortitude, _cardsInHand.Count, _cardsInArsenal.Count);
    }
    
    public void CleanDataFromPastTurn()
    {
        _cardPlayedByOpponent = new FormatterCardRepresentation();
    }
    
}