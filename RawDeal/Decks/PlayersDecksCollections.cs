using RawDeal.Cards;
using RawDeal.Collections;
using RawDeal.Exceptions;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView.Formatters;

namespace RawDeal.Decks;

public class PlayersDecksCollections
{
    private SuperStar _superStar;
    private DeckCollection _cardsInRingside;
    private DeckCollection _cardsInRingArea;
    private DeckCollection _cardsInHand;
    private DeckCollection _cardsInArsenal;
    private StringCollection _playeablesCardsInHandInStringFormat;
    private StringCollection _reversalCardsInHandInStringFormat;
    private CardRepresentationCollection _playeablesCardsInHand;
    private CardRepresentationCollection _reversalCardsInHand;
    private FormatterCardRepresentation _cardPlayedByOpponent;
    
    private bool _hasHeel;
    private bool _hasFace;
    
    private const int MaxDeckSize = 60;
    private const int EmptyDeck = 0;
    private const string ReversalCardType = "Reversal";
    private const string CardPlayAsAction = "Action";
    
    public PlayersDecksCollections(SuperStar superStar)
    {
        _superStar = superStar;
        _cardsInArsenal = new DeckCollection(new List<Card>());
        _cardsInHand = new DeckCollection(new List<Card>());
        _cardsInRingside = new DeckCollection(new List<Card>());
        _cardsInRingArea = new DeckCollection(new List<Card>());
    }
    
    public void CheckIfHaveValidDeckWhenYouAddCard(Card cardToAdd)
    {
        var numberOfRepeatedCards = _cardsInArsenal.Count(cardInDeck => cardInDeck.Title == cardToAdd.Title);
        IsInvalidUniqueCard(cardToAdd, numberOfRepeatedCards);
        IsInvalidNumberOfRepeatedCards(numberOfRepeatedCards, cardToAdd);
        IsInvalidFaceAndHeelCombination(cardToAdd);
        IsDeckSizeExceeded();
        IsInvalidLogo(cardToAdd);
    }
    
    private static void IsInvalidUniqueCard(Card card, int numberOfRepeatedCards)
    {
        const int numberOfRepeatedCardsAllowed = 0;
        if (card.Subtypes!.Contains("Unique") && numberOfRepeatedCards > numberOfRepeatedCardsAllowed)
            throw new InvalidDeckException();
    }
    
    private static void IsInvalidNumberOfRepeatedCards(int numberOfRepeatedCards, Card card)
    {
        const int numberOfRepeatedCardsAllowed = 3;
        if (!card.Subtypes!.Contains("SetUp") && numberOfRepeatedCards >= numberOfRepeatedCardsAllowed)
            throw new InvalidDeckException();
    }

    private void IsInvalidFaceAndHeelCombination(Card card)
    {
        if (PlayerAlreadyHasFaceOrHeel(card)) throw new InvalidDeckException();
        if (card.Subtypes!.Contains("Heel")) _hasHeel = true; 
        if (card.Subtypes.Contains("Face")) _hasFace = true; 
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
    
    private void IsInvalidLogo(Card card)
    {
        if (card.CheckIfHaveAnotherLogo(_superStar.SuperCard)) throw new InvalidDeckException();
    }
    
    public void IsDeckSizeCorrect()
    {
        if (_cardsInArsenal.Count != MaxDeckSize) throw new InvalidDeckException();
    }

    public void AddCardToHandFromArsenal()
    {
        Card card = _cardsInArsenal.Last();
        _cardsInHand.Add(card);
        _cardsInArsenal.Remove(card);
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
    
    public void AddCardToArsenal(Card card)
    {
        _cardsInArsenal.Add(card);
    }

    private int GetFortitude()
    {
        return _cardsInRingArea.Sum(card => int.Parse(card.Damage!));
    } 
    
    public Tuple<CardRepresentationCollection, StringCollection> MakeAListOfCardsThatArePlayeableFromHand()
    {
        _playeablesCardsInHand = new CardRepresentationCollection(new List<FormatterCardRepresentation>());
        _playeablesCardsInHandInStringFormat = new StringCollection(new List<string>());
        foreach (var card in _cardsInHand.Where(card => card.IsPlayeableCard(GetFortitude())))
        {
            AddAllTypesToPlayeableCardsList(card);
        }
        return new Tuple<CardRepresentationCollection, StringCollection>(_playeablesCardsInHand, _playeablesCardsInHandInStringFormat);
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
    
    public CardRepresentationCollection MakeAListOfReversalCardsOnCardFormat()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHand;
    }
    
    public StringCollection MakeAListOfReversalCardsInStringFormat()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHandInStringFormat;
    }
    
    public void SetTheCardPlayedByOpponent(FormatterCardRepresentation card)
    {
        _cardPlayedByOpponent = card;
    }
    
    public void TryReversalFromMaze(Card card, int remainingDamage)
    {
        if (!CheckReversalOfTheCardPlayedByTheOpponent(card)) return;
        int stunValue = CheckHayManyCardsCanTheOpponentStealFromDeckByHisStunValue(remainingDamage);
        _cardPlayedByOpponent = new FormatterCardRepresentation();
        throw new ReversalFromDeckException(stunValue);
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
    
    private bool CheckReversalOfTheCardPlayedByTheOpponent(Card card)
    {
        bool isReversal = _cardPlayedByOpponent.Type != null;
        if (isReversal)
        {
            isReversal = CheckReversalForAction(card) || CheckReversalForCardType(card);
        }
        return isReversal;
    }
    
    // TODO: Enviar a la carta
    private bool CheckReversalForAction(Card card)
    {
        return _cardPlayedByOpponent.Type == CardPlayAsAction.ToUpper() &&
               card.Subtypes!.Any(subtype =>
                   subtype.Contains("ReversalAction") && GetFortitude() >= int.Parse(card.Fortitude));
    }

    // TODO: Enviar a la carta
    private bool CheckReversalForCardType(Card card)
    {
        Card cardPlayedByOpponent = _cardPlayedByOpponent.CardInObjectFormat!;
        return (from subtype in card.Subtypes! 
            where card.CanBeUsedAsReversal(GetFortitude()) && _cardPlayedByOpponent.Type != CardPlayAsAction.ToUpper()
            select subtype.Split(ReversalCardType)[1]).Any(typeOfReversal => cardPlayedByOpponent.Subtypes!.Contains(typeOfReversal));
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
    
    public bool PlayerHasAllConditionsToPlayReversalFromHand()
    {
        MakeAListOfReversalCardsOnCardFormat();
        bool hasConditions = _reversalCardsInHand.Count != EmptyDeck
                             && _reversalCardsInHand.Any(card =>
                                 CheckReversalOfTheCardPlayedByTheOpponent(card.CardInObjectFormat!)); 
        return hasConditions;
    }
    
    
    
    
    
    
    public int MoveCardFromHandToRingArea(Card card)
    {
        _cardsInHand.Remove(card);
        _cardsInRingArea.Add(card);
        return int.Parse(card.Damage!);
    } 
    
    public void MoveCardFromHandToRingside(Card card)
    {
        _cardsInHand.Remove(card);
        _cardsInRingside.Add(card);
    }
    
    public void MoveCardFromHandToArsenal(Card card)
    {
        _cardsInHand.Remove(card);
        _cardsInArsenal.Insert(0, card);
    }
    
    public void MoveCardFromArsenalToRingSide(Card card)
    {
        _cardsInArsenal.Remove(card);
        _cardsInRingside.Add(card);
    }

    public void MoveCardFromArsenalToHand(Card card)
    {
        _cardsInArsenal.Remove(card);
        _cardsInHand.Add(card);
    }
    
    public void MoveCardFromRingsideToArsenal(Card card)
    {
        _cardsInRingside.Remove(card);
        _cardsInArsenal.Insert(0, card);
    }
    
    public void MoveCardFromRingsideToHand(Card card)
    {
        _cardsInRingside.Remove(card);
        _cardsInHand.Add(card);
    }
    
    public bool IsArsenalDeckEmpty()
    {
        return _cardsInArsenal.Count == EmptyDeck;
    }
    
    public DeckCollection GetRingsideDeck()
    {
        return _cardsInRingside;
    }

    public DeckCollection GetHandCards()
    {
        return _cardsInHand;
    }
    
    public DeckCollection GetArsenalDeck()
    {
        return _cardsInArsenal;
    }
    
}