using RawDeal.Cards;
using RawDeal.Utils;
using RawDealView.Formatters;

namespace RawDeal.Decks;

public class PlayersDecksCollections
{
    private DeckCollection _cardsInRingside;
    private DeckCollection _cardsInRingArea;
    private DeckCollection _cardsInHand;
    private DeckCollection _cardsInArsenal;
    private StringCollection _playeablesCardsInHandInStringFormat;
    private StringCollection _reversalCardsInHandInStringFormat;
    private CardRepresentationCollection _playeablesCardsInHand;
    private CardRepresentationCollection _reversalCardsInHand;
    private FormatterCardRepresentation _cardPlayedByOpponent;
    
    private const int MaxDeckSize = 60;
    private const int EmptyDeck = 0;
    private const string ReversalCardType = "Reversal";
    private const string CardPlayAsAction = "Action";
    
    
    public PlayersDecksCollections()
    {
        _cardsInArsenal = new DeckCollection(new List<Card>());
        _cardsInHand = new DeckCollection(new List<Card>());
        _cardsInRingside = new DeckCollection(new List<Card>());
        _cardsInRingArea = new DeckCollection(new List<Card>());
    }

    private int GetFortitude()
    {
        return _cardsInRingArea.Sum(card => int.Parse(card.Damage!));
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
        foreach (var card in _cardsInHand.Where(card => card.IsPlayeableCard(GetFortitude())))
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
    
    public void SetTheCardPlayedByOpponent(FormatterCardRepresentation card)
    {
        _cardPlayedByOpponent = card;
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
        return _cardPlayedByOpponent.Type == CardPlayAsAction.ToUpper() &&
               card.Subtypes!.Any(subtype =>
                   subtype.Contains("ReversalAction") && GetFortitude() >= int.Parse(card.Fortitude));
    }

    private bool CheckReversalForCardType(Card card)
    {
        Card cardPlayedByOpponent = _cardPlayedByOpponent.CardInObjectFormat!;
        return (from subtype in card.Subtypes! 
            where card.CanBeUsedAsReversal(GetFortitude()) && _cardPlayedByOpponent.Type != CardPlayAsAction.ToUpper()
            select subtype.Split(ReversalCardType)[1]).Any(typeOfReversal => cardPlayedByOpponent.Subtypes!.Contains(typeOfReversal));
    }
}