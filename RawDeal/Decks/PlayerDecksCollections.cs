using RawDeal.Cards;
using RawDeal.Cards.Formatter;
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
    private readonly SuperStar _superStar;
    private readonly DeckListCollection _cardsInRingside;
    private readonly DeckListCollection _cardsInRingArea;
    private readonly DeckListCollection _cardsInHand;
    private readonly DeckListCollection _cardsInArsenal;
    private StringListCollection? _playeablesCardsInHandInStringListFormat;
    private StringListCollection? _reversalCardsInHandInStringListFormat;
    private CardRepresentationListCollection? _playeablesCardsInHand;
    private CardRepresentationListCollection? _reversalCardsInHand;
    private FormatterCardRepresentation _cardPlayedByOpponent;
    private ICardTypeStrategy? _cardTypeStrategyPlayedByOpponent;
    private FormatterCardRepresentation _cardPlayed;
    private ICardTypeStrategy? _cardTypeStrategyPlayed;
    
    private readonly CardsStrategiesFactory _factory;
    private readonly Game _game;
    private readonly Player _player;
    private bool _hasHeel;
    private bool _hasFace;
    private const int MaxDeckSize = 60;
    private const int EmptyDeck = 0;
    private const string CardPlayAsAction = "Action";
    private const string ReversalCardType = "Reversal";
    private const string ManeuverCardType = "Maneuver";
    private const string UniqueCardType = "Unique";
    private const string SetUpCardType = "SetUp";
    private const int GrappleFortitudePlusForReversal = 8;
    private const string HeelCardSubType = "Heel";
    private const string FaceCardSubType = "Face";
    private const string ReversalActionCardSubType = "ReversalAction";
    private const string HibridCard = "Undertaker's Tombstone Piledriver";
    
    public PlayerDecksCollections(SuperStar superStar, CardsStrategiesFactory factory, Game game, Player player)
    {
        _superStar = superStar;
        _cardsInArsenal = new DeckListCollection(new List<Card>());
        _cardsInHand = new DeckListCollection(new List<Card>());
        _cardsInRingside = new DeckListCollection(new List<Card>());
        _cardsInRingArea = new DeckListCollection(new List<Card>());
        _factory = factory;
        _game = game;
        _player = player;
        _cardPlayedByOpponent = new FormatterCardRepresentation();
        _cardPlayed = new FormatterCardRepresentation();
    }
    
    public void CheckIfHaveValidDeckWhenYouAddCard(Card cardToAdd)
    {
        CheckDuplicateCards(cardToAdd);
        CheckIfTheCardAddedIsValid(cardToAdd);
        IsDeckSizeExceeded();
    }

    private void CheckDuplicateCards(Card cardToAdd)
    {
        var numberOfRepeatedCards = _cardsInArsenal.Count(cardInDeck => cardInDeck.Title == cardToAdd.Title);
        IsInvalidUniqueCard(cardToAdd, numberOfRepeatedCards);
        IsInvalidNumberOfRepeatedCards(cardToAdd, numberOfRepeatedCards);
    }

    private void CheckIfTheCardAddedIsValid(Card cardToAdd)
    {
        IsInvalidFaceAndHeelCombination(cardToAdd);
        IsInvalidLogo(cardToAdd);
    }
    
    private static void IsInvalidUniqueCard(Card card, int numberOfRepeatedCards)
    {
        const int numberOfRepeatedCardsAllowed = 0;
        if (card.Subtypes!.Contains(UniqueCardType) && numberOfRepeatedCards > numberOfRepeatedCardsAllowed)
            throw new InvalidDeckException();
    }
    
    private static void IsInvalidNumberOfRepeatedCards( Card card, int numberOfRepeatedCards)
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

    public bool AddCardToHandFromArsenal()
    {
        if (_cardsInArsenal.Count == EmptyDeck) return false;
        Card card = _cardsInArsenal.Last();
        _cardsInHand.Add(card);
        _cardsInArsenal.Remove(card);
        return true;
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

    public int GetFortitude()
    {
        return _cardsInRingArea.Sum(card => card.DamageValue);
    } 
    
    public Tuple<CardRepresentationListCollection, StringListCollection?> MakeAListOfCardsThatArePlayeableFromHand()
    {
        _playeablesCardsInHand = new CardRepresentationListCollection(new List<FormatterCardRepresentation>());
        _playeablesCardsInHandInStringListFormat = new StringListCollection(new List<string>());
        foreach (var card in _cardsInHand.Where(card => card.IsPlayeableCard(GetFortitude())))
        {
            AddAllTypesToPlayeableCardsList(card);
        }
        return new Tuple<CardRepresentationListCollection, StringListCollection?>(_playeablesCardsInHand,
            _playeablesCardsInHandInStringListFormat);
    }
    
    private void AddAllTypesToPlayeableCardsList(Card card)
    {
        foreach (var type in card.Types!)
        {
            if (type == ReversalCardType || !CheckSpecificConditions(card, type)) continue;
            var formaterPlayableCardInfo = Formatter.PlayToString(
                new FormatterPlayableCardInfo(card, type.ToUpper()));
            _playeablesCardsInHandInStringListFormat!.Add(formaterPlayableCardInfo);
            _playeablesCardsInHand!.Add( new FormatterCardRepresentation
            {
                CardInObjectFormat = card,
                CardInStringFormat = formaterPlayableCardInfo,
                Type = type.ToUpper()
            });
        }
    }
    
    private bool CheckSpecificConditions(Card card, string type)
    {
        _cardTypeStrategyPlayed = _factory.BuildCard(card, type.ToUpper());
        bool isEffectApplicable = _cardTypeStrategyPlayed.IsEffectApplicable();
        return card.Title switch
        {
            "Undertaker's Tombstone Piledriver" => CheckIfValidHibridCardType(card, type),
            "Lionsault" => isEffectApplicable,
            "Austin Elbow Smash" => isEffectApplicable,
            "Spit At Opponent" => isEffectApplicable,
            _ => true
        };
    }

    private bool CheckIfValidHibridCardType(Card card, string type)
    {
        bool isValidAsAction = type == CardPlayAsAction && GetFortitude() >= 0;
        bool isValidAsManeuver = type == ManeuverCardType && GetFortitude() >= long.Parse(card.Fortitude);
        return isValidAsAction || isValidAsManeuver;
    }
    
    public CardRepresentationListCollection? MakeAListOfReversalCardsOnCardFormat()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHand;
    }
    
    public StringListCollection? MakeAListOfReversalCardsInStringFormat()
    {
        CheckWhichCardsAreReversal();
        return _reversalCardsInHandInStringListFormat;
    }
    
    public void SetTheCardPlayedByOpponent(FormatterCardRepresentation card)
    {
        _cardPlayedByOpponent = card;
        if (card.Type == null) return;
        _cardTypeStrategyPlayedByOpponent = _factory.BuildCard(card.CardInObjectFormat!, card.Type!.ToUpper());
    }
    
    public void SetCardPlayed(FormatterCardRepresentation card)
    {
        _cardPlayed = card;
        if (card.Type == null) return;
        _cardTypeStrategyPlayed = _factory.BuildCard(card.CardInObjectFormat!, card.Type!.ToUpper());
    }
    
    public void TryReversalFromMaze(Card card, int remainingDamage)
    {
        if (!CheckReversalOfTheCardPlayedByTheOpponent(card)) return;
        int stunValue = CheckHayManyCardsCanTheOpponentStealFromDeckByHisStunValue(remainingDamage);
        PerformReversalFromMaze(card);
        CleanDataAfterReversalFromMaze();
        throw new ReversalFromDeckException(stunValue);
    }

    public void PerformManeuver(Card card, Player opponent)
    {
        FormatterCardRepresentation cardFormatter = CreateCardFormatter(card, ManeuverCardType);
        ICardTypeStrategy cardTypeStrategy = cardFormatter.CardTypeStrategy!;
        if (!cardTypeStrategy.IsEffectApplicable()) return;
        cardTypeStrategy.PerformEffect(cardFormatter, opponent);
    }
    
    public bool PerformAction(Card card, Player opponent)
    {
        FormatterCardRepresentation cardFormatter = CreateCardFormatter(card, CardPlayAsAction);
        ICardTypeStrategy cardTypeStrategy = cardFormatter.CardTypeStrategy!;
        if (!cardTypeStrategy.IsEffectApplicable()) return false;
        cardTypeStrategy.PerformEffect(cardFormatter, opponent);
        return true;
    }

    private void CleanDataAfterReversalFromMaze()
    {
        _game.ResetCardJockeyingForPositionEffects();
        _cardPlayedByOpponent = new FormatterCardRepresentation();
    }

    private void PerformReversalFromMaze(Card card)
    {
        _player.LastCardPlayedFromDeck = CardSetFull.Arsenal;
        FormatterCardRepresentation cardFormatter = CreateCardFormatter(card, ReversalCardType);
        ICardTypeStrategy cardTypeStrategy = cardFormatter.CardTypeStrategy!;
        cardTypeStrategy.PerformEffect(_cardPlayedByOpponent, _player);
    }

    private FormatterCardRepresentation CreateCardFormatter(Card card, string type)
    {
        var formaterPlayableCardInfo = Formatter.PlayToString(new FormatterPlayableCardInfo(card,
            ReversalCardType));
        FormatterCardRepresentation cardPlayed =  new FormatterCardRepresentation
        {
            CardInObjectFormat = card,
            CardInStringFormat = formaterPlayableCardInfo,
            Type = ReversalCardType,
            CardTypeStrategy = _factory.BuildCard(card, type.ToUpper())
        };
        return cardPlayed;
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
        _cardTypeStrategyPlayedByOpponent = _factory.BuildCard(card, ReversalCardType.ToUpper());
        if (IsIfIsNonReversalCard()) return false;
        return IsReversalStrategyApplicable(card);
    }
    
    private bool IsReversalStrategyApplicable(Card cardPlayed)
    {
        int fortitude = _game.GetOptionChoosedForJockeyingForPosition() == SelectedEffectFull.NextGrapplesReversalIsPlus8F ? 
            GrappleFortitudePlusForReversal : 0;
        return _cardTypeStrategyPlayedByOpponent!.IsEffectApplicable() &&
                      GetFortitude() >= int.Parse(cardPlayed.Fortitude) + fortitude &&
                      CheckIfIsReversal(cardPlayed);
    }
    
    private bool IsIfIsNonReversalCard()
    {
        FormatterCardRepresentation cardPlayed = GetLastCardPlayedByOpponent();
        Card card = cardPlayed.CardInObjectFormat!;
        if (cardPlayed.Type == null) return false;
        switch (card.Title)
        {
            case "Tree of Woe":
                return true;
            case "Austin Elbow Smash":
                return true;
            default:
                return false;
        }
    }
    
    private bool CheckIfIsReversal(Card card)
    {
        return card.Subtypes!.Any(subtype => subtype.Contains(ReversalCardType));
    }
    
    private void AddAllTypesToReversalCardsList(Card card)
    {
        foreach (var type in card.Types!.Where(type => type.Contains(ReversalCardType)))
        {
            AddOneTypeToReversalCardsList(card, type);
        }
    }

    private void AddOneTypeToReversalCardsList(Card card, string type)
    {
        var formaterPlayableCardInfo = Formatter.PlayToString(
            new FormatterPlayableCardInfo(card, type.ToUpper()));
        _reversalCardsInHandInStringListFormat!.Add(formaterPlayableCardInfo);
        _reversalCardsInHand!.Add( new FormatterCardRepresentation
        {
            CardInObjectFormat = card,
            CardInStringFormat = formaterPlayableCardInfo,
            Type = type.ToUpper(),
            CardTypeStrategy = _factory.BuildCard(card, type.ToUpper())
        });
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
        return _reversalCardsInHand!.Count != EmptyDeck
               && _reversalCardsInHand.Any(card =>
                   CheckReversalOfTheCardPlayedByTheOpponent(card.CardInObjectFormat!)); ;
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
    
    public FormatterCardRepresentation GetLastCardPlayed()
    {
        return _cardPlayed;
    }
}