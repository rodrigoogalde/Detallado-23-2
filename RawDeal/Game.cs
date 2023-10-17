using RawDeal.Cards;
using RawDeal.Options;
using RawDeal.SuperStarsCards;
using RawDeal.Utils;
using RawDealView;
using RawDealView.Formatters;
using RawDealView.Options;

namespace RawDeal;

public class Game
{
    private readonly View _view;
    private readonly string _deckFolder;
    private readonly List<Player> _playersList;
    private Player _playerOnTurn = null!;
    private Player _playerWaiting = null!;
    private Player? _winnerPlayer;
    
    private const int OptionComeBack = -1;
    
    private const int MaindKindDamageReduction = 1;
    private const int EmptyDeck = 0;

    private NextPlay _optionChoosed;
    private CardSetFull _optionWhichCardsToSee;
    private bool _thePlayerRunOutOfArsenalCardsInMiddleOfTheAttack;
    private bool _playerCanUseHisAbility;
    private bool _playerUseHisAbilityInTheTurn;
    
    private const string ActionCardType = "Action";
    private const string ManeuverCardType = "Maneuver";


    public Game(View view, string deckFolder)
    {
        _view = view;
        _deckFolder = deckFolder;
        _playersList = new List<Player>();
        _winnerPlayer = null;
        File.ReadAllText(Path.Combine("data", "cards.json"));
        
    }

    private void AddThisPlayer(Player player)
    {
        _playersList.Add(player);
    }

    public void Play()
    {
        try
        {
            ValidatePlayersDeck();
            InitializeGame();
        } catch (InvalidDeckException e) { e.InvalidDeckMessage(_view); }
    }

    private void ValidatePlayersDeck()
    {
        int numberOfPlayers = 2;
        for (var i = 0; i < numberOfPlayers; i++)
        {
            Player player = new Player(_view.AskUserToSelectDeck(_deckFolder), _view);
            AddThisPlayer(player);
        }
    }
    
    private void InitializeGame()
    {
        ChooseWhoStarts();  
        PlayersDrawFirstCards(); 
        StartGameFlow(); 
        SayWhoWins(); 
    }
    
    private void ChooseWhoStarts()
    {
        var player1SuperStarCard = _playersList[0].SuperStar;
        var player2SuperStarCard = _playersList[1].SuperStar;
        int index = player1SuperStarCard.SuperstarValue >= player2SuperStarCard.SuperstarValue ? 0 : 1;
        _playerOnTurn = _playersList[index];
        _playerWaiting = _playersList[(index + 1) % 2];
    }
    
    private void PlayersDrawFirstCards()
    {
        _playersList[0].DrawFirstHand();
        _playersList[1].DrawFirstHand();
    }

    private void StartGameFlow()
    {
        while (!GameHasWinner()) {
            BeforeDrawSegment();
            DrawSegment();
            PlayerPlayHisTurn();
            PlayerPassTurn();
        }
    }

    private void BeforeDrawSegment()
    {
        SuperStar superStarInfo = _playerOnTurn.SuperStar;
        _view.SayThatATurnBegins(superStarInfo.Name!);
        UseSuperCardAbilityBeforeDrawCard();
        CheckIfPlayerHasTheConditionsToUseHisAbility();
    }

    private void DrawSegment()
    {
        _playerOnTurn.DrawCard();
        var superStar = _playerOnTurn.SuperStar;
        if (superStar.CanSteelMoreThanOneCard()) 
        { _playerOnTurn.DrawCard(); }
    }

    private void PlayerPlayHisTurn()
    {
        while (_optionChoosed != NextPlay.EndTurn && _optionChoosed != NextPlay.GiveUp && !_thePlayerRunOutOfArsenalCardsInMiddleOfTheAttack)
        {
            CheckIfPlayerHasTheConditionsToUseHisAbility();
            _view.ShowGameInfo(_playerOnTurn.GetPlayerInfo(), _playerWaiting.GetPlayerInfo());  
            ChooseAnOption();
        }
    }

    private void ChooseAnOption()
    {
        _optionChoosed = (_playerCanUseHisAbility && !_playerUseHisAbilityInTheTurn) ? _view.AskUserWhatToDoWhenUsingHisAbilityIsPossible() : _view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        WhatToDoWithTheOptionChoosed();
    }
    
    private void WhatToDoWithTheOptionChoosed()
    {
        switch (_optionChoosed)
        {
            case NextPlay.ShowCards:
                ChooseWhichCardsYouWantToSee();
                break;
            case NextPlay.PlayCard:
                ChooseWhichCardDoYouWantToPlayOrPass(_view.AskUserToSelectAPlay(_playerOnTurn.TransformPlayableCardsInHandIntoStringFormat()));
                break;
            case NextPlay.EndTurn:
                break;
            case NextPlay.GiveUp:
                PlayerOnTurnGiveUp();
                break;
            case NextPlay.UseAbility:
                UseSuperCardAbilityOncePerTurn();
                break;
        }
    }

    private void ChooseWhichCardsYouWantToSee()
    {
        ChangeFormatterCardSet(_view.AskUserWhatSetOfCardsHeWantsToSee());
        _view.ShowCards(_optionWhichCardsToSee is CardSetFull.OpponentsRingArea or CardSetFull.OpponentsRingsidePile
            ? _playerWaiting.ChooseWhichMazeOfCardsTransformToStringFormat(_optionWhichCardsToSee)
            : _playerOnTurn.ChooseWhichMazeOfCardsTransformToStringFormat(_optionWhichCardsToSee));
    }

    private void ChangeFormatterCardSet(CardSet cardSet)
    {
        switch (cardSet)
        {
            case CardSet.Hand:
                _optionWhichCardsToSee = CardSetFull.Hand;
                break;
            case CardSet.RingArea:
                _optionWhichCardsToSee = CardSetFull.RingArea;
                break;
            case CardSet.RingsidePile:
                _optionWhichCardsToSee = CardSetFull.RingsidePile;
                break;
            case CardSet.OpponentsRingArea:
                _optionWhichCardsToSee = CardSetFull.OpponentsRingArea;
                break;
            case CardSet.OpponentsRingsidePile:
                _optionWhichCardsToSee = CardSetFull.OpponentsRingsidePile;
                break;
        }
    }
    
    private void ChooseWhichCardDoYouWantToPlayOrPass(int optionCardChoosed)
    {
        if (optionCardChoosed != OptionComeBack) { PlayerChooseToPlayACard(optionCardChoosed); } 
    }
    
    private void PlayerChooseToPlayACard(int optionCardChoosed)
    {
        FormatterCardInfo cardChoseenInBothFormats = _playerOnTurn.CheckWhichCardWillBePlayed(optionCardChoosed);
        _view.SayThatPlayerSuccessfullyPlayedACard();
        CheckPlayModeOfTheCardPlayed(cardChoseenInBothFormats);
    }
    
    private void CheckPlayModeOfTheCardPlayed(FormatterCardInfo cardChoseen)
    {
        Card card = cardChoseen.CardInObjectFormat!;
        if (cardChoseen.CardInStringFormat!.Contains(ActionCardType.ToUpper())) {
            _playerOnTurn.PlayCardAsAction(card);
        }
        else {
            PlayerWaitingTakeDamage(Convert.ToInt32(card.Damage)); 
            _playerOnTurn.MoveCardFromHandToRingArea(card);
        }
    }

    private void PlayerWaitingTakeDamage(int damage)
    {
        SuperStar superStarOpponent = _playerWaiting.SuperStar;
        damage = IsDamageReducedForManKind(superStarOpponent) ? damage - MaindKindDamageReduction : damage;
        if (damage == 0) { return; }
        _view.SayThatSuperstarWillTakeSomeDamage(superStarOpponent.Name!, damage);
        _thePlayerRunOutOfArsenalCardsInMiddleOfTheAttack = _playerWaiting.TakeDamage(damage);
    }
    
    private bool IsDamageReducedForManKind(SuperStar superstar)
    {
        return superstar.IsManKind();
    }

    private bool GameHasWinner()
    {
        CheckIfSomePlayerRunOutOfArsenalCards();
        return _winnerPlayer != null;
    }
    
    private void SayWhoWins()
    {
        SuperStar superStarWinnerPlayer = _winnerPlayer!.SuperStar;
        _view.CongratulateWinner(superStarWinnerPlayer.Name!);
    }
    
    private void PlayerOnTurnGiveUp()
    {
        _winnerPlayer = _playerWaiting;
    }
    
    private void CheckIfSomePlayerRunOutOfArsenalCards()
    {
        if (_playerOnTurn.ChooseWhichMazeOfCardsTransformToStringFormat(CardSetFull.Arsenal).Count == EmptyDeck) 
        { _winnerPlayer = _playerWaiting; }
        else if (_playerWaiting.ChooseWhichMazeOfCardsTransformToStringFormat(CardSetFull.Arsenal).Count == EmptyDeck) 
        { _winnerPlayer = _playerOnTurn; }
    }

    private void PlayerPassTurn()
    {
        (_playerOnTurn, _playerWaiting) = (_playerWaiting, _playerOnTurn);
        (_optionChoosed, _optionWhichCardsToSee, _playerCanUseHisAbility, _playerUseHisAbilityInTheTurn) = (0, 0, false, false);
    }
    
    private void CheckIfPlayerHasTheConditionsToUseHisAbility()
    {
        SuperStar superStar = _playerOnTurn.SuperStar;
        _playerCanUseHisAbility = superStar.HasTheConditionsToUseAbility();
    }
    
    private void UseSuperCardAbilityBeforeDrawCard()
    {
        SuperStar superStar = _playerOnTurn.SuperStar;
        superStar.UseAbilityBeforeDrawing(_playerWaiting);
    }
    
    private void UseSuperCardAbilityOncePerTurn()
    {
        SuperStar superStar = _playerOnTurn.SuperStar;
        _view.SayThatPlayerIsGoingToUseHisAbility(superStar.Name!, superStar.SuperstarAbility!);
        _playerOnTurn.SuperStar.UseAbility(_playerWaiting);
        _playerUseHisAbilityInTheTurn = true;
    }
}
