using RawDeal.SuperStarsCards;
using RawDealView;
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
    
    private const int OptionUseAbility = 0;
    private const int OptionSeeCards = 1;
    private const int OptionPlayCard = 2;
    private const int OptionEndTurn = 3;
    private const int OptionGiveUp = 4;
    
    private const int OptionComeBack = -1;
    
    private const int MaindKindDamageReduction = 1;
    private const int EmptyDeck = 0;

    private int _optionChoosed;
    private CardSet _optionWhichCardsToSee;
    private bool _thePlayerRunOutOfArsenalCardsInMiddleOfTheAttack;
    private bool _playerCanUseHisAbility;
    private bool _playerUseHisAbilityInTheTurn;


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
        int index = _playersList[0].SuperStarCardInfo!.SuperstarValue >= _playersList[1].SuperStarCardInfo!.SuperstarValue ? 0 : 1;
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
        _view.SayThatATurnBegins(_playerOnTurn.SuperStarCardInfo!.Name);
        UseSuperCardAbilityBeforeDrawCard();
        CheckIfPlayerHasTheConditionsToUseHisAbility();
    }

    private void DrawSegment()
    {
        _playerOnTurn.DrawCard();
        if (_playerOnTurn.SuperStar.CanSteelMoreThanOneCard()) 
        { _playerOnTurn.DrawCard(); }
    }

    private void PlayerPlayHisTurn()
    {
        while (_optionChoosed != OptionEndTurn && _optionChoosed != OptionGiveUp && !_thePlayerRunOutOfArsenalCardsInMiddleOfTheAttack)
        {
            CheckIfPlayerHasTheConditionsToUseHisAbility();
            _view.ShowGameInfo(_playerOnTurn.GetPlayerInfo(), _playerWaiting.GetPlayerInfo());  
            ChooseAnOption();
        }
    }

    private void ChooseAnOption()
    {
        _optionChoosed = (_playerCanUseHisAbility && !_playerUseHisAbilityInTheTurn) ? (int)_view.AskUserWhatToDoWhenUsingHisAbilityIsPossible() : (int)_view.AskUserWhatToDoWhenHeCannotUseHisAbility();
        WhatToDoWithTheOptionChoosed();
    }
    
    private void WhatToDoWithTheOptionChoosed()
    {
        switch (_optionChoosed)
        {
            case OptionSeeCards:
                ChooseWhichCardsYouWantToSee();
                break;
            case OptionPlayCard:
                ChooseWhichCardDoYouWantToPlayOrPass(_view.AskUserToSelectAPlay(_playerOnTurn.TransformPlayableCardsIntoHandInStringFormat()));
                break;
            case OptionEndTurn:
                break;
            case OptionGiveUp:
                PlayerOnTurnGiveUp();
                break;
            case OptionUseAbility:
                UseSuperCardAbilityOncePerTurn();
                break;
        }
    }

    private void ChooseWhichCardsYouWantToSee()
    {
        _optionWhichCardsToSee = _view.AskUserWhatSetOfCardsHeWantsToSee();
        if (_optionWhichCardsToSee is CardSet.OpponentsRingArea or CardSet.OpponentsRingsidePile)
        {
            _view.ShowCards(_playerWaiting.ChooseWhichMazeOfCardsTransformToStringFormat(_optionWhichCardsToSee));
        }
        else
        {
            _view.ShowCards(_playerOnTurn.ChooseWhichMazeOfCardsTransformToStringFormat(_optionWhichCardsToSee));
        }
        
        // ELIMINAR
        // if (_optionWhichCardsToSee == CardSet.Hand) { _view.ShowCards(_playerOnTurn.TransformCardsInHandIntoStringFormat()); }
        // else if (_optionWhichCardsToSee == CardSet.RingArea) {_view.ShowCards(_playerOnTurn.TransformCardsInRingAreaIntoStringFormat());}
        // else if (_optionWhichCardsToSee == CardSet.RingsidePile) {_view.ShowCards(_playerOnTurn.TransformCardsInRingsideIntoStringFormat());}
        // else if (_optionWhichCardsToSee == CardSet.OpponentsRingArea) {_view.ShowCards(_playerWaiting.TransformCardsInRingAreaIntoStringFormat());}
        // else if (_optionWhichCardsToSee == CardSet.OpponentsRingsidePile) {_view.ShowCards(_playerWaiting.TransformCardsInRingsideIntoStringFormat());}
    }
    
    private void ChooseWhichCardDoYouWantToPlayOrPass(int optionCardChoosed)
    {
        if (optionCardChoosed != OptionComeBack) { PlayerChooseToPlayACard(optionCardChoosed); } 
    }
    
    private void PlayerChooseToPlayACard(int optionCardChoosed)
    {
        var cardChoseen = _playerOnTurn.DiscardCardPlayableFromHandToRingside(optionCardChoosed);
        _view.SayThatPlayerSuccessfullyPlayedACard();
        PlayerWaitingTakeDamage(Convert.ToInt32(cardChoseen.Damage));
    }

    private void PlayerWaitingTakeDamage(int damage)
    {
        damage = IsDamageReducedForManKind(_playerWaiting.SuperStar) ? damage - MaindKindDamageReduction : damage;
        _view.SayThatSuperstarWillTakeSomeDamage(_playerWaiting.SuperStarCardInfo!.Name, damage);
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
        _view.CongratulateWinner(_winnerPlayer!.SuperStarCardInfo!.Name);
    }
    
    private void PlayerOnTurnGiveUp()
    {
        _winnerPlayer = _playerWaiting;
    }
    
    private void CheckIfSomePlayerRunOutOfArsenalCards()
    {
        // ELIMINAR
        // if (_playerOnTurn.TransformCardsInArsenalIntoStringFormat().Count == EmptyDeck) 
        
        if (_playerOnTurn.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Arsenal).Count == EmptyDeck) 
        { _winnerPlayer = _playerWaiting; }
        
        // ELIMINAR
        // else if (_playerWaiting.TransformCardsInArsenalIntoStringFormat().Count == EmptyDeck) 
        else if (_playerWaiting.ChooseWhichMazeOfCardsTransformToStringFormat(CardSet.Arsenal).Count == EmptyDeck) 
        { _winnerPlayer = _playerOnTurn; }
    }

    private void PlayerPassTurn()
    {
        (_playerOnTurn, _playerWaiting) = (_playerWaiting, _playerOnTurn);
        (_optionChoosed, _optionWhichCardsToSee, _playerCanUseHisAbility, _playerUseHisAbilityInTheTurn) = (0, 0, false, false);
    }
    
    private void CheckIfPlayerHasTheConditionsToUseHisAbility()
    {
        _playerCanUseHisAbility = _playerOnTurn.SuperStar.HasTheConditionsToUseAbility();
    }
    
    private void UseSuperCardAbilityBeforeDrawCard()
    {
        _playerOnTurn.SuperStar.UseAbilityBeforeDrawing(_playerWaiting);
    }
    
    private void UseSuperCardAbilityOncePerTurn()
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(_playerOnTurn.SuperStarCardInfo!.Name, _playerOnTurn.SuperStarCardInfo!.SuperstarAbility!);
        _playerOnTurn.SuperStar.UseAbility(_playerWaiting);
        _playerUseHisAbilityInTheTurn = true;
    }
}
