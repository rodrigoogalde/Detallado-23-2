using RawDeal.Cards;
using RawDeal.Cards.Reversal;
using RawDeal.Collections;
using RawDeal.Decks;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;
using FormatterCardInfo = RawDeal.Cards.Formatter.FormatterCardInfo;

namespace RawDeal;

public class Player
{
    private readonly View _view;
    private readonly Game _game;
    public SuperStar SuperStar;
    private int _indexCardToDiscard;
    private PlayerDecksCollections _decksCollections;
    private readonly string? _pathDeck;
    private readonly CardsStrategiesFactory _factory;
    private int _fortitude;
    private SelectedEffectFull _optionChoosedForJockeyingForPosition = SelectedEffectFull.None;
    public CardSetFull LastCardPlayedFromDeck { get; set; }
    private const string ManeuverCardType = "Maneuver";

    public Player(string pathDeck, View view, Game game)
    {
        _pathDeck = pathDeck;
        _view = view;
        _game = game;
        _factory = new CardsStrategiesFactory(_view, this, _game);
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
        _decksCollections = new PlayerDecksCollections(SuperStar, _factory,
            _game, this);
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

    public void DrawFirstHand()
    {
        SuperCardInfo superCard = SuperStar.SuperCard;
        int cardsToDraw = superCard.HandSize;
        for (int i = 0; i < cardsToDraw; i++)
        {
            DrawCard();
        }
    }
    
    public void DrawCard()
    {
        _decksCollections.AddCardToHandFromArsenal();
    }

    public StringListCollection TransformMazeToStringFormat(CardSetFull cardSet)
    {
        return _decksCollections.ChooseWhichMazeOfCardsTransformToStringFormat(cardSet);
    }
    
    public StringListCollection? GimeMePlayeableCardsFromHandInStringFormat()
    {
        return _decksCollections.MakeAListOfCardsThatArePlayeableFromHand().Item2;
    }
    
    public StringListCollection? GimeMeReversalCardsInStringFormat()
    {
        return _decksCollections.MakeAListOfReversalCardsInStringFormat();
    }

    public CardRepresentationListCollection? GimeMeReversalCardsInCardFormat()
    {
        return _decksCollections.MakeAListOfReversalCardsOnCardFormat();
    }


    public void PlayCardAsManeuver(Card cardToPlay, Player opponent)
    {
        _decksCollections.MoveCardBetweenDecks(cardToPlay, 
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingArea));
        // _decksCollections.PerformManeuver(cardToPlay, opponent);
        
    }
    
    public void PlayCardAsAction(Card cardToPlay)
    {   
        if (CheckIfJockeyForPositionIsPlayed(cardToPlay)) return; 
        _view.SayThatPlayerMustDiscardThisCard(SuperStar.Name!, cardToPlay.Title);
        DrawCard();
        _view.SayThatPlayerDrawCards(SuperStar.Name!, 1);
        _decksCollections.MoveCardBetweenDecks(cardToPlay,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingsidePile));
    }
    
    public bool CheckIfJockeyForPositionIsPlayed(Card cardToPlay)
    {
        if (cardToPlay.Title != "Jockeying for Position") return false;
        SetOptionChoosedForJockeyingForPosition(_view.AskUserToSelectAnEffectForJockeyForPosition(SuperStar.Name!));
        _decksCollections.MoveCardBetweenDecks(cardToPlay,
             new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingArea));
        return true;
        
    }
    
    private void SetOptionChoosedForJockeyingForPosition(SelectedEffect optionChoosed)
    {
        switch (optionChoosed)
        {
            case SelectedEffect.NextGrappleIsPlus4D:
                _optionChoosedForJockeyingForPosition = SelectedEffectFull.NextGrappleIsPlus4D;
                break;
            case SelectedEffect.NextGrapplesReversalIsPlus8F:
                _optionChoosedForJockeyingForPosition = SelectedEffectFull.NextGrapplesReversalIsPlus8F;
                break;
        }
        _game.GetOptionChoosedForJockeyingForPosition();
    }
    
    public void PlayerLoosesEffectOfJockeyingForPosition()
    {
        _optionChoosedForJockeyingForPosition = SelectedEffectFull.None;
    }
    
    public void MoveCardFromHandToRingArea(Card cardToDiscard)
    { 
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
        CardRepresentationListCollection playeablesCardsInHand = 
            _decksCollections.MakeAListOfCardsThatArePlayeableFromHand().Item1;
        var cardToDiscardInBothFormats = playeablesCardsInHand[_indexCardToDiscard];
        _view.SayThatPlayerIsTryingToPlayThisCard(SuperStar.Name!,
            cardToDiscardInBothFormats.CardInStringFormat!);
        return cardToDiscardInBothFormats;
    }
    
    public bool TakeDamage(int damage)
    {
        int i;
        for (i = 1; (i - 1) < damage && !_decksCollections.IsArsenalDeckEmpty(); i++)
        {
            Card card = MoveLastCardFromArsenalToRingSide();
            _view.ShowCardOverturnByTakingDamage(Formatter.CardToString(
                new FormatterCardInfo(card)), i, damage);
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
        LastCardPlayedFromDeck = CardSetFull.Hand;
        return _decksCollections.PlayerHasAllConditionsToPlayReversalFromHand();
    }
    
    public void DiscardCardFromHandToRingsideWithIndex(int index)
    {
        Card cardToDiscard = _decksCollections.GetHandCards()[index];
        _decksCollections.MoveCardBetweenDecks(cardToDiscard,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingsidePile));
    }

    public void MoveCardFromHandToRingside(Card card)
    {
        _decksCollections.MoveCardBetweenDecks(card,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Hand, CardSetFull.RingsidePile));
    }
    
    public void MoveCardFromRingsideToHandWithIndex(int index)
    {
        _decksCollections.MoveCardBetweenDecks(_decksCollections.GetRingsideDeck()[index],
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.RingsidePile, CardSetFull.Hand));
    }
    
    public void MoveTopCardFromArsenalToHand()
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
    
    public void MoveTopCardFromArsenalToRingSide()
    {
        Card card = _decksCollections.GetArsenalDeck().Last();
        _decksCollections.MoveCardBetweenDecks(card,
            new Tuple<CardSetFull, CardSetFull>(CardSetFull.Arsenal, CardSetFull.RingsidePile));
    }
    
    public PlayerInfo GetPlayerInfo()
    {
        SuperCardInfo superCardInfo = SuperStar.SuperCard;
        return new PlayerInfo(superCardInfo.Name, _decksCollections.GetFortitude(),
            _decksCollections.GetHandCards().Count,
            _decksCollections.GetArsenalDeck().Count);
    }

    public FormatterCardRepresentation GetLastCardPlayedByOpponent()
    {
        return _decksCollections.GetLastCardPlayedByOpponent();
    }
    
    public SelectedEffectFull GetOptionChoosedForJockeyingForPosition()
    {
        return _optionChoosedForJockeyingForPosition;
    }
    
    public void CleanDataFromPastTurn(bool isPlayerReversedCardWithJockeyingForPosition)
    {
        SetTheCardPlayedByOpponent(new FormatterCardRepresentation());
        if (isPlayerReversedCardWithJockeyingForPosition) return;
        _optionChoosedForJockeyingForPosition = SelectedEffectFull.None;
    }
    
}