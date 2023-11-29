using RawDeal.Cards;
using RawDeal.Cards.Reversal;
using RawDeal.Collections;
using RawDeal.Exceptions;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;
using RawDealView.Formatters;

namespace RawDeal.Decks;

public class PlayerDecksCollections
{
    private SuperStar _superStar;
    private DeckListCollection _cardsInRingside;
    private DeckListCollection _cardsInRingArea;
    private DeckListCollection _cardsInHand;
    private DeckListCollection _cardsInArsenal;
    private StringListCollection _playeablesCardsInHandInStringListFormat;
    private StringListCollection _reversalCardsInHandInStringListFormat;
    private CardRepresentationListCollection _playeablesCardsInHand;
    private CardRepresentationListCollection _reversalCardsInHand;
    private FormatterCardRepresentation _cardPlayedByOpponent;
    private ICardTypeStrategy _cardTypeStrategyPlayedByOpponent;
    private CardsStrategiesFactory _factory;
    private SelectedEffectFull _selectedEffect;
    private Game _game;
    
    private bool _hasHeel;
    private bool _hasFace;
    
    private const int MaxDeckSize = 60;
    private const int EmptyDeck = 0;
    private const string ReversalCardType = "Reversal";
    private const string UniqueCardType = "Unique";
    private const string SetUpCardType = "SetUp";
    private const int GrappleFortitudePlusForReversal = 8;
    
    private const string HeelCardSubType = "Heel";
    private const string FaceCardSubType = "Face";
    private const string ReversalActionCardSubType = "ReversalAction";
    
    private const string CardPlayAsAction = "Action";
    
    public PlayerDecksCollections(SuperStar superStar, CardsStrategiesFactory factory, Game game)
    {
        _superStar = superStar;
        _cardsInArsenal = new DeckListCollection(new List<Card>());
        _cardsInHand = new DeckListCollection(new List<Card>());
        _cardsInRingside = new DeckListCollection(new List<Card>());
        _cardsInRingArea = new DeckListCollection(new List<Card>());
        _factory = factory;
        _game = game;
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
        if (card.Subtypes!.Contains(UniqueCardType) && numberOfRepeatedCards > numberOfRepeatedCardsAllowed)
            throw new InvalidDeckException();
    }
    
    private static void IsInvalidNumberOfRepeatedCards(int numberOfRepeatedCards, Card card)
    {
        const int numberOfRepeatedCardsAllowed = 3;
        if (!card.Subtypes!.Contains(SetUpCardType) && numberOfRepeatedCards >= numberOfRepeatedCardsAllowed)
            throw new InvalidDeckException();
    }

    private void IsInvalidFaceAndHeelCombination(Card card)
    {
        if (PlayerAlreadyHasFaceOrHeel(card)) throw new InvalidDeckException();
        if (card.Subtypes!.Contains(HeelCardSubType)) _hasHeel = true; 
        if (card.Subtypes.Contains(FaceCardSubType)) _hasFace = true; 
    }
    
    private bool PlayerAlreadyHasFaceOrHeel(Card card)
    {
        return (_hasFace && card.Subtypes!.Contains(HeelCardSubType)) 
               || (_hasHeel && card.Subtypes!.Contains(FaceCardSubType));
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
    
    public StringListCollection ChooseWhichMazeOfCardsTransformToStringFormat(CardSetFull cardSet)
    {
        var cardsInStringFormat = new StringListCollection( new List<string>());
        switch (cardSet)
        {
            case CardSetFull.Arsenal:
                cardsInStringFormat = new StringListCollection(_cardsInArsenal.TransformListOfCardsIntoStringFormat());
                break;
            case CardSetFull.Hand:
                cardsInStringFormat = new StringListCollection(_cardsInHand.TransformListOfCardsIntoStringFormat());
                break;
            case CardSetFull.RingArea or CardSetFull.OpponentsRingArea:
                cardsInStringFormat = new StringListCollection(_cardsInRingArea.TransformListOfCardsIntoStringFormat());
                break;
            case CardSetFull.RingsidePile or CardSetFull.OpponentsRingsidePile:
                cardsInStringFormat = new StringListCollection(_cardsInRingside.TransformListOfCardsIntoStringFormat());
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
    
    public Tuple<CardRepresentationListCollection, StringListCollection> MakeAListOfCardsThatArePlayeableFromHand()
    {
        _playeablesCardsInHand = new CardRepresentationListCollection(new List<FormatterCardRepresentation>());
        _playeablesCardsInHandInStringListFormat = new StringListCollection(new List<string>());
        foreach (var card in _cardsInHand.Where(card => card.IsPlayeableCard(GetFortitude())))
        {
            AddAllTypesToPlayeableCardsList(card);
        }
        return new Tuple<CardRepresentationListCollection, StringListCollection>(_playeablesCardsInHand, _playeablesCardsInHandInStringListFormat);
    }
    
    private void AddAllTypesToPlayeableCardsList(Card card)
    {
        foreach (var type in card.Types!)
        {
            if (type == ReversalCardType) continue;
            var formaterPlayableCardInfo = Formatter.PlayToString(new FormatterPlayableCardInfo(card, type.ToUpper()));
            _playeablesCardsInHandInStringListFormat.Add(formaterPlayableCardInfo);
            _playeablesCardsInHand.Add( new FormatterCardRepresentation
            {
                CardInObjectFormat = card,
                CardInStringFormat = formaterPlayableCardInfo,
                Type = type.ToUpper()
            });
        }
    }
    
    public CardRepresentationListCollection MakeAListOfReversalCardsOnCardFormat()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHand;
    }
    
    public StringListCollection MakeAListOfReversalCardsInStringFormat()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHandInStringListFormat;
    }
    
    public void SetTheCardPlayedByOpponent(FormatterCardRepresentation card)
    {
        _cardPlayedByOpponent = card;
        if (card.CardInObjectFormat == null) return;
        _cardTypeStrategyPlayedByOpponent = _factory.BuildCard(card.CardInObjectFormat!);
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
        _reversalCardsInHand = new CardRepresentationListCollection(new List<FormatterCardRepresentation>());
        _reversalCardsInHandInStringListFormat = new StringListCollection(new List<string>());
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
        _cardTypeStrategyPlayedByOpponent = _factory.BuildCard(card);
        bool isReversal = _cardPlayedByOpponent.Type != null;
        bool isReversal2 = _cardTypeStrategyPlayedByOpponent != null;
        
        if (isReversal)
        {
            isReversal = CheckReversalForAction(card) || CheckReversalForCardType(card);
        }
        if (!isReversal2) return isReversal && isReversal2;
        
        int fortitude = _game.OptionChoosedForJockeyingForPosition == SelectedEffectFull.NextGrapplesReversalIsPlus8F ? 
            GrappleFortitudePlusForReversal : 0;
        isReversal2 = _cardTypeStrategyPlayedByOpponent!.IsEffectApplicable() &&
                      GetFortitude() >= int.Parse(card.Fortitude) + fortitude &&
                      CheckIfIsReversal(card);
        return isReversal2;
    }
    
    private bool CheckIfIsReversal(Card card)
    {
        return card.Subtypes!.Any(subtype => subtype.Contains(ReversalCardType));
    }
    
    private bool CheckReversalForAction(Card card)
    {
        return _cardPlayedByOpponent.Type == CardPlayAsAction.ToUpper() &&
               card.Subtypes!.Any(subtype =>
                   subtype.Contains(ReversalActionCardSubType) && GetFortitude() >= int.Parse(card.Fortitude));
    }

    private bool CheckReversalForCardType(Card card)
    {
        Card cardPlayedByOpponent = _cardPlayedByOpponent.CardInObjectFormat!;
        return (from subtype in card.Subtypes! 
            where card.CanBeUsedAsReversal(GetFortitude(), ReversalCardType) && _cardPlayedByOpponent.Type != CardPlayAsAction.ToUpper() && subtype.Contains(ReversalCardType)
            select subtype.Split(ReversalCardType)[1]).Any(typeOfReversal => cardPlayedByOpponent.Subtypes!.Contains(typeOfReversal));
    }
    
    private void AddAllTypesToReversalCardsList(Card card)
    {
        foreach (var type in card.Types!)
        {
            if (!type.Contains(ReversalCardType)) continue;
            var formaterPlayableCardInfo = Formatter.PlayToString(new FormatterPlayableCardInfo(card, type.ToUpper()));
            _reversalCardsInHandInStringListFormat.Add(formaterPlayableCardInfo);
            _reversalCardsInHand.Add( new FormatterCardRepresentation
            {
                CardInObjectFormat = card,
                CardInStringFormat = formaterPlayableCardInfo,
                Type = type.ToUpper(),
                CardTypeStrategy = _factory.BuildCard(card)
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
    
    public void MoveCardBetweenDecks(Card card, Tuple<CardSetFull, CardSetFull> cardsFromTo)
    {
        DeckListCollection deckFrom = ChooseDeck(cardsFromTo.Item1);
        DeckListCollection deckTo = ChooseDeck(cardsFromTo.Item2);
        RemoveCardFromDeck(card, deckFrom);
        if (cardsFromTo.Item2 == CardSetFull.Arsenal) { InsertCardIntoDeck(card, deckTo); }
        else { AddCardToDeck(card, deckTo); }
    }
    
    private DeckListCollection ChooseDeck(CardSetFull cardsFrom)
    {
        DeckListCollection deckFrom = new DeckListCollection(new List<Card>());
        switch (cardsFrom)
        {
            case CardSetFull.Hand:
                deckFrom = _cardsInHand;
                break;
            case CardSetFull.RingsidePile:
                deckFrom = _cardsInRingside;
                break;
            case CardSetFull.Arsenal:
                deckFrom = _cardsInArsenal;
                break;
            case CardSetFull.RingArea:
                deckFrom = _cardsInRingArea;
                break;
        }
        return deckFrom;
    }

    private void RemoveCardFromDeck(Card card, DeckListCollection deckFrom)
    {
        deckFrom.Remove(card);
    }
    
    private void InsertCardIntoDeck(Card card, DeckListCollection deckTo)
    {
        deckTo.Insert(0, card);
    }
    
    private void AddCardToDeck(Card card, DeckListCollection deckTo)
    {
        deckTo.Add(card);
    }
    
    public bool IsArsenalDeckEmpty()
    {
        return _cardsInArsenal.Count == EmptyDeck;
    }
    
    public DeckListCollection GetRingsideDeck()
    {
        return _cardsInRingside;
    }

    public DeckListCollection GetHandCards()
    {
        return _cardsInHand;
    }
    
    public DeckListCollection GetArsenalDeck()
    {
        return _cardsInArsenal;
    }

    public FormatterCardRepresentation GetLastCardPlayedByOpponent()
    {
        return _cardPlayedByOpponent;
    }
    
    public void SetSelectedEffect(SelectedEffectFull selectedEffect)
    {
        _selectedEffect = selectedEffect;
    }
}