using RawDealView;

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
    private const int OptionSeeCardsInHand = 0;
    private const int OptionSeeCardsInRingArea = 1;
    private const int OptionSeeCardsInRingside = 2;
    private const int OptionSeeCardsInOpponentRingArea = 3;
    private const int OptionSeeCardsInOpponentRingside = 4;

    private int _optionChoosed;
    private int _optionWhichCardsToSee;
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
        int index = _playersList[0].SuperCard!.SuperstarValue >= _playersList[1].SuperCard!.SuperstarValue ? 0 : 1;
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
        _view.SayThatATurnBegins(_playerOnTurn.SuperCard!.Name);
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
        if (_optionChoosed == OptionSeeCards) { ChooseWhichCardsYouWantToSee();}
        else if (_optionChoosed == OptionPlayCard) { ChooseWhichCardDoYouWantToPlayOrPass(_view.AskUserToSelectAPlay(_playerOnTurn.PlayableCardsInHandInStringFormat()));}
        else if (_optionChoosed == OptionEndTurn) { }
        else if (_optionChoosed == OptionGiveUp) { PlayerOnTurnGiveUp();}
        else if (_optionChoosed == OptionUseAbility) { UseSuperCardAbilityOncePerTurn();}
    }

    private void ChooseWhichCardsYouWantToSee()
    {
        _optionWhichCardsToSee = (int)_view.AskUserWhatSetOfCardsHeWantsToSee();
        if (_optionWhichCardsToSee == OptionSeeCardsInHand) { _view.ShowCards(_playerOnTurn.CardsInHandInStringFormat()); }
        else if (_optionWhichCardsToSee == OptionSeeCardsInRingArea) {_view.ShowCards(_playerOnTurn.CardsInRingAreaInStringFormat());}
        else if (_optionWhichCardsToSee == OptionSeeCardsInRingside) {_view.ShowCards(_playerOnTurn.CardsInRingsideInStringFormat());}
        else if (_optionWhichCardsToSee == OptionSeeCardsInOpponentRingArea) {_view.ShowCards(_playerWaiting.CardsInRingAreaInStringFormat());}
        else if (_optionWhichCardsToSee == OptionSeeCardsInOpponentRingside) {_view.ShowCards(_playerWaiting.CardsInRingsideInStringFormat());}
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
        damage = _playerWaiting.SuperStar.IsManKind() ? damage - 1 : damage;
        _view.SayThatSuperstarWillTakeSomeDamage(_playerWaiting.SuperCard!.Name, damage);
        _thePlayerRunOutOfArsenalCardsInMiddleOfTheAttack = _playerWaiting.TakeDamage(damage);
    }

    private bool GameHasWinner()
    {
        CheckIfSomePlayerRunOutOfArsenalCards();
        return _winnerPlayer != null;
    }
    
    private void SayWhoWins()
    {
        _view.CongratulateWinner(_winnerPlayer!.SuperCard!.Name);
    }
    
    private void PlayerOnTurnGiveUp()
    {
        _winnerPlayer = _playerWaiting;
    }
    
    private void CheckIfSomePlayerRunOutOfArsenalCards()
    {
        if (_playerOnTurn.CardsInArsenalInStringFormat().Count == 0) 
        { _winnerPlayer = _playerWaiting; }
        else if (_playerWaiting.CardsInArsenalInStringFormat().Count == 0) 
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
        _view.SayThatPlayerIsGoingToUseHisAbility(_playerOnTurn.SuperCard!.Name, _playerOnTurn.SuperCard!.SuperstarAbility!);
        _playerOnTurn.SuperStar.UseAbility(_playerWaiting);
        _playerUseHisAbilityInTheTurn = true;
    }
    
}
