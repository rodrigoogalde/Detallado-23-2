using RawDeal.Cards;
using RawDeal.Collections;
using RawDeal.Decks;
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
    
    private int _indexCardToDiscard;
    
    private PlayerDecksCollections _decksCollections;
    private readonly string? _pathDeck;
    private int _fortitude;

    public Player(string pathDeck, View view)
    {
        _pathDeck = pathDeck;
        _view = view;
        
        SetUpDeck();
    }

    private void SetUpDeck()
    {
        var deckLines  = File.ReadAllLines(_pathDeck!);
        SetSuperCard(deckLines);
        AddCardsFromTxtToDeck(deckLines.Skip(1).ToArray());
    }
    
    private void SetSuperCard(IReadOnlyList<string> deckLines)
    {
        var superName = deckLines[0].Replace(" (Superstar Card)", "");
        SelectWhichSuperStar(superName);
        _decksCollections = new PlayerDecksCollections(SuperStar);
    }

    private void SelectWhichSuperStar(string superName)
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
            Card card = new Card(line);
            _decksCollections.CheckIfHaveValidDeckWhenYouAddCard(card);
            _decksCollections.AddCardToArsenal(card);
        }
        _decksCollections.IsDeckSizeCorrect();
    }

    public void DrawCard()
    {
        _decksCollections.AddCardToHandFromArsenal();
    }

    public void DrawFirstHand()
    {
        SuperCardInfo superCard = SuperStar.SuperCard;
        int cardsToDraw = superCard.HandSize;
        for (int i = 0; i < cardsToDraw; i++)
        {
            DrawCard();
        }
    }

    public StringListCollection TransformMazeToStringFormat(CardSetFull cardSet)
    {
        return _decksCollections.ChooseWhichMazeOfCardsTransformToStringFormat(cardSet);
    }
    
    public StringListCollection GimeMePlayeableCardsFromHandInStringFormat()
    {
        return _decksCollections.MakeAListOfCardsThatArePlayeableFromHand().Item2;
    }
    
    public StringListCollection GimeMeReversalCardsInStringFormat()
    {
        return _decksCollections.MakeAListOfReversalCardsInStringFormat();
    }

    public CardRepresentationListCollection GimeMeReversalCardsInCardFormat()
    {
        return _decksCollections.MakeAListOfReversalCardsOnCardFormat();
    }
    
    public void PlayCardAsAction(Card cardToPlay)
    {   
        _view.SayThatPlayerMustDiscardThisCard(SuperStar.Name!, cardToPlay.Title);
        DrawCard();
        _view.SayThatPlayerDrawCards(SuperStar.Name!, 1);
        _decksCollections.MoveCardBetweenDecks(cardToPlay,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingsidePile));
    }
    
    public void MoveCardFromHandToRingArea(Card cardToDiscard)
    {
        _fortitude += int.Parse(cardToDiscard.Damage!);
        _decksCollections.MoveCardBetweenDecks(cardToDiscard,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingArea));
    }
    
    private Card MoveLastCardFromArsenalToRingSide()
    {
        Card card = _decksCollections.GetArsenalDeck().Last();
        _decksCollections.MoveCardBetweenDecks(card,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Arsenal, CardSetFull.RingsidePile));
        return card;
    }
    
    public FormatterCardRepresentation CheckWhichCardWillBePlayed(int indexCardToDiscard)
    {
        _indexCardToDiscard = indexCardToDiscard;
        CardRepresentationListCollection playeablesCardsInHand = _decksCollections.MakeAListOfCardsThatArePlayeableFromHand().Item1;
        var cardToDiscardInBothFormats = playeablesCardsInHand[_indexCardToDiscard];
        _view.SayThatPlayerIsTryingToPlayThisCard(SuperStar.Name!, cardToDiscardInBothFormats.CardInStringFormat!);
        return cardToDiscardInBothFormats;
    }
    
    public bool TakeDamage(int damage)
    {
        int i;
        for (i = 1; (i - 1) < damage && !_decksCollections.IsArsenalDeckEmpty(); i++)
        {
            Card card = MoveLastCardFromArsenalToRingSide();
            _view.ShowCardOverturnByTakingDamage(Formatter.CardToString(new FormatterCardInfo(card)), i, damage);
            _decksCollections.TryReversalFromMaze(card, damage - i);
        }
        return _decksCollections.IsArsenalDeckEmpty() && (i - 1) != damage;
    }
    
    public void SetTheCardPlayedByOpponent(FormatterCardRepresentation card)
    {
        _decksCollections.SetTheCardPlayedByOpponent(card);
    }
    
    public void MoveCardFromRingsideToArsenalWithIndex(int index)
    {
        _decksCollections.MoveCardBetweenDecks(_decksCollections.GetRingsideDeck()[index],
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.RingsidePile, CardSetFull.Arsenal));
    }
    
    public bool CanReverseTheCardPlayed()
    {
        return _decksCollections.PlayerHasAllConditionsToPlayReversalFromHand();
    }
    
    public void DiscardCardFromHandToRingsideWithIndex(int index)
    {
        Card cardToDiscard = _decksCollections.GetHandCards()[index];
        _decksCollections.MoveCardBetweenDecks(cardToDiscard,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingsidePile));
    }

    public void CardFromHandToRingside(Card card)
    {
        _decksCollections.MoveCardBetweenDecks(card,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingsidePile));
    }
    
    public void MoveCardFromRingsideToHandWithIndex(int index)
    {
        _decksCollections.MoveCardBetweenDecks(_decksCollections.GetRingsideDeck()[index],
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.RingsidePile, CardSetFull.Hand));
    }
    
    public void MoveTopeCardFromArsenalToHand()
    {
        Card card = _decksCollections.GetArsenalDeck().Last();
        _decksCollections.MoveCardBetweenDecks(card,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Arsenal, CardSetFull.Hand));
    }
    
    public void MoveCardFromHandToArsenalBottomWithIndex(int index)
    {
        Card card = _decksCollections.GetHandCards()[index];
        _decksCollections.MoveCardBetweenDecks(card,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.Arsenal));
    }
    
    public PlayerInfo GetPlayerInfo()
    {
        SuperCardInfo superCardInfo = SuperStar.SuperCard;
        return new PlayerInfo(superCardInfo.Name, _fortitude,
            _decksCollections.GetHandCards().Count,
            _decksCollections.GetArsenalDeck().Count);
    }
    
    public void CleanDataFromPastTurn()
    {
        SetTheCardPlayedByOpponent(new FormatterCardRepresentation());
    }
    
}